using TMPro;
using UnityEngine;

public class TRMovementDebug : MonoBehaviour
{
    public int frameCap = 60;
    public TextMeshProUGUI debugTxt;
    public CharacterLocomotion locomotion;


    private void Start() => RefreshFPSCap();

    private void LateUpdate() {
        var txt = string.Empty;
        txt += $"{locomotion.name} ({(locomotion.gameObject.activeSelf ? "Enabled" : "Disabled")}) \n" +
            $"Position: {locomotion.transform.position} \n" +
            $"Rotation: {locomotion.transform.rotation} \n" +
            $"AngleToMouse: {locomotion.angle} \n" +
            $"FPS: {(1f / Time.deltaTime).ToString("0")}";

        debugTxt.text = txt;
    }

    [ContextMenu("RefreshFPSCap")]
    private void RefreshFPSCap() => Application.targetFrameRate = frameCap;
}
