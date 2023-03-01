using UnityEngine;

public struct AIGoal
{
    public Transform Target { get; private set; }
    public Vector3 TargetLocation { get; private set; }
    public bool HasGoal => Target is not null || TargetLocation != Vector3.zero;

    public AIGoal(Transform target) { 
        Target = target;
        TargetLocation = Target.position;
    }

    public AIGoal(Vector3 position) {
        TargetLocation = position;
        Target = null;
    }

    public void Release() { 
        Target = null;
        TargetLocation = Vector3.zero;
    }

}
