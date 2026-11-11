using UnityEngine;
using UnityEngine.InputSystem;

public class Spinner : MonoBehaviour
{
    private Rigidbody rb;
    public float spinForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            rb.AddTorque(Vector3.up * spinForce, ForceMode.Impulse);
        }

        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            rb.AddTorque(Vector3.up * -spinForce, ForceMode.Impulse);
        }

        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            rb.AddTorque(Vector3.forward * -spinForce, ForceMode.Impulse);
        }
    
    }
}
