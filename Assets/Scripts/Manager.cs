using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{

    public float score = 0;
    public TMP_Text scoreText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

}
