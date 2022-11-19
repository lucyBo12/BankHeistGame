using UnityEngine;

public class ShootingRangeSwitchOne : InteractableSwitch
{

    public override void Interact(Transform user)
    {
        if (isActive)
        {
            ShootingRangeOne.Instance.StartChallenge();
        }
        else {
            ShootingRangeOne.Instance.StopChallenge();
        }
        base.Interact(user);
    }

}
