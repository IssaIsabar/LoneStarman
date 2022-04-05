using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    public float speed;
    public float bossHealth = 40f;
    private Transform target;
    public HealthBar healthBar;
    private Shooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        shooting = GetComponent<Shooting>();
        healthBar.SetMaxHealth(bossHealth);
    }
    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
        healthBar.SetHealth(bossHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LaserBullet") && bossHealth > 1)
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("LaserBullet") && bossHealth == 1)
        {
            TakeDamage(1);
            Debug.Log("Level Complete");
            Destroy(this.gameObject);
            GameManager.Instance.LevelComplete();
        }

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }
}
