using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private Animator animator;
    float currentHealth;
    public bool canFly;
    private GameObject player;
    private Rigidbody2D rb;
    // get the sprite renderer
    private SpriteRenderer Srenderer;
    public float attackRange ;
    private float nextAttackTime = 0f;
    private float attackRate = 1f;
    public bool isAttacking = false;
    public float attackDamage;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform attackPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator= GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Srenderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(player.transform.position, attackPoint.position);
/*        if (!canFly)
        {
            // if the player is on the ladder 
            bool isLadder = player.GetComponent<Player>().isLadder;
            if (isLadder)
            {
                return;
            }

        }*/
        if (distance <= attackRange && Time.time >= nextAttackTime) {
            isAttacking = true;
            animator.SetTrigger("attacking");
            rb.velocity = new Vector2(0f, 0f);
            nextAttackTime = Time.time + 1/attackRate;
        }
    }

    public void finishattacking()
    {
        isAttacking = false;
    }
    public void TakeDamage(float damage, bool attackFromLeft, float impulse)
    {
        // return if the current object is already dead
        if (currentHealth <= 0)
        {
            return;
        }
        // change the color of the sprite
        Color oldColor = Srenderer.color;
        StartCoroutine(AttackResponse(oldColor));
        // let the enemy being stopped first
        Vector2 orginalv = rb.velocity;
        StartCoroutine(AttackImpulse(attackFromLeft,orginalv, impulse));
        currentHealth -= damage;
        GetComponent<Enemy_Chase>().Being_attacked();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator AttackResponse(Color oldColor)
    {
        // change sprite color to red
        Srenderer.color= Color.red;
        yield return new WaitForSeconds(0.5f);
        Srenderer.color =oldColor;
    }
    
    IEnumerator AttackImpulse(bool attackFromLeft, Vector2 originalv, float impulse)
    {
        if (attackFromLeft)
        {
            rb.velocity = new Vector2(-impulse,rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-impulse, rb.velocity.y);
        }
        yield return new WaitForSeconds(0.5f);
        rb.velocity = originalv;
    }
    void Attack()
    {
        // return if the current object is already dead
        if(currentHealth <= 0) 
        {
            return;
        }
        Collider2D []hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D player_ in hitPlayer)
        {
            player_.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
    void Die()
    {
        Debug.Log("Enemy die and health is " + currentHealth);
        animator.SetTrigger("die");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Enemy_Patrol>().enabled = false;
        GetComponent<Enemy_Chase>().enabled = false;
        GetComponent<Enemy>().enabled = false;
        StartCoroutine(waitToDestory());
        //this.enabled = false;
    }

    IEnumerator waitToDestory()
    {
        // wait for five seconds before destorying the object
        yield return new WaitForSeconds(5);
        // destory the object
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
