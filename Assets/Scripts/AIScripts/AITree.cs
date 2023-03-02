using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AITree : ScriptableObject
{
    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    public List<AINodeData> AINodeData = new List<AINodeData>();

    public static AINode GetNode(string type, string GUID = "") {
        
        switch (type) {
            case "AI_GetCivillianGoalNode":
                return new AI_GetCivillianGoalNode(GUID);
            case "AI_HasGoalNode":
                return new AI_HasGoalNode(GUID);
            case "AI_InCombatNode":
                return new AI_InCombatNode(GUID);
            case "AI_AlarmNode":
                return new AI_AlarmNode(GUID);
            case "AI_Cower":
                return new AI_Cower(GUID);
            case "AI_Flee":
                return new AI_Flee(GUID);
            case "Cop_Attack": //CHANGE HERE
                return new Cop_Attack(GUID); //CHANGE HERE
            case "Cop_Retreat": //CHANGE HERE
                return new Cop_Retreat(GUID); //CHANGE HERE
            case "Cop_GetRoom": //CHANGE HERE
                return new AI_GetRoom(GUID); //CHANGE HERE
            case "Cop_HasTarget": //CHANGE HERE
                return new Cop_HasTarget(GUID); //CHANGE HERE
            case "Cop_GetTarget": //CHANGE HERE
                return new Cop_GetTarget(GUID); //CHANGE HERE
            case "Cop_Cover": //CHANGE HERE
                return new Cop_Cover(GUID); //CHANGE HERE
            default:
                return new AINode(GUID);
        }
    }

    public AINode StartNode() {
        AINodeData start = GetNodeData(Start());
        return GetNode(start.NodeType, start.GUID);
    }

    public AINode[] Next(AINode node, AIBase aIBase) {

        NodeLinkData[] baseLinks = GetLinks(node);
        List<AINode> nodes = new List<AINode>();

        foreach (NodeLinkData link in baseLinks) {
            foreach (AINodeData data in AINodeData)
            {
                if (data.GUID.Equals(link.TargetNodeGuid))
                    nodes.Add(GetNode(data.NodeType, data.GUID));
            }
        }

        if (nodes.Count == 0)
            Debug.LogWarning("<AITree> Could not make successful link. " +
                $"\nAITree: {name}" +
                $"\nNode: {node.name}");

        if (node.Conditional) {
            foreach (NodeLinkData link in baseLinks) {
                if (link.PortName.Equals(node.BoolResult(aIBase) ? "True" : "False")) {
                    AINode x = GetNode(link.TargetNodeGuid);
                    Debug.Log(link + $"\n--------\n{x.GUID}");
                    return new AINode[] { 
                        GetNode(link.TargetNodeGuid)
                    };
                }
            }

            return new AINode[] {
                StartNode()
            };
        }

        var values = nodes.OrderBy(x => x.Weight(aIBase));

        return values.ToArray();
    }

    private AINodeData GetNodeData(NodeLinkData link) {

        foreach (AINodeData data in AINodeData) { 
            if(data.GUID.Equals(link.BaseNodeGuid))
                return data;    
        }

        return AINodeData[0];
    }

    private AINode GetNode(string GUID) {

        foreach (AINodeData data in AINodeData) {
            if (data.GUID.Equals(GUID))
                return GetNode(data.NodeType, GUID);
        }

        return StartNode();
    }

    private NodeLinkData[] GetLinks(AINode node) {
        List<NodeLinkData> edges = new List<NodeLinkData>();
        foreach (NodeLinkData link in NodeLinks) {
            if (node.GUID.Equals(link.BaseNodeGuid))
                edges.Add(link);
        }

        return edges.Count > 0 ? edges.ToArray() : new NodeLinkData[] { Start() };
    }

    private NodeLinkData Start() {
        foreach (NodeLinkData link in NodeLinks) { 
            if(link.PortName.Equals("START"))
                return link;
        }

        return NodeLinks[0];
    }
}
