using TMPro;
using UnityEngine;

public class TRAIDebug : MonoBehaviour
{
    [SerializeField] AIBase ai;
    [SerializeField] TextMeshProUGUI debugText;

    private void Update()
    {
        debugText.text = $"'{ai.name}' active: [{ai.gameObject.activeSelf}] " +
            $"\nAIEnabled: [{ai.aiEnabled}]" +
            $"\nNode: {ai.currentNode.ToString(ai)}";
    }

}
