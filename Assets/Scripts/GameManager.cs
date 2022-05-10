using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerScore = 0;
    public float playerHealth = 5;
    public bool levelComplete = false;
    public bool transitionScene = false;
    public bool bossCollision = false;
    public bool gameHasEnded = false;
    
    private readonly float restartDelay = 4f;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is null!");
            return _instance;
        }
    }
    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void EndGame()
    {
        gameHasEnded = true;
        Invoke(nameof(RestartGame), restartDelay);
    }
    void RestartGame()
    {
        SceneManager.LoadScene(1);
        playerHealth = 10;
        playerScore = 0;
    }
    public void LevelComplete()
    {
        levelComplete = true;
    }

}
