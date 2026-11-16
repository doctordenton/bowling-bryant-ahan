using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public TMP_Text scoreText;
    private int score = 0;
    public GameObject[] pins;
    public GameObject ball;
    //bool gameEnded = false; //we don't need this, I am not aiming for lvl 10 :')
    
    private Vector3[] pinStartPositions;
    private Quaternion[] pinStartRotations; //the pins will spawn in fallen down otherwise lol
    private Rigidbody[] pinRB;
    
    private Vector3 ballStartPosition;
    private Quaternion ballStartRotation;
    private Rigidbody ballRB;
    
    bool resetStarted = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pinStartPositions = new Vector3[pins.Length];
        pinStartRotations = new Quaternion[pins.Length];
        pinRB = new Rigidbody[pins.Length];
        
        ballStartPosition = ball.transform.position;
        ballStartRotation = ball.transform.rotation; //it's a ball. Idc how it's rotated, but the
        ballRB = ball.GetComponent<Rigidbody>();

        for (int i = 0; i < pins.Length; i++)
        {
            pinStartPositions[i] = pins[i].transform.position; //get every pin's starting position etc
            pinStartRotations[i] = pins[i].transform.rotation; 
            pinRB[i] = pins[i].GetComponent<Rigidbody>(); 
        }   
    }

    // Update is called once per frame
    void Update()
    {
        pinsDown();
        // if (gameEnded)
        // {
        //     Invoke(ResetObjects, 10); //ok this isn't as easy as I thought
        // }
        //
        // scoreText.text = score.ToString(); 
    }


    void pinsDown()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            if (Quaternion.Angle(pins[i].transform.rotation, pinStartRotations[i]) > 90f
                && pins[i].activeSelf) //Quaternion.Angle(a, b) checks the difference between two angles (obv had to look this up)
            {
                score++;
                scoreText.text = score.ToString();
                pins[i].SetActive(false); //so it doesn't count itself indefinitely     
               
                if (!resetStarted)
                {
                    resetStarted = true;
                    StartCoroutine(TriggerReset()); //coroutine needs to be ienumerator
                }
            }
        }
    }
    
    //  HEY KARINA so I mostly do get how to do this (it is taking me a while) 
    // however I did a two-day game jam over the weekend and didn't manage my time very well
    // so I'm submitting this for now. Sorry for the delay! :'

    IEnumerator TriggerReset()   //IEnumerator is the kind of Coroutine we need to use
    {
        yield return new WaitForSeconds(5); //this is how to write a timer basically
        ResetObjects();
        resetStarted = false;
    }

    void ResetObjects()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].SetActive(true); //because I set them to false when they are counted
            ResetObject(pins[i], pinRB[i], pinStartPositions[i], pinStartRotations[i]); //separately calling a Reset object fn 
        }

        ResetBall();
        //ResetObject(ball, ballRB, ballStartPosition, ballStartRotation); //I need to call this function when the ball goes to the gutter
    }

    void ResetBall()
    {
        ResetObject(ball, ballRB, ballStartPosition, ballStartRotation);
    }
    
    void ResetObject(GameObject gameObject, Rigidbody rb, Vector3 startPosition, Quaternion startRotation)
    {
        rb.linearVelocity = Vector3.zero; //we're setting all the reset parameters
        rb.angularVelocity = Vector3.zero;
        rb.position = startPosition;
        rb.rotation = startRotation;
    }

    public void GutterBall()
    {
        if (!resetStarted)
        {
            ResetBall();
        }
    }
}
