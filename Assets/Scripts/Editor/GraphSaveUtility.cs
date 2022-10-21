using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class GraphSaveUtility
{
    private AIGraphView _targetGraphView;
    private AITree _AITreeCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<AINode> Nodes => _targetGraphView.nodes.ToList().Cast<AINode>().ToList();

    public static GraphSaveUtility GetInstance(AIGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName)
    {
        var AITree = ScriptableObject.CreateInstance<AITree>();
        if (!SaveNodes(AITree))
        {
            return;
        } 

        if (!AssetDatabase.IsValidFolder("Assets/Resources/AI")) { 
            AssetDatabase.CreateFolder("Assets", "Resources"); 
            AssetDatabase.CreateFolder("Assets/Resources", "AI"); 
        }

        AssetDatabase.CreateAsset(AITree, $"Assets/Resources/AI/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    private bool SaveNodes(AITree AITree)
    {
        if (!Edges.Any())
        {
            EditorUtility.DisplayDialog("Error", "Can't save new tree without at least single connected node!", ok: "Ok");
            return false;
        }

        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for (var i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as AINode;
            var inputNode = connectedPorts[i].input.node as AINode;

            AITree.NodeLinks.Add(new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            }); ;
        }

        foreach (var AITreeNode in Nodes.Where(node => !node.EntryPoint))
        {
            AITree.AINodeData.Add(new AINodeData
            {
                Guid = AITreeNode.GUID,
                Type = AITreeNode.GetType(),
                Position = AITreeNode.GetPosition().position
            });; ;
        }

        return true;
    }

    public void LoadGraph(string fileName)
    {
        _AITreeCache = Resources.Load<AITree>("AI/" + fileName);
        if (_AITreeCache == null)
        {
            EditorUtility.DisplayDialog("AITree Tree not found!", "Unity could not find the AITree Tree. This is a problem for Joe.", ok: "Oh shit.");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void CreateNodes()
    {
        foreach (var nodeData in _AITreeCache.AINodeData)
        {
            Debug.Log($"Type: {nodeData.Type.Name}");
            AINode node = _targetGraphView.CreateAI_HasGoalNode();


            node.GUID = nodeData.Guid;
            _targetGraphView.AddElement(node);

            var nodePorts = _AITreeCache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.Guid).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(node, x.PortName));

        }
    }

    private void ConnectNodes()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            var connections = _AITreeCache.NodeLinks.Where(x => x.BaseNodeGuid == Nodes[i].GUID).ToList();
            for (int j = 0; j < connections.Count; j++)
            {
                var targetNodeGuid = connections[j].TargetNodeGuid;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGuid);
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(_AITreeCache.AINodeData.First(x => x.Guid == targetNodeGuid).Position, _targetGraphView.defaultNodeSize));
            }
        }
    }

    private void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge 
        { 
            output = output,
            input = input
        };

        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);

        _targetGraphView.Add(tempEdge);
    }

    private void ClearGraph()
    {
        //Start point
        Nodes.Find(x => x.EntryPoint).GUID = _AITreeCache.NodeLinks[0].BaseNodeGuid;

        foreach (var node in Nodes)
        {
            if (node.EntryPoint) continue;
            Edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));
            _targetGraphView.RemoveElement(node);
        }
    }

}