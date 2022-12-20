using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private AIGraphView _graphView;
    private EditorWindow _window;
    private Texture2D _indentationIcon;

    public void Init(EditorWindow window, AIGraphView graphView)
    {
        _graphView = graphView;
        _window = window;

        _indentationIcon = new Texture2D(1, 1);
        _indentationIcon.SetPixel(0, 0, new Color(0, 0, 0, 0));
        _indentationIcon.Apply();
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Create Elements"), 0),
            
            //Conditional
            new SearchTreeGroupEntry(new GUIContent("Condition"), 1),
            new SearchTreeEntry(new GUIContent("Has Goal", _indentationIcon))
            {
                userData = new AI_HasGoalNode(),
                level = 2
            },
            new SearchTreeEntry(new GUIContent("In Combat", _indentationIcon)) { 
               userData = new AI_InCombatNode(),
               level = 2
            },

            //Action
            new SearchTreeGroupEntry(new GUIContent("Action"), 1),
            new SearchTreeEntry(new GUIContent("Get Civillian Goal", _indentationIcon))
            {
                userData = new AI_GetCivillianGoalNode(),
                level = 2
            },
            new SearchTreeEntry(new GUIContent("Press Alarm", _indentationIcon)) {
               userData = new AI_AlarmNode(),
               level = 2
            },
            new SearchTreeEntry(new GUIContent("Cower", _indentationIcon))
            {
                userData = new AI_Cower(),
                level = 2
            },
             new SearchTreeEntry(new GUIContent("Flee", _indentationIcon))
            {
                userData = new AI_Flee(),
                level = 2
            },

        };

        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var worldMousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent, context.screenMousePosition - _window.position.position);
        var localMousePosition = _graphView.contentViewContainer.WorldToLocal(worldMousePosition);

        switch (SearchTreeEntry.userData)
        {
            case AI_HasGoalNode:
                _graphView.AddNode(_graphView.CreateAI_HasGoalNode(), localMousePosition);
                return true;
            case AI_GetCivillianGoalNode:
                _graphView.AddNode(_graphView.CreateAI_GetCivillianGoalNode(), localMousePosition);
                return true;
            case AI_InCombatNode:
                _graphView.AddNode(_graphView.CreateAI_InCombatNode(), localMousePosition);
                return true;
            case AI_AlarmNode:
                _graphView.AddNode(_graphView.CreateAI_AlarmNode(), localMousePosition);
                return true;
            case AI_Cower:
                _graphView.AddNode(_graphView.CreateAI_Cower(), localMousePosition);
                return true;
            case AI_Flee:
                _graphView.AddNode(_graphView.CreateAI_Flee(), localMousePosition);
                return true;
            default:
                return false;
        }

    }
}