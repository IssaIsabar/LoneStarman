using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject player;

    private float repeatRate = 9f;
    private int bossAmount = 0;
    private int alternateInteger = 0;
    private float randomXPosition, randomYPosition;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawner), 0f, 30);
        player.GetComponent<Transform>();
    }
    private void Spawner()
    {
        for (int i = 0; i < bossAmount; i++)
        {
            Invoke(nameof(SpawnBoss), 0f);
        }
        InvokeRepeating(nameof(SpawnNewEnemy), 0f, repeatRate);
        IncreaseDifficulty();
    }

    private void IncreaseDifficulty()
    {
        if (repeatRate != 1)
            repeatRate--;
        bossAmount++;
    }
    private void SpawnBoss()
    {
        Instantiate(boss, GetCoordinates(), Quaternion.identity);
    }
    private void SpawnBiggerBoss()
    {
        Instantiate(boss, GetCoordinates(), Quaternion.identity);
    }
    private void SpawnNewEnemy()
    {
        Instantiate(enemy, GetCoordinates(), Quaternion.identity);
    }
    private Vector3 GetCoordinates()
    {
        if (alternateInteger < 3)
            alternateInteger++;
        else
            alternateInteger = 0;

        float pX = player.transform.position.x;
        float pY = player.transform.position.y;

        switch (alternateInteger)
        {
            case 0:
                randomXPosition = Random.Range(pX - 25f, pX - 24f);
                randomYPosition = Random.Range(pY - 23f, pY - 23f);
                break;
            case 1:
                randomXPosition = Random.Range(pX - 25f, pX - 25f);
                randomYPosition = Random.Range(pY - 23f, pY - 24f);
                break;
            case 2:
                randomXPosition = Random.Range(pX + 24f, pX + 25f);
                randomYPosition = Random.Range(pY - 18f, pY + 18f);
                break;
            case 3:
                randomXPosition = Random.Range(pX - 20f, pX + 20f);
                randomYPosition = Random.Range(pY + 17f, pY + 18f);
                break;
        }

        return new Vector3(randomXPosition, randomYPosition, 0f);
    }
}
