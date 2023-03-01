using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Static class for use as a global hub for necessary runtime
 * logic on local machine.
 * 
 * 
 * author: Joseph Denby 
 * email: jd744@kent.ac.uk
 */
public static class GameManager
{
    public static InputMaster Input { get; private set; }
    public static GameState State { get; private set; }
    public static bool InCombat => WantedLevel > 0;
    [Range(0f, 5f)] public static float WantedLevel;
    public static List<Transform> Players = new List<Transform>();
    public static List<Character> Characters => Players.Select(x => x.GetComponent<Character>()).ToList();
    
    public static List<Room> Rooms = new List<Room>();

   

    public static Transform Exit;

    /**
     * Uses "RuntimeInitializeOnLoadMethod" to run game setup
     * logic.
     */
    [RuntimeInitializeOnLoadMethod]
    private static void Initialize() {
        Input = new InputMaster();    
    }

    public static Room GetRoom(GameObject other) {
        foreach (Room room in Rooms) { 
            if(room.HasInhibtant(other)) return room;
        
        }
        return null;
    }

    public static void StartNewGame() {

        Characters.ForEach(x => { 
            x.ResetCharacter(); 
        });
        WantedLevel = 0;
        State = GameState.Active;
    }

    public static void CheckGameState() {
        if (Characters.All(x => x.dead)) {
            State = GameState.Failed;
        }
        else if (Characters.All(x => x.safe)) {
            State = GameState.Success;
        }
        State = GameState.Active;
    }
    
}
