using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static UIManager _instance;
    private string flashText = string.Empty;

    public GameObject pickedItem;
    public GameObject levelPopObject;
    public GameObject rapidFireImg;
    public Text scoreText;
    public Text pickedItemText;
    public Text levelPop;
    public Text speedText;
    public int speedIndex = 0;
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
        scoreText.text = "Score: " + GameManager.Instance.playerScore;
        pickedItemText.text = flashText;
        levelPop.text = "Survive";
        speedText.text = "x" + speedIndex.ToString();
    }
    public void ActivatePickedItem(string text)
    {
        flashText = text;
        pickedItem.SetActive(false);
        pickedItem.SetActive(true);
    }

}
