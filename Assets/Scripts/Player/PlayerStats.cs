using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    public float playerHealth = 5f;
    public float playerScore = 0f;


    public static PlayerStats Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Playerstats is null!");
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
}
