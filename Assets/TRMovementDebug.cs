using TMPro;
using UnityEngine;

public class TRMovementDebug : MonoBehaviour
{
    public static TRMovementDebug instance;
    private void Awake() => instance = this;

    public Transform ball;
    public TextMeshProUGUI debugTxt;

    public void Message(string message) { 
        debugTxt.text += message;
    }

    public void MoveBall(Vector3 pos) {
        ball.position = pos;
    }

}
