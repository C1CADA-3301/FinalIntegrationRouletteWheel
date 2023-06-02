using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i 
    { get 
        { // Using it instantiate as a global place to store data and use it anywhere required
            if(_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i; 
        } 
    }
    // Placeholder where all the assets reference are taken 
    public GameObject placedCoinChip;
}
