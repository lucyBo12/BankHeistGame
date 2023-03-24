using UnityEngine;

public class DEBUG_CombatToggle : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GameManager.StartNewGame(); 
            GameManager.WantedLevel = 1;
            Debug.Log($"Start {GameManager.WantedLevel} State: {GameManager.State}");
        }
    }
}
