using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScoreBoard : MonoBehaviour
{
    [SerializeField]private GameObject panel1, panel2, panel3;
    public int score;


    [ContextMenu("Test Score")]
    public void UpdateScore() {
        string line = score.ToString();
        Debug.Log(line);
        for (int i = 2; i >= 0; i--) {
            GameObject panel = i == 0 ? panel1 : 
                i == 1 ? panel2 : panel3;

            int val = i < line.Length ? int.Parse(line[line.Length - (i + 1)].ToString()) : 0;
            Show(panel, val);
        }
    }

    [ContextMenu("Test Fail")]
    public void ShowFail() {
        Show(panel1, panel1.transform.childCount - 1);
        Show(panel2, panel2.transform.childCount - 1);
        Show(panel3, panel3.transform.childCount - 1);
    }

    private void Show(GameObject panel, int child) {
        for (int i = 0; i < panel.transform.childCount; i++) {
            GameObject childObj = panel.transform.GetChild(i).gameObject;
            childObj.SetActive(i == child);
        }

        var num = panel.transform.GetChild(child).gameObject;
        num.transform.localScale = new Vector3(-1, 1, 1);

        LeanTween.cancel(num);
        LeanTween.scale(num, new Vector3(-1.2f, 1.2f, 1f), 1f).setEaseInOutElastic();
    }

}
