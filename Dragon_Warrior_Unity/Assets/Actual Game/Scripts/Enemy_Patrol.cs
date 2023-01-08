using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{

    Rigidbody2D rb;
    private Animator animator;
    public float patrolSpeed = 2.0f;
    public bool is_Patrol = true;
    public bool facingRight = true;
    //private Vector3 baseScale;
    [SerializeField]
    private Transform castTrans;
    [SerializeField]
    private float baseCastDist;
    private bool is_chasing;
    // Start is called before the first frame update
    void Start()
    {
        //baseScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        is_chasing = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (GetComponent<Enemy>().isAttacking)
        {
            return;
        }

        // detect whether the enemy spotted the player and try to chase the player
        if (is_chasing)
        {
            is_Patrol = false;
        }
        else
        {
            is_Patrol = true;
        }

        if (is_Patrol)
        {
            float vX = patrolSpeed;
            if (!facingRight)
            {
                vX = -patrolSpeed;
            }
            rb.velocity = new Vector2(vX, rb.velocity.y);
            animator.SetBool("is_patrol", true);
            if (IsHittingWallOrEdge())
            {
                ChangeFacingDirection();
            }
        }
        else
        {
            animator.SetBool("is_patrol", false);
        }
    }
    
    bool IsHittingWallOrEdge()
    {
        bool hit = false;
        float castDist = baseCastDist;
        // Check if hitting wall
        if (!facingRight)
        {
            castDist = -baseCastDist;
        }
        // determine the target destination (the wall edge that touches with the tip of the ray) based on the cast distance
        Vector3 targetPos = castTrans.position;
        targetPos.x += castDist;

        Debug.DrawLine(castTrans.position, targetPos, Color.blue);
        if (Physics2D.Linecast(castTrans.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            hit = true;
            return hit;
        }

        // Check if hitting edge
        targetPos = castTrans.position;
        targetPos.y = targetPos.y - baseCastDist;
        if (Physics2D.Linecast(castTrans.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            hit = false;
        }
        else
        {
            hit = true;
        }
        return hit;
    }

    public void detect_chasing(bool is_chasing, bool facingRight)
    {
        this.is_chasing = is_chasing;
        this.facingRight = facingRight;
    }

    void ChangeFacingDirection()
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
}
