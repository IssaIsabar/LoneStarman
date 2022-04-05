using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject healthPill;
    [SerializeField] private GameObject rapidFire;
    [SerializeField] private GameObject speed;
    private GameObject item;

    private GameObject newItem;
    private Vector3 spawnPosition;
    private int chance = 0;
    private static ItemSpawner _instance;
    public static ItemSpawner Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Itemspawn is null!");
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public void SpawnNewItem(float x, float y)
    {
        spawnPosition = new Vector3(x, y, 0f);

        chance = Random.Range(0, 10);
        if (chance <= 1)
            item = healthPill;
        else if (chance > 1 && chance <= 3)
            item = speed;
        else if (chance > 5 && chance <= 7)
            item = rapidFire;
        else
            item = null;

        if(item != null)
            newItem = Instantiate(item, spawnPosition, Quaternion.identity);
    }
}
