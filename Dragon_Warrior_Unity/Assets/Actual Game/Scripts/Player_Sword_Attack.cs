using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_Sword_Attack : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRange = 0.8f;
    [SerializeField]
    private LayerMask enemyLayers;
    public LayerMask BreakableLayers;
    [SerializeField]
    private float playerDamage = 20;
    //set up the attackRate
    [SerializeField]
    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    public Slider energyBar;
    public float maxEnergy = 100f;
    public float currentEnergy;
    // initialize the sword level
    private int SwordLevel = 1;
    private float DamageMultiplier = 1;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;
        setMaxEnergy(maxEnergy);
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
                attackRate = 3f;
                nextAttackTime = Time.time + 1/attackRate;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                // decrease energy by 20, if not enough energy, the player cannot use this attack method
                if (currentEnergy >= 20f)
                {
                    currentEnergy -= 20f;
                }
                else
                {
                    return;
                }
                setEnergy(currentEnergy);
                // Play the attack animation
                animator.SetTrigger("attack2");
                // there is an attack event inside the animation, as the attack animation to a good position, the attack1 function would be triggered.
                attackRate = 5f;
                nextAttackTime = Time.time + 1 / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                // decrease energy by 50, if not enough energy, the player cannot use this attack method
                if (currentEnergy >= 50f)
                {
                    currentEnergy -= 50f;
                }
                else
                {
                    return;
                }
                setEnergy(currentEnergy);
                // Play the attack animation
                animator.SetTrigger("attack3");
                // there is an attack event inside the animation, as the attack animation to a good position, the attack1 function would be triggered.
                attackRate = 2f;
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }
    }
    public void SetSwordLevel(int level)
    {
        SwordLevel = level;
        Debug.Log("The sword level is " + SwordLevel);
        DamageMultiplier = Mathf.Pow(1.2f, SwordLevel - 1);
    }

    public int GetSwordLevel()
    {
        return SwordLevel;
    }
    void setMaxEnergy(float energy)
    {
        //set max energy for slider
        energyBar.maxValue = energy;
        energyBar.value = energy;
    }

    void setEnergy(float energy)
    {
        // set energy for slider
        energyBar.value = energy;

    }
    void Attack1()
    {
        attackRange = 0.8f;
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage them
        playerDamage = 20f * DamageMultiplier;
        // impulse that the enemy would have after being attacked
        float impulse = 2f;
        foreach (Collider2D enemy in hitEnemies)
        {
            // increase energy by 10
            currentEnergy += 10;
            if (currentEnergy > maxEnergy)
            {
                currentEnergy = maxEnergy;
            }
            setEnergy(currentEnergy);
            bool attackFromLeft = isAttackFromLeft(transform, enemy.transform);
            enemy.GetComponent<Enemy>().TakeDamage(playerDamage, attackFromLeft, impulse);
        }
        hitBreakable();
    }
    void Attack2()
    {
        attackRange = 0.6f;
        playerDamage = 15 * DamageMultiplier;
        float impulse = 3f;
        hitBreakable();
        hitEnemy(impulse);

    }
    void Attack3()
    {
        // increase the attack range temporarily
        attackRange = 1.2f;
        playerDamage = 50 * DamageMultiplier;
        float impulse = 4f;
        hitBreakable();
        hitEnemy(impulse);
    }

    void hitEnemy(float impulse)
    {
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            bool attackFromLeft = isAttackFromLeft(transform, enemy.transform);
            enemy.GetComponent<Enemy>().TakeDamage(playerDamage, attackFromLeft, impulse);
        }
    }
    void hitBreakable()
    {
        Collider2D[] hitBreakables = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, BreakableLayers);
        foreach (Collider2D breakable in hitBreakables)
        {
            Destroy(breakable.gameObject);
        }
    }
    bool isAttackFromLeft(Transform player, Transform enemyTrans)
    {
        bool attackFromLeft;
        if (Mathf.Sign(player.position.x - enemyTrans.position.x) < 0)
        {
            attackFromLeft = true;
        }
        else
        {
            attackFromLeft = false;
        }
        return attackFromLeft;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
