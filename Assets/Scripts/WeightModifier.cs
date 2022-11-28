using UnityEngine.Animations.Rigging;
using UnityEngine;

public class WeightModifier : MonoBehaviour
{
    [Header("Primary Idle")]
    public MultiAimConstraint primaryAimConstraintIdle;
    public MultiParentConstraint primaryParentConstraintIdle;
    public MultiPositionConstraint primaryPositionConstraintIdle;

    [Header("Primary Aim")]
    public MultiAimConstraint primaryAimConstraintAim;
    public MultiParentConstraint primaryParentConstraintAim;
    public MultiPositionConstraint primaryPositionConstraintAim;

    [Header("SideArm Idle")]
    public MultiAimConstraint sideAimConstraintIdle;
    public MultiParentConstraint sideParentConstraintIdle;
    public MultiPositionConstraint sidePositionConstraintIdle;

    [Header("SideArm Aim")]
    public MultiAimConstraint sideAimConstraintAim;
    public MultiParentConstraint sideParentConstraintAim;
    public MultiPositionConstraint sidePositionConstraintAim;

    public enum WeightSwitch {  
        PrimaryIdle, PrimaryAim, SideArmIdle, SideArmAim
    }

    public void SetWeight(WeightSwitch constraint, float weight) {
        switch (constraint) {
            case WeightSwitch.PrimaryIdle:
                primaryParentConstraintIdle.weight = weight;
                primaryPositionConstraintIdle.weight = weight;
                primaryAimConstraintIdle.weight = weight;
                break;
            case WeightSwitch.SideArmIdle:
                sideParentConstraintIdle.weight = weight;
                sidePositionConstraintIdle.weight = weight;
                //sideAimConstraintIdle.weight = weight;
                break;
            case WeightSwitch.PrimaryAim:
                primaryParentConstraintAim.weight = weight;
                primaryPositionConstraintAim.weight = weight;
                primaryAimConstraintAim.weight = weight;
                break;
            case WeightSwitch.SideArmAim:
                sideParentConstraintAim.weight = weight;
                sidePositionConstraintAim.weight = weight;
                sideAimConstraintAim.weight = weight;
                break;
        }
    }
}
