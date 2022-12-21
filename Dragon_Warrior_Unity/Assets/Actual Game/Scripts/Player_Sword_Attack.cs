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
    private int playDamage = 20;
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
        if (Input.GetKeyDown(KeyCode.J)) {
            Attack1();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack2();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Attack3();
        }
    }

    void Attack1()
    {
        // Play an attack animation
        animator.SetTrigger("attack1");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(playDamage);
        }
    }
    void Attack2()
    {
        animator.SetTrigger("attack2");
    }
    void Attack3()
    {
        animator.SetTrigger("attack3");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
