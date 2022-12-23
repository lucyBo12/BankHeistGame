using UnityEngine.Animations.Rigging;
using UnityEngine;

public class WeightModifier : MonoBehaviour
{
    [Header("Primary")]
    public Rig primaryIdle;
    public Rig primaryAim;

    [Header("SideArm")]
    public Rig sideArmAim;
    public Rig sideArmIdle;


    // The speed at which we want to lerp the weight value
    public float lerpSpeed = 1.0f;

    // The current weight value of the rig
    private float currentWeight = 0.0f;

    public void SetWeight(Weapon.WeaponType weapon, bool isAiming)
    {
        if (weapon == Weapon.WeaponType.primary) {
            sideArmAim.weight = 0;
            sideArmIdle.weight = 0;
        }

        // Calculate the new weight value by lerping from the current weight to the target weight
        currentWeight = Mathf.Lerp(currentWeight, isAiming ? 1.0f : 0.0f, lerpSpeed * Time.deltaTime);
        if (Mathf.Abs(currentWeight - (isAiming ? 1.0f : 0.0f)) < 0.1f) {
            currentWeight = (isAiming ? 1.0f : 0.0f);
        }

        //var rig = weapon == Weapon.WeaponType.primary ? (isAiming ? primaryAim : primaryIdle) : sideArmIdle;
        sideArmAim.weight = currentWeight;
    }

}
