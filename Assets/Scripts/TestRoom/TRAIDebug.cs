using TMPro;
using UnityEngine;

public class TRAIDebug : MonoBehaviour
{
    [SerializeField] AIBase ai;
    [SerializeField] Transform player;
    [SerializeField] Transform button;
    [SerializeField] TextMeshProUGUI debugText;


    private void Start() {
        GameManager.Players.Add(player);
        GameManager.Alarms.Add(button);
    }

    private void LateUpdate()
    {
        debugText.text = $"'{ai.name}' active: [{ai.gameObject.activeSelf}] " +
            $"\nAIEnabled: [{ai.aiEnabled}]" +
            $"\nNode: {(ai.currentNode == null ? "<NULL>" : ai.currentNode.ToString(ai))}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameManager.StartCombat();
        }
    }

}
