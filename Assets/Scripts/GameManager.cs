using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float delay = 4f;
    bool gameHasEnded = false;
    private readonly float restartDelay = 4f;
    private static GameManager _instance;
    public int levelIndex = 1;
    public GameObject completeLevelUI;
    public GameObject gameOverUI;
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
        }else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
    }
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            gameOverUI.SetActive(true);
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
        levelIndex++;
        StartCoroutine(LoadLevelAfterDelay(levelIndex));
    }
    IEnumerator LoadLevelAfterDelay(int newlevel)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(newlevel);
    }
}
