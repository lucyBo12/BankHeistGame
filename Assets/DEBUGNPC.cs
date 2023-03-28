using Cinemachine;
using TMPro;
using UnityEngine;

public class DEBUGNPC : MonoBehaviour
{
    public TextMeshProUGUI text;
    public AIBase aiBase;

    // Start is called before the first frame update
    void Start()
    {
        aiBase = ObjectPool.Get(ObjectPool.CopPool).GetComponent<AIBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if(aiBase is null) return;

        text.text = $"Name: {aiBase.transform.name} \n" +
            $"Node: {(aiBase.currentNode is not null ? aiBase.currentNode.GetType().Name : "NULL")} \n" +
            $"In Range: {(aiBase.Target ? Physics.Raycast(aiBase.transform.position, (aiBase.Target.transform.position)) : "Null")}";
            
    }
}
