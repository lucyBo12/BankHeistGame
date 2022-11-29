using UnityEngine.UI;
using UnityEngine;

public class DEBUG_CombatToggle : MonoBehaviour
{
    public Toggle toggle;
    public void CombatToggle() => GameManager.InCombat = toggle.isOn;
}
