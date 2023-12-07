using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private bool isImmune = false;
    private float immuneDuration = 3f;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [SerializeField] private AudioSource deathSoundEffect;
    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHeartsUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isImmune && collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth--;

        MakeImmune();
        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");

        // Restart the level after a short delay
        Invoke("RestartLevel", 2f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            hearts[i].enabled = (i < maxHealth); // Setăm vizibilitatea inimilor doar pentru prima maxHealth inimi
        }
    }

    private void MakeImmune()
    {
        isImmune = true;
        StartCoroutine(ImmunityDuration());
    }

    private IEnumerator ImmunityDuration()
    {
        yield return new WaitForSeconds(immuneDuration);
        isImmune = false;
    }
}
