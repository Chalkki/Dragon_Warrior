using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private Animator animator;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GetComponent<Enemy_Chase>().Being_attacked();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Attack()
    {

    }
    void Die()
    {
        Debug.Log("Enemy die and health is " + currentHealth);
        animator.SetTrigger("die");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Enemy_Patrol>().enabled = false;
        GetComponent<Enemy_Chase>().enabled = false;
        this.enabled = false;
    }
}
