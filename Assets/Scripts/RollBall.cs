using UnityEngine;
using UnityEngine.InputSystem;


public class RollBall : MonoBehaviour
{
    Rigidbody rigidBody;

    private InputAction bowlButt;

    private bool holdingButt = true;
    private bool roll;

    public float rollPower;
    public LayerMask personLayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        bowlButt = InputSystem.actions.FindAction("Bowl");
        //rigidBody.AddForce(transform.forward * 1000f);
    }

    
    void Update()
    {
        if (bowlButt.WasPressedThisFrame())
        {
            //rigidBody.AddForce(transform.forward * 1000f);
            //StartRoll();
        } else if (bowlButt.WasReleasedThisFrame())
        {
            holdingButt = false;
            roll = true;
        }
    }

    void FixedUpdate()
    {
        if (holdingButt && rigidBody.linearVelocity == Vector3.zero) //if I am still
        {
            //find the direction of mouse 
            Debug.DrawRay(transform.position, FindDirection()*1000f , Color.red); //Find Direction * 1000f is just showing how long the line is
        }

        if (roll)
        {
            StartRoll(); //in the mouse direction
        }
    }
    
    void StartRoll()
    {
        //rigidBody.AddForce(transform.forward * 1000f);
        //if (Physics.Raycast(transform.position, FindDirection(), 1000f, personLayer)) //1000f is 1000metres distance
        //{
            rigidBody.isKinematic = false;
            rigidBody.AddForce(rollPower * transform.TransformDirection(FindDirection()), ForceMode.Impulse);
            roll = false;
        //}
        //rigidBody.AddForce(rollPower * transform.TransformDirection(FindDirection()), ForceMode.Impulse); //this is before integrating Raycast
        //roll = false;
    }

    Vector3 FindDirection() //In this case we put Vector3 instead of Void because we actually want it to return something
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 10f; //if you don't put it past the camera you will have the mouse on top of the camera always which is bad
        Vector3 direction = Camera.main.ScreenToWorldPoint(mousePosition);
        direction.z = 10f;
        return direction.normalized;
    }

    //bool CheckGameOver()
    //{
        //if the score is less than X
        //do game things
        //return true
        
        //if NOT less than stuff
        //return false
    //}

    int CalculateGrade(int a, int b)
    {
        return a + b;
    }
}
