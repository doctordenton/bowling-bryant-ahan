using UnityEngine;
using UnityEngine.InputSystem;

public class BallForces : MonoBehaviour
{
    private Rigidbody rb;
    public float movementForce;
    public float jumpForce;
    public float clickForce;
    public float minLaunch;
    public float maxLaunch;
    public float chargeTimer = 0;
    public float maxChargeTimer = 3;
    public Manager myManager;
    Vector3 startPosition;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //
        transform.position = startPosition;
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame) //check if left mouse button was just clicked
        {
            //where did the mouse click in terms of the world. 
            Ray mouseCameraRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit mouseRaycastHit;

            if(Physics.Raycast(mouseCameraRay, out mouseRaycastHit)){
                
                //did the mouse raycast just hit this ball's collider?
                if(mouseRaycastHit.collider.gameObject == this.gameObject)
                {
                    //push the ball away from the position where the raycast hit it
                    Vector3 forceDirection = transform.position - mouseRaycastHit.point;
                    forceDirection.Normalize();
                    rb.AddForceAtPosition(forceDirection * clickForce, 
                        mouseRaycastHit.point,ForceMode.Impulse);
                }
            }

            //Do not launch in the code above. The press down should only be to start the charge.
            //When we release the left mouse button is when the launch occurs.

            //To find the launch power we want to do minimum Launch power + (MaxLaunchPower - MinimumLaunchPower * the percentage of time the 
            
        }
    }

    void FixedUpdate()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            rb.AddForce(Vector3.forward * movementForce, ForceMode.Force);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            rb.AddForce(Vector3.back * movementForce, ForceMode.Force);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            rb.AddForce(Vector3.right * movementForce, ForceMode.Force);
        }
        if (Keyboard.current.aKey.isPressed)
        {
            rb.AddForce(Vector3.left * movementForce, ForceMode.Force);
        }
    }

    void UpdateCharge()
    {
        float launchPowerRange = maxLaunch - minLaunch;
        chargeTimer += Time.deltaTime;
        chargeTimer = Mathf.Clamp(chargeTimer, 0, maxChargeTimer);
        float percentageCharge = chargeTimer / maxChargeTimer;
        launchPowerRange = (launchPowerRange + percentageCharge);

        //if (isCharging == true)

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Gutter")
        {
            Debug.Log("gutter collision");
            myManager.EnterReset();
        }
    }
}
