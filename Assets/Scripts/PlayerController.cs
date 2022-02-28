using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Static instance so methods can be accessed in other scripts
    public static PlayerController instance;

    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    

    //Ground properties
    public bool isGrounded; //false by default
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public bool canDoubleJump;

    private Animator anim;
    private SpriteRenderer theSR;

    //How long the knockback lasts and the force of the knockback
    public float knockbackLength, knockbackForce;
    //Counts down from knockback length
    private float knockbackCounter;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //Player control is only given if knockback counter is less than or equal to zero

        if (knockbackCounter <= 0)
        {

            theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);//Checks if object is on ground layer 

            if (isGrounded)
            {
                canDoubleJump = true; //Double jump is possible when touching ground
            }

            if (Input.GetButtonDown("Jump"))// If jump button is pressed
            {

                if (isGrounded)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce); //Character jumps in the air
                }
                else
                {
                    if (canDoubleJump)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        canDoubleJump = false; //Stops unlimited jumping
                    }
                }

            }

            /**
             *Allows player to change direction when moving
             */
            if (theRB.velocity.x < 0)
            {
                theSR.flipX = true;
            }
            else if (theRB.velocity.x > 0)
            {
                theSR.flipX = false;
            }
        }
        else

        {
            knockbackCounter -= Time.deltaTime;
            if (!theSR.flipX)
            {
                theRB.velocity = new Vector2(-knockbackForce, theRB.velocity.y);
            }else
            {
                theRB.velocity = new Vector2(knockbackForce, theRB.velocity.y);
            }
        }
        
        anim.SetBool("isGrounded", isGrounded);//Sets id to match values
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));//Makes move speed always positive 
    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;

        theRB.velocity = new Vector2(0f, knockbackForce);

        anim.SetTrigger("hurt");
    }
}
