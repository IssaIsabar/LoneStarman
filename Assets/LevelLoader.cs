using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionDelay = 1f;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.levelComplete)
        {
            GameManager.Instance.levelComplete = false;
            GameManager.Instance.transitionScene = true;
            LoadNextLevel();
        }else if (GameManager.Instance.gameHasEnded)
        {
            transition.SetTrigger("GameOver");
            GameManager.Instance.EndGame();
        }

    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelAfterDelay(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevelAfterDelay(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionDelay);
        GameManager.Instance.transitionScene = false;
        SceneManager.LoadScene(levelIndex);
    }
}
