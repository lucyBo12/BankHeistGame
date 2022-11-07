using TMPro;
using UnityEngine;

public class InteractPromt : MonoBehaviour
{
    public TextMeshProUGUI messageTxt;
    public GameObject arrow;

    public static GameObject CreateNewPrompt(Transform parent) {
        GameObject prefab = Resources.Load<GameObject>("InteractPrompt");   
        if(prefab == null) return null;

        return Instantiate(prefab, parent);
    }

    public void SetPrompt(string message) {
        messageTxt.text = message.Replace("{n}", transform.parent.name);
        LeanTween.rotateLocal(arrow, new Vector3(180, 0, 0), 0.33f);
    }
}
