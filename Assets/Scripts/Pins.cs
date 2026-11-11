using UnityEngine;
using UnityPipeline.Microsoft.CodeAnalysis.CSharp.Syntax;

public class Pins : MonoBehaviour


{
    private bool hasFallen = false;
    Vector3 startPosition;
    Quaternion startRotation;
    
    [SerializeField] float fallAngle = 20;
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
    bool CheckHasFallen()
    {
        //euler numbers 0 - 360, converting
        Vector3 EulerRotation = transform.rotation.eulerAngles;
        if (Mathf.Abs(EulerRotation.x) > fallAngle || Mathf.Abs(EulerRotation.z) > fallAngle)
      { 
            return true;
      }
        return false;
    }
  

    void Update()
    {
        // if bool is true 
        if (CheckHasFallen() == true && hasFallen == false)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Manager>().PinHasFallen();
            hasFallen = true;
            Debug.Log("has fell");
        }
    }

    public void ResetPins()
    {
        transform.position = startPosition;
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.rotation = startRotation;
       
    }
}


