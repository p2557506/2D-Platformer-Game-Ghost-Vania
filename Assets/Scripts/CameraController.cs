using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Transform farBackground, nearFarBackground, middleBackgroundB, middleBackgroundA, nearClose, close;

    private float lastXPos;

    private float lastYPos;

    public float minHeight, maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        lastXPos = transform.position.x;
        lastYPos = transform.position.y;//This is where camera starts
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Camera follows player on x axis with target
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        //makes sure transform.positon.y possible minimum or max value can only be equal to either minHeight or maxHeight
        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);*/

        //Cam follows player on x-axis and y-axis and camera is given boundaries on y-axis as to not expose too much of the environment
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

        //Update code to single variable amount to move with a vector 2
        float amountToMoveX = transform.position.x - lastXPos;
        float amountToMoveY = transform.position.y - lastYPos;

        farBackground.position = farBackground.position + new Vector3(amountToMoveX, amountToMoveY, 0f);
        nearFarBackground.position += new Vector3(amountToMoveX * 0.83f, amountToMoveY * 0.83f, 0f);
        middleBackgroundA.position += new Vector3(amountToMoveX * 0.67f, amountToMoveY * 0.67f, 0f);
        middleBackgroundB.position += new Vector3(amountToMoveX * 0.5f, amountToMoveY * 0.5f, 0f);
        nearClose.position += new Vector3(amountToMoveX * 0.33f, amountToMoveY * 0.33f, 0f);
        close.position += new Vector3(amountToMoveX * 0.17f, amountToMoveY * 0.17f, 0f);

        lastXPos = transform.position.x;
        lastYPos = transform.position.y;
        //farBackground.position = farBackground.position + new Vector3(amountToMoveX, 0f, 0f);
       
        //lastXPos = transform.position.x;
    }
}
