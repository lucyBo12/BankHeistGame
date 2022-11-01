using System.Collections.Generic;
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
    public static bool InCombat { get; private set; }
    public static List<Transform> Players = new List<Transform>();
    public static List<Transform> Alarms = new List<Transform>();

    /**
     * Uses "RuntimeInitializeOnLoadMethod" to run game setup
     * logic.
     */
    [RuntimeInitializeOnLoadMethod]
    private static void Initialize() {
        Input = new InputMaster();    
    }

    public static void StartCombat() => InCombat = true;

}
