using UnityEngine;
using UnityEngine.Playables;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        if (GameManager.Players.Count > 0) {
            Destroy(gameObject);
            return;
        }
         
        director.Play();
        //audioSource.Play();
    }
}
