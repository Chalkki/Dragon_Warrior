using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public Slider slider;
    float currentHealth;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
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
        currentHealth -= damage;
        if(currentHealth <= 0) {
            SetHealth(0f);
            Die();
            return;
        }
        SetHealth(currentHealth);
    }

    void Die()
    {
        animator.SetTrigger("is_dead");
        MonoBehaviour[] monos = GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour mono in monos)
        {
            mono.enabled = false;
        }
    }
}
