using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System;

public class AIGraphView : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2();
    private NodeSearchWindow _searchWindow;
    

    public AIGraphView(EditorWindow editorWindow)
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        AddElement(GenerateEntryPointNode());
        AddSearchWindow(editorWindow);
    }

    private void AddSearchWindow(EditorWindow editorWindow)
    {
        var _searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        _searchWindow.Init(editorWindow, this);
        nodeCreationRequest = contect => SearchWindow.Open(new SearchWindowContext(contect.screenMousePosition), _searchWindow);
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach((port) => 
        {
            if (startPort != port && startPort.node != port.node)
            { compatiblePorts.Add(port); }
        });

        return compatiblePorts;
    }

    private Port GeneratePort(AINode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    private AINode GenerateEntryPointNode()
    {
        var node = new AINode
        {
            GUID = Guid.NewGuid().ToString(),
            EntryPoint = true
        };

        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "START";
        node.outputContainer.Add(generatedPort);

        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public void AddNode(AINode node, Vector2 position) {
        node.RefreshExpandedState();
        node.RefreshPorts();
        node.SetPosition(new Rect(position, defaultNodeSize));
        AddElement(node);
    }

    public AI_HasGoalNode CreateAI_HasGoalNode() {
        var node = new AI_HasGoalNode
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };

        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);

        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "True";
        node.outputContainer.Add(outPort);

        //Output Port
        var outPortB = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPortB.portName = "False";
        node.outputContainer.Add(outPortB);

        node.title = "Has Goal?";

        return node;
    }

    public AI_InCombatNode CreateAI_InCombatNode()
    {
        var node = new AI_InCombatNode
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };

        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);

        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "True";
        node.outputContainer.Add(outPort);

        //Output Port
        var outPortB = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPortB.portName = "False";
        node.outputContainer.Add(outPortB);

        node.title = "In Combat?";

        return node;
    }

    public AI_Cower CreateAI_Cower()
    {
        var node = new AI_Cower
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };
        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);


        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "Next";
        node.outputContainer.Add(outPort);

        node.title = " make civillian cower"; return node;

    }

    public AI_Flee CreateAI_Flee()
    {
        var node = new AI_Flee
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };
        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);


        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "Next";
        node.outputContainer.Add(outPort);

        node.title = " make civillian Flee"; return node;

    }

    public AI_GetCivillianGoalNode CreateAI_GetCivillianGoalNode()
    {
        var node = new AI_GetCivillianGoalNode
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };

        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);

        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "Next";
        node.outputContainer.Add(outPort);

        node.title = "Get Civillian Goal";

        return node;
    }

    public AI_AlarmNode CreateAI_AlarmNode()
    {
        var node = new AI_AlarmNode
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };

        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);

        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "Next";
        node.outputContainer.Add(outPort);

        node.title = "Press Alarm";

        return node;
    }

    public Cop_Attack CreateCop_Attack() //CHANGE HERE
    {
        var node = new Cop_Attack //CHANGE HERE
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };
        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);


        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "Next";
        node.outputContainer.Add(outPort);

        node.title = " make cops attack"; //CHANGE HERE
        return node;

    }

    public Cop_Retreat CreateCop_Retreat() //CHANGE HERE *2
    {
        var node = new Cop_Retreat //CHANGE HERE
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };
        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);


        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "Next";
        node.outputContainer.Add(outPort);

        node.title = " make cops retreat"; //CHANGE HERE
        return node;

    }

    public AI_GetRoom CreateAI_GetRoom() //CHANGE HERE *2
    {
        var node = new AI_GetRoom //CHANGE HERE
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };
        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);


        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "Next";
        node.outputContainer.Add(outPort);

        node.title = " selects a room for cops to move to in combat"; //CHANGE HERE
        return node;

    }
    public Cop_HasTarget CreateCop_HasTarget()
    {
        var node = new Cop_HasTarget
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };

        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);

        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "True";
        node.outputContainer.Add(outPort);

        //Output Port
        var outPortB = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPortB.portName = "False";
        node.outputContainer.Add(outPortB);

        node.title = "Has Target?";

        return node;
    }
    public Cop_GetTarget CreateCop_GetTarget() //CHANGE HERE *2
    {
        var node = new Cop_GetTarget //CHANGE HERE
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };
        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);


        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "Next";
        node.outputContainer.Add(outPort);

        node.title = " Gets the cop's target"; //CHANGE HERE
        return node;

    }
    public Cop_Cover CreateCop_Cover() //CHANGE HERE *2
    {
        var node = new Cop_Cover //CHANGE HERE
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };
        //Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);


        //Output Port
        var outPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outPort.portName = "Next";
        node.outputContainer.Add(outPort);

        node.title = " checks if the cop should go in cover or not"; //CHANGE HERE
        return node;

    }



    public void AddChoicePort(AINode node, string overriddenPortName = "")
    {
        var generatedPort = GeneratePort(node, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = node.outputContainer.Query("connector").ToList().Count;

        var deleteButton = new Button(() => RemovePort(node, generatedPort))
        {
            text = "x"
        };
        generatedPort.contentContainer.Add(deleteButton);
        node.outputContainer.Add(generatedPort);
        node.RefreshPorts();
        node.RefreshExpandedState();
    }

    private void RemovePort(AINode node, Port generatedPort)
    {
        var targetEdge = edges.ToList().Where(x => x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);
        var edge = targetEdge.First();
        edge.input.Disconnect(edge);
        RemoveElement(targetEdge.First());

        node.outputContainer.Remove(generatedPort);
        node.RefreshPorts();
        node.RefreshExpandedState();
    }




}
