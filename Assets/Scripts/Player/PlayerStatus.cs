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

    public void TakeDamage(float damage)
    {
        GameManager.Instance.playerHealth -= damage;
        UIManager.Instance.ActivatePickedItem("-1 hp");
        healthBar.SetHealth(GameManager.Instance.playerHealth);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPills"))
        {
            GameManager.Instance.playerHealth++;
            healthBar.SetHealth(GameManager.Instance.playerHealth);
            UIManager.Instance.ActivatePickedItem("+1 health");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("SpeedDrink") && playerMovement.MovementSpeed < 12)
        {
            playerMovement.MovementSpeed++;
            UIManager.Instance.ActivatePickedItem("+1 speed");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("RapidFire"))
        {
            shooting.rapidFire = true;
            UIManager.Instance.ActivatePickedItem("Rapid fire enabled");
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            GameManager.Instance.playerHealth = 0;
            healthBar.SetHealth(GameManager.Instance.playerHealth);
            StartCoroutine(FadeOut(GetComponent<SpriteRenderer>()));
            GameManager.Instance.EndGame();
            playerMovement.enabled = false;
        }
        else if (collision.gameObject.CompareTag("Enemy") && GameManager.Instance.playerHealth == 1)
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
            GameManager.Instance.EndGame();
            StartCoroutine(FadeOut(GetComponent<SpriteRenderer>()));
            playerMovement.enabled = false;
        }
        else if (collision.gameObject.CompareTag("BossLaserBullet") && GameManager.Instance.playerHealth == 1)
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
            GameManager.Instance.EndGame();
            StartCoroutine(FadeOut(GetComponent<SpriteRenderer>()));
            playerMovement.enabled = false;
        }
        else
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
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
