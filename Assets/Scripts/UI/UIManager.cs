using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pickedItem;
    private static UIManager _instance;
    private string flashText = string.Empty;
    public Text scoreText;
    public Text levelText;
    public Text pickedItemText;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UIManager is null!");
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + PlayerStats.Instance.playerScore.ToString();
        levelText.text = "Level: " + GameManager.Instance.currentLevel.ToString();
        pickedItemText.text = flashText;
    }
    public void ActivatePickedItem(string text)
    {
        flashText = text;
        pickedItem.SetActive(false);
        pickedItem.SetActive(true);
    }
}
