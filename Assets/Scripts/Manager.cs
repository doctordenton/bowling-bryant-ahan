using UnityEngine;
using TMPro;
using System.Linq;
public enum GameState { Start, Shot1, Shot2, Reset }

public class Manager : MonoBehaviour
{
    public GameState state;
    public float score = 0;
    public TMP_Text scoreText;
    public float timerTime = 3f;
    public BallForces myBall;
    public Pins[] allPins;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //   EnterStart();
        state = GameState.Start;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "score:" + score; 

        if (state == GameState.Reset)
        {
            timerTime -= Time.deltaTime;
            Debug.Log("timer began"); 

            if( timerTime < 0)
            {
                EnterStart();
                Debug.Log("reentered start");
            }
        }
    }

    public void PinHasFallen()
    {
        score++;

    }

    public void EnterStart()
    {
        myBall.ResetBall();
        for(int i = 0; i<allPins.Length; i++)
        {
            allPins[i].ResetPins();
        }
        state = GameState.Start;
        score = 0;
    }

    public void EnterReset()
    {
        Debug.Log("reset state started");
        state = GameState.Reset;

        //wait 3 seconds, 
        //
        if (timerTime > 0)
        {
            
        }
        else
        {
        //        EnterStart();
        //    Debug.Log("reentered start");
        }
    }

}
