using UnityEngine;

public class AIBase : MonoBehaviour
{
    public AICivillianMovement AICivillianMovement = new AICivillianMovement();

    private void Start()
    {
        Debug.Log($"Node Weight = {AICivillianMovement.Weight()}");
        AICivillianMovement.CallStart();
    }

    private void Update()
    {
        AICivillianMovement.CallUpdate();
    }


}
