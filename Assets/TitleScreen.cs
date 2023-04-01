using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    private void Start()
    {
        if (GameManager.Players.Count > 0) {
            Destroy(gameObject);
            return;
        }
         
        director.Play();
    }

    public void Play() {
        SceneManager.LoadScene("TestRoom005");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
