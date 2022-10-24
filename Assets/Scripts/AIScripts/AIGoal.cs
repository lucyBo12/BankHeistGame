using UnityEngine;

public struct AIGoal
{
    public Transform Target { get; private set; }
    public Vector3 TargetLocation { get; private set; }
    public bool ActiveGoal;

    public AIGoal(Transform target) { 
        Target = target;
        TargetLocation = Target.position;
        ActiveGoal = true;
    }

    public AIGoal(Vector3 position) {
        TargetLocation = position;
        Target = null;
        ActiveGoal = true;
    } 
}
