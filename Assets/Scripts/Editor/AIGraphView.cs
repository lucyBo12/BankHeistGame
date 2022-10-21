using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System;
using UnityEditor.UIElements;

public class AIGraphView : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2();

    public Blackboard Blackboard;
    public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
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
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            EntryPoint = true
        };

        var generatedPort = GeneratePort(node, Direction.Output);
        node.outputContainer.Add(generatedPort);

        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public void CreateNode(Vector2 position)
    {
        AddElement(CreateAITreeNode(position));
    }


    public AINode CreateAITreeNode(Vector2 position)
    {
        var AITreeNode = new AINode
        {
            title = "AINode",
            GUID = Guid.NewGuid().ToString()
        };

        //Input Port
        var inputPort = GeneratePort(AITreeNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        AITreeNode.inputContainer.Add(inputPort);

        //Output port
        var button = new Button(() => { AddChoicePort(AITreeNode); });
        button.text = "+";
        AITreeNode.titleContainer.Add(button);

        AITreeNode.RefreshExpandedState();
        AITreeNode.RefreshPorts();
        AITreeNode.SetPosition(new Rect(position, defaultNodeSize));

        return AITreeNode;
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

    public void ClearBlackBoardAndExposedProperties() 
    {
        ExposedProperties.Clear();
        Blackboard.Clear();
    }

    public void AddPropertyToBlackBoard(ExposedProperty exposedProperty) 
    {
        var localPropertyName = exposedProperty.PropertyName;
        var localPropertyValue = exposedProperty.PropertyValue;
        while (ExposedProperties.Any(x => x.PropertyName == localPropertyName))
            localPropertyName = $"{localPropertyName}(1)";

        var property = new ExposedProperty();
        property.PropertyName = localPropertyName;
        property.PropertyValue = localPropertyValue;
        ExposedProperties.Add(property);

        var container = new VisualElement();
        var blackBoardField = new BlackboardField { text = property.PropertyName, typeText = "string property" };
        container.Add(blackBoardField);

        var propertyValueTextField = new TextField("Value:") 
        { 
            value = localPropertyValue
        };
        propertyValueTextField.RegisterValueChangedCallback(evt => 
        {
            var changingPropertyIndex = ExposedProperties.FindIndex(x => x.PropertyName == property.PropertyName);
            ExposedProperties[changingPropertyIndex].PropertyValue = evt.newValue;
        });
        var blackBoardValueRow = new BlackboardRow(blackBoardField, propertyValueTextField);
        container.Add(blackBoardValueRow);

        Blackboard.Add(container);
    }

}
