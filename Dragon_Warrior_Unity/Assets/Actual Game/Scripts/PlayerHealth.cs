using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public Slider slider;
    float currentHealth;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer Srenderer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Srenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health; 
        slider.value = health;
    }
    public void TakeDamage(float damage)
    {
        if(currentHealth<= 0)
        {
            return;
        }
        // change the color of the sprite
        Color oldColor = Srenderer.color;
        StartCoroutine(AttackResponse(oldColor));
        currentHealth -= damage;
        if(currentHealth <= 0) {
            SetHealth(0f);
            Die();
            return;
        }
        SetHealth(currentHealth);
    }

    IEnumerator AttackResponse(Color oldColor)
    {
        // change sprite color to red
        Srenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        Srenderer.color = oldColor;
    }
    void Die()
    {
        animator.SetTrigger("is_dead");
        MonoBehaviour[] monos = GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour mono in monos)
        {
            mono.enabled = false;
        }
        Destroy(rb);
    }
}
