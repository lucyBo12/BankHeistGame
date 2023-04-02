using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField, ShowOnly] private int playersInZone = 0;
    public int requiredPlayers => GameManager.Players.Count;
    [SerializeField, ShowOnly] private float time = 0;
    public float countDown = 5;
    public string sceneName = "Game";
    public Vector3 location = Vector3.zero;
    private string lobbyTxtPrompt => $"Players: {playersInZone} / {requiredPlayers}";
    private int connectedPlayers => NetworkManager.Singleton.ConnectedClients.Count;

    public AudioSource teleporterSound;

    [Header("UI")]
    [SerializeField] private TMPro.TextMeshProUGUI lobbyTxt;


    private void Start() => lobbyTxt.text = lobbyTxtPrompt;

    private void OnEnable() => LeanTween.scaleY(gameObject, 2, 3f).setLoopPingPong();

    private void OnDisable() => LeanTween.cancel(gameObject);

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;

        playersInZone++;
        lobbyTxt.text = lobbyTxtPrompt;
    }

    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) return;

        playersInZone--;
        lobbyTxt.text = lobbyTxtPrompt;
    }

    private void FixedUpdate()
    {
        if(requiredPlayers == 0) return;
        if (playersInZone != requiredPlayers) {
            time = countDown;
            return;
        } 

        time -= Time.deltaTime;
        lobbyTxt.text = $"Starting in {time.ToString("0")} ...";
        if (time < 0) {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) { 
                player.transform.position = location;   
            }
            teleporterSound.Play();
            SceneManager.LoadScene(sceneName);
            GameManager.StartNewGame();
        }
    }

}
