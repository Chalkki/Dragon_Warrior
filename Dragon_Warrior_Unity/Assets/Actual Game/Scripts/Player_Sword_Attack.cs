using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sword_Attack : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRange = 0.5f;
    [SerializeField]
    private LayerMask enemyLayers;
    [SerializeField]
    private int playerDamage = 20;
    //set up the attackRate
    [SerializeField]
    private float attackRate = 2f;
    private float nextAttackTime = 0f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack1();
                attackRate = 2f;
                nextAttackTime = Time.time + 1/attackRate;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Attack2();
                attackRate = 3f;
                nextAttackTime = Time.time + 1 / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Attack3();
                attackRate = 1f;
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }
    }

    void Attack1()
    {
        // Play an attack animation
        animator.SetTrigger("attack1");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        playerDamage = 20;
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(playerDamage);
        }
    }
    void Attack2()
    {
        animator.SetTrigger("attack2");
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        playerDamage = 10;
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(playerDamage);
        }

    }
    void Attack3()
    {
        animator.SetTrigger("attack3");
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        playerDamage = 30;
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(playerDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
