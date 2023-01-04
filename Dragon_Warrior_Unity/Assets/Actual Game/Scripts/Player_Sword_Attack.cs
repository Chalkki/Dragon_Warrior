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
                // Play the attack animation
                animator.SetTrigger("attack1");
                // there is an attack event inside the animation, as the attack animation to a good position, the attack1 function would be triggered.
                attackRate = 2f;
                nextAttackTime = Time.time + 1/attackRate;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                // Play the attack animation
                animator.SetTrigger("attack2");
                // there is an attack event inside the animation, as the attack animation to a good position, the attack1 function would be triggered.
                attackRate = 3f;
                nextAttackTime = Time.time + 1 / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                // Play the attack animation
                animator.SetTrigger("attack3");
                // there is an attack event inside the animation, as the attack animation to a good position, the attack1 function would be triggered.
                attackRate = 1f;
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }
    }

    void Attack1()
    {

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
