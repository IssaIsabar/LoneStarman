using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float currentLevel = 1f;
    private readonly float restartDelay = 5f;
    private static GameManager _instance;
    public GameObject completeLevelUI;
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
        _instance = this;
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!");
            Invoke(nameof(RestartGame), restartDelay);
        }
    }
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LevelComplete()
    {
        completeLevelUI.SetActive(true);
    }
}
