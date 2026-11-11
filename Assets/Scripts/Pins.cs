using UnityEngine;
using UnityPipeline.Microsoft.CodeAnalysis.CSharp.Syntax;

public class Pins : MonoBehaviour


{
    private bool hasFallen = false;
    [SerializeField] float fallAngle = 20;

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

    
}


