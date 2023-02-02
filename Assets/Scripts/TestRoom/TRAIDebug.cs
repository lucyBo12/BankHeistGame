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
            $"\nNode: {(ai.currentNode == null ? "<NULL>" : ai.currentNode.ToString(ai))}" +
            $"\nInCombat: {GameManager.InCombat} " +
            $"\nGoal: {(ai.Goal.HasGoal ? $"{ai.Goal.TargetLocation}" : "NULL")}";
    }


}
