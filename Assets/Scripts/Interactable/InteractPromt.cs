using TMPro;
using UnityEngine;

public class InteractPromt : MonoBehaviour
{
    public TextMeshProUGUI messageTxt;
    public GameObject arrow;

    public static GameObject CreateNewPrompt(Transform parent, Vector3 offset) {
        GameObject prefab = Resources.Load<GameObject>("InteractPrompt");   
        if(prefab == null) return null;
        GameObject prompt = Instantiate(prefab, parent);
        prompt.transform.rotation = new Quaternion(offset.x, offset.y, offset.z, 0);
        prompt.transform.localScale = Vector3.one;
        return prompt;
    }

    public void SetPrompt(string message) {
        messageTxt.text = message.Replace("{n}", transform.parent.name);
        LeanTween.rotateLocal(arrow, new Vector3(180, 0, 0), 0.33f);
    }
}
