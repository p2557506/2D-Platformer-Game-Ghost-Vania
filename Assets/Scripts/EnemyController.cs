using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour

    
{
    public static EnemyController instance;

    //Speed enemy moves
    public float moveSpeed;

    //Points that enemy moves between
    public Transform leftPoint, rightPoint;

    //Lets me know if player is moving towards left or right
    private bool movingRight;

    private Rigidbody2D theRB;
    // Start is called before the first frame update

    public SpriteRenderer theSR;

    private Animator anim;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Left point does not have parent when game starts
        leftPoint.parent = null;

        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;
            //Actually moving enemy
            if (movingRight)
            {

                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

                theSR.flipX = true;

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;//Stops movement to the right
                }
            }
            else
            {

                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                theSR.flipX = false;

                if (transform.position.x < leftPoint.position.x)//If enemy passes left point
                {
                    //Enemy starts moving right
                    movingRight = true;
                }


            }

            if(moveCount <= 0)//Countdown has reached 0 or below
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.75f);//Chooses random wait time
            }
            //Animation plays when enemy moves
            anim.SetBool("isMoving", true);
        }else if(waitCount > 0)//Counter still above 0
        {
            waitCount -= Time.deltaTime;//Counter counts down
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            if(waitCount <= 0)//If wait time has ended
            {
                moveCount = Random.Range(moveTime * .75f, moveTime * 1.75f);
            }
            //Animation stops playing when false
            anim.SetBool("isMoving", false);
        }
    }
}
