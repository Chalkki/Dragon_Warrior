using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chase : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody2D rb;
    float chaseSpeed;
    bool facingRight;
    // Initialize is_chasing to false at first
    public bool is_chasing = false;
    // The look range for the enemy
    public float lookRadius = 5f;
    // the center point of model of the enemy
    public Transform sprite_center_point;
    bool being_attacked;
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        chaseSpeed = GetComponent<Enemy_Patrol>().patrolSpeed * 2;
        rb = GetComponent<Rigidbody2D>();
        facingRight = GetComponent<Enemy_Patrol>().facingRight;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        facingRight = GetComponent<Enemy_Patrol>().facingRight;
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        float x_direction = Mathf.Sign(player.transform.position.x - transform.position.x);
        if (distance <= lookRadius)
        {
            if (facingRight && x_direction == 1)
            {
                is_chasing = true;
            }
            else if (!facingRight && x_direction == -1)
            {
                is_chasing = true;
            }
            if (being_attacked)
            {
                is_chasing = true;
                being_attacked = false;
            }
        }
        else
        {
            is_chasing = false;
            animator.SetBool("is_chasing", false);
        }
        if (is_chasing)
        {
            animator.SetBool("is_chasing", true);
            if (facingRight && x_direction == -1)
            {
                ChangeFacingDirection();
            }
            else if (!facingRight && x_direction == 1)
            {
                ChangeFacingDirection();
            }
            float vX = x_direction * chaseSpeed;
            float vY = rb.velocity.y;
            rb.velocity = new Vector2(vX, vY);
        }
        GetComponent<Enemy_Patrol>().detect_chasing(is_chasing, facingRight);
    }

    public void Being_attacked()
    {
        being_attacked = true;
    }
    private void ChangeFacingDirection()
    {
        if (facingRight)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
        transform.Rotate(0f, 180f, 0f);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(sprite_center_point.position, lookRadius);
    }
}
