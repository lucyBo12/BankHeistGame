using TMPro;
using UnityEngine;

public class InteractPromt : MonoBehaviour
{
    public TextMeshProUGUI messageTxt;

    public static GameObject CreateNewPrompt(Transform parent) {
        GameObject prefab = Resources.Load<GameObject>("InteractPrompt");   
        if(prefab == null) return null;

        return Instantiate(prefab, parent);
    }

    private void LateUpdate()
    {
        if(!Camera.main) return;

        transform.LookAt(Camera.main.transform);
    }

    public void SetPrompt(string message) {
        messageTxt.text = message;
    }
}
