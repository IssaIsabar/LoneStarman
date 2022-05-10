using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public HealthBar healthBar;
    private readonly float fadeOutTime = 1f;
    private PlayerMovement playerMovement;
    private Shooting shooting;
    // Start is called before the first frame update
    void Start()
    {
        shooting = GetComponent<Shooting>();
        playerMovement = GetComponent<PlayerMovement>();
        healthBar.SetMaxHealth(GameManager.Instance.playerHealth);
    }
    void Update()
    {
        healthBar.SetHealth(GameManager.Instance.playerHealth);
    }

    public void TakeDamage(float damage)
    {
        GameManager.Instance.playerHealth -= damage;
        UIManager.Instance.ActivatePickedItem("-1 hp");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPills"))
        {
            if (GameManager.Instance.playerHealth < 10)
                GameManager.Instance.playerHealth++;
            UIManager.Instance.ActivatePickedItem("+1 health");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("SpeedDrink") && playerMovement.MovementSpeed < 12)
        {
            playerMovement.MovementSpeed++;
            UIManager.Instance.speedIndex++;
            UIManager.Instance.ActivatePickedItem("+1 speed");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("RapidFire"))
        {
            shooting.rapidFire = true;
            UIManager.Instance.rapidFireImg.SetActive(true);
            UIManager.Instance.ActivatePickedItem("Rapid fire enabled");
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("BigBoss") || collision.gameObject.CompareTag("BiggestBoss"))
        {
            GameManager.Instance.playerHealth = 0;
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BossLaserBullet") && GameManager.Instance.playerHealth > 1)
        {
            TakeDamage(1);
            Destroy(collision.gameObject);

        }
        if (GameManager.Instance.playerHealth <= 0)
        {
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            StartCoroutine(FadeOut(GetComponent<SpriteRenderer>()));
            playerMovement.enabled = false;
            GameManager.Instance.EndGame();
        }
    }

    IEnumerator FadeOut(SpriteRenderer sr)
    {
        Color tmpColor = sr.color;

        while (tmpColor.a > 0f)
        {
            tmpColor.a -= Time.deltaTime / fadeOutTime;
            sr.color = tmpColor;

            if (tmpColor.a <= 0f)
                tmpColor.a = 0.0f;


            yield return null;
        }

        sr.color = tmpColor;
    }

}
