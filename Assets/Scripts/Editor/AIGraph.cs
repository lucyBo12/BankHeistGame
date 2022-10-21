using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Linq;

public class AIGraph : EditorWindow
{
    private AIGraphView _graphView;
    private string _fileName = "New AITree";

    [MenuItem("Window/AI/AITree")]
    public static void OpenAIGraphWindow()
    {
        var window = GetWindow<AIGraph>();
        window.titleContent = new GUIContent("AITree Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateMinimap();
    }


    private void ConstructGraphView()
    {
        _graphView = new AIGraphView(this)
        {
            name = "AITree"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("File Name:");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save" });
        toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load" });
        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("No file name!", "All new instances of AITree must have a file name." +
                "You can find the 'File Name' input field on the toolbar above the grid view.\n (Ask Joe if you're still lost)", ok: "Got it!");
            return;
        }

        var saveUtility = GraphSaveUtility.GetInstance(_graphView);
        if (save)
        {
            saveUtility.SaveGraph(_fileName);
        }
        else
        {
            saveUtility.LoadGraph(_fileName);
        }

    }

    private void GenerateMinimap()
    {
        var miniMap = new MiniMap { anchored = true }; ;
        var cords = _graphView.contentViewContainer.WorldToLocal(new Vector2(maxSize.x - 10, 30));
        miniMap.SetPosition(new Rect(cords.x, cords.y, 200, 140));
        _graphView.Add(miniMap);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
