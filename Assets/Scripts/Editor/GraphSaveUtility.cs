using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;
using System.Linq;

/**
 * Compiles AITree scriptable object to '\Assets\Resources' folder
 * based on active AITree window.
 * 
 * author: Joseph Denby
 * email: jd744@kent.ac.uk
 */
public class GraphSaveUtility
{
    private AIGraphView _targetGraphView;   //Active AITree window
    private AITree _AITreeCache;            //AITree scriptable object

    //All connections between nodes in active window
    private List<Edge> Edges => _targetGraphView.edges.ToList();
    //All nodes in active window
    private List<AINode> Nodes => _targetGraphView.nodes.ToList().Cast<AINode>().ToList();

    /**
     * =====================================================================
     *  Returns new instance of this class.
     *  
     *  @params AIGraphView
     *  @return GraphSaveUtility
     * =====================================================================
     */
    public static GraphSaveUtility GetInstance(AIGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    /**
     *======================================================================
     *  Creates new instance of AITree scriptable object and compiles
     *  active window 'Edges' and 'AINodes' as 'NodeLinkData' and 'AINodeData'
     *  respectively.
     *  
     *  @params string
     *======================================================================
     */
    public void SaveGraph(string fileName)
    {
        var AITree = ScriptableObject.CreateInstance<AITree>();
        if (!SaveNodes(AITree)) return; //Return if tree is not viable

        //With AITree assembled we ensure save path
        if (!AssetDatabase.IsValidFolder("Assets/Resources/AI")) { 
            AssetDatabase.CreateFolder("Assets", "Resources"); 
            AssetDatabase.CreateFolder("Assets/Resources", "AI"); 
        }

        //Save AITree to '\Assets\Resources'
        AssetDatabase.CreateAsset(AITree, $"Assets/Resources/AI/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    /**
     * =======================================================================
     *  Compiles given AITree scriptable object with 'NodeLinkData' and 
     *  'AINodeData' based on active AITree window. Returns false if there
     *  are not Edges defined in current tree.
     *  
     *  @params AITree
     *  @return bool
     * =======================================================================
     */
    private bool SaveNodes(AITree AITree)
    {
        if (!Edges.Any()) {
            EditorUtility.DisplayDialog("Error", "Can't save new tree without at least single connected node!", ok: "Ok");
            return false;   //Not viable tree. Return false
        }

        //Get array of nodes that contain connections
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();

        //Compile 'NodeLinkData' for AITree
        for (var i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as AINode;
            var inputNode = connectedPorts[i].input.node as AINode;

            //Create new 'NodeLinkData' that represents 'outputNode' and 'inputNode' connection
            AITree.NodeLinks.Add(new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            }); ;
        }

        //Compile 'AINodeData' for AITree
        foreach (var AITreeNode in Nodes.Where(node => !node.EntryPoint))
        {
            //Create 'AINodeData' that represents 'AITreeNode'
            AITree.AINodeData.Add(new AINodeData
            {
                GUID = AITreeNode.GUID,
                NodeType = AITreeNode.GetType().Name,
                Position = AITreeNode.GetPosition().position
            });

        }

        return true;
    }

    /**
     * ==============================================================================
     * Finds AITree with given 'fileName' at path '\Assets\Resources' and builds
     * the graph based on AITree data.
     * 
     * @params string
     * ==============================================================================
     */
    public void LoadGraph(string fileName)
    {
        _AITreeCache = Resources.Load<AITree>("AI/" + fileName);
        if (_AITreeCache == null)
        {
            EditorUtility.DisplayDialog("AITree Tree not found!", "Unity could not find the AITree Tree. This is a problem for Joe.", ok: "Oh shit.");
            return; //Did not find AITree at '\Assets\Resources\<fileName>'
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void CreateNodes()
    {
        foreach (var nodeData in _AITreeCache.AINodeData)
        {
            AINode node = AITree.GetNode(nodeData.NodeType);
            System.Type type = node.GetType();

            if (type == typeof(AI_HasGoalNode))
                node = _targetGraphView.CreateAI_HasGoalNode();
            else if (type == typeof(AI_GetCivillianGoalNode))
                node = _targetGraphView.CreateAI_GetCivillianGoalNode();
            else if (type == typeof(AI_InCombatNode))
                node = _targetGraphView.CreateAI_InCombatNode();
            else if (type == typeof(AI_AlarmNode))
                node = _targetGraphView.CreateAI_AlarmNode();
            else if (type == typeof(AI_Cower))
                node = _targetGraphView.CreateAI_Cower();
            else if (type == typeof(AI_Flee))
                node = _targetGraphView.CreateAI_Flee();
            else if (type == typeof(Cop_Attack)) //CHANGE HERE
                node = _targetGraphView.CreateCop_Attack(); //CHANGE HERE
            else if (type == typeof(Cop_Retreat)) //CHANGE HERE
                node = _targetGraphView.CreateCop_Retreat(); //CHANGE HERE
            else if (type == typeof(AI_GetRoom)) //CHANGE HERE
                node = _targetGraphView.CreateAI_GetRoom(); //CHANGE HERE
            else if (type == typeof(Cop_HasTarget)) //CHANGE HERE
                node = _targetGraphView.CreateCop_HasTarget(); //CHANGE HERE
            else if (type == typeof(Cop_GetTarget)) //CHANGE HERE
                node = _targetGraphView.CreateCop_GetTarget(); //CHANGE HERE
            else if (type == typeof(Cop_Cover)) //CHANGE HERE
                node = _targetGraphView.CreateCop_Cover(); //CHANGE HERE

            node.GUID = nodeData.GUID;
            _targetGraphView.AddElement(node);

            //Only do the below for custom choice nodes (might not need)
            /*var nodePorts = _AITreeCache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.Guid).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(node, x.PortName));*/

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
                if (Nodes[i] == null)
                {
                    Debug.LogError("EMPTY NODE SAVE!!");
                }
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(_AITreeCache.AINodeData.First(x => x.GUID == targetNodeGuid).Position, _targetGraphView.defaultNodeSize));
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