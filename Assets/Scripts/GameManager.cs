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

    /**
     * Uses "RuntimeInitializeOnLoadMethod" to run game setup
     * logic.
     */
    [RuntimeInitializeOnLoadMethod]
    private static void Initialize() {
        Input = new InputMaster();    
    }

}
