using UnityEngine.Animations.Rigging;
using UnityEngine;

public class WeightModifier : MonoBehaviour
{
    [Header("IKRef")]
    public IKRef primary = new();
    public IKRef sideArm = new();

    [Header("Constraint")]
    public TwoBoneIKConstraint leftIK;
    public TwoBoneIKConstraint rightIK;



    // The speed at which we want to lerp the weight value
    public float lerpSpeed = 1.0f;

    // The current weight value of the rig
    private float currentWeight = 0.0f;

    public void SetWeight(Weapon.WeaponType weapon, bool isAiming)
    {
        IKRef ik = weapon == Weapon.WeaponType.primary ? primary : sideArm;

        if (weapon == Weapon.WeaponType.primary) {
            sideArm.aim.weight = 0;
            sideArm.idle.weight = 0;
        }

        // Calculate the new weight value by lerping from the current weight to the target weight
        currentWeight = Mathf.Lerp(currentWeight, isAiming ? 1.0f : 0.0f, lerpSpeed * Time.deltaTime);
        if (Mathf.Abs(currentWeight - (isAiming ? 1.0f : 0.0f)) < 0.1f) {
            currentWeight = (isAiming ? 1.0f : 0.0f);
        }

        
    }


    [System.Serializable]
    public class IKRef {
        public Rig idle;
        public Rig aim;

        [Header("Right Hand")]
        public Transform rightTarget;
        public Transform rightHint;

        [Header("Left Hand")]
        public Transform leftTarget;
        public Transform leftHint;
    }
}
