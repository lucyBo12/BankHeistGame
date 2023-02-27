using UnityEngine.UI;
using UnityEngine;

public class WantedGUI : MonoBehaviour
{
    public Image badge;
    public Image badgeFill;
    private int prevWanted;



    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            GameManager.WantedLevel = GameManager.WantedLevel >= 3 ? 0 : GameManager.WantedLevel +0.1f;
            WantedColor();
        }
    }

    public void WantedColor()
    {
        int wantedLevel = (int)GameManager.WantedLevel;
        float fillAmount = GameManager.WantedLevel - wantedLevel;
        badgeFill.fillAmount = fillAmount;
        if (wantedLevel == prevWanted) return;
        switch (wantedLevel)
        {
            case 0:
                badge.CrossFadeColor(Color.white,1f,false, true);
                break;
            case 1:
                badge.CrossFadeColor(Color.yellow, 1f, false, true);
                break;
            case 2:
                badge.CrossFadeColor(new Color(1f, 0.5f, 0f), 1f, false, true);
                break;
            case 3:
                badge.CrossFadeColor(Color.red, 1f, false, true);
                break;

        }


        LeanTween.scale(badge.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 1f).setEaseInBounce().setOnComplete(() => { LeanTween.scale(badge.gameObject, Vector3.one, 0.5f); });
        prevWanted = wantedLevel;
    }
}
