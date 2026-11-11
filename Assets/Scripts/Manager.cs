using UnityEngine;
using TMPro;
public enum GameState { Start, Shot1, Shot2, Reset }

public class Manager : MonoBehaviour
{
    public GameState state;
    public float score = 0;
    public TMP_Text scoreText;
    public float timerTime = 3f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnterStart(); 
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "score:" + score; 
    }

    public void PinHasFallen()
    {
        score++;

    }

    public void EnterStart()
    {
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
            timerTime -= Time.deltaTime;
            Debug.Log("timer began");
        }
        else
        {
                EnterStart();
            Debug.Log("reentered start");
        }
    }

}
