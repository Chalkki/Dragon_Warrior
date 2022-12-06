using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float movementX;
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 5f;
    // Start is called before the first frame update

    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D myBody;

    //the parameter for the character to run
    private string Run_Anim = "is_run";


    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
    }


    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0, 0) * moveForce * Time.deltaTime;

        if(movementX > 0)
        {
            // move right
            anim.SetBool("is_run", true);
            sr.flipX = false;
        }else if (movementX < 0)
        {
            // move left
            anim.SetBool("is_run", true);
            sr.flipX = true;
        }
        else
        {
            // stable

            anim.SetBool("is_run", false);
        }
    }
    void PlayerJump()
    {

    }
}
