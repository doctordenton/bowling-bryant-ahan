using UnityEngine;

public class Gutter : MonoBehaviour
{
    public GameManager gameManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        if (other.tag == "Ball")
        {
            gameManager.GutterBall();
        }
    }
}
