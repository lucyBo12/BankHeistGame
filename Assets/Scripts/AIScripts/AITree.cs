﻿using System.Collections.Generic;
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

        var values = nodes.OrderBy(x => x.Weight(aIBase));

        if (nodes.Count == 0)
            Debug.LogWarning("<AITree> Could not make successful link. " +
                $"\nAITree: {name}" +
                $"\nNode: {node.name}");

        return values.ToArray();
    }

    private AINodeData GetNodeData(NodeLinkData link) {

        foreach (AINodeData data in AINodeData) { 
            if(data.GUID.Equals(link.BaseNodeGuid))
                return data;    
        }

        return AINodeData[0];
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
