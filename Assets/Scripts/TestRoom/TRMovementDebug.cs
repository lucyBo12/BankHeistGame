using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TRMovementDebug : MonoBehaviour
{
    public TextMeshProUGUI debugTxt;
    public CharacterLocomotion locomotion;
    public float fps;


    private void Start() { 
        UpdateFPS();
        GameManager.Input.Player.Enable();
    }

    private void LateUpdate() {
        var txt = string.Empty;
        txt += $"{locomotion.name} ({(locomotion.gameObject.activeSelf ? "Enabled" : "Disabled")}) \n" +
            $"Position: {locomotion.transform.position.ToString("0.00")} \n" +
            $"Rotation: {locomotion.transform.localRotation.eulerAngles.ToString("0.00")} \n" +
            $"AngleToMouse: {locomotion.angle.ToString("0")} \n" +
            $"FPS: {fps.ToString("0")}";

        debugTxt.text = txt;
    }

    private async void UpdateFPS() { 
        fps = 1f / Time.deltaTime;
        await Task.Delay(300);
        UpdateFPS();
    }

}
