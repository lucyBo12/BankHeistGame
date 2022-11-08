using UnityEngine;

public class ShootingRangeSwitchOne : InteractableSwitch
{

    public override void Interact()
    {
        base.Interact();
        if (isActive)
        {
            ShootingRangeOne.Instance.StartChallenge();
        }
        else {
            ShootingRangeOne.Instance.StopChallenge();
        }
    }

}
