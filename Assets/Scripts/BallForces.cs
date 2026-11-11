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
        
        startPosition = transform.position;
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Mouse.current.leftButton.isPressed)

        {

            UpdateCharge();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame) //check if left mouse button was just clicked
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
        clickForce = (launchPowerRange * percentageCharge);

       

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Gutter")
        {
            Debug.Log("gutter collision");
            myManager.EnterReset();
        }
    }

    public void ResetBall()
    {
        transform.position = startPosition;
    }
}
