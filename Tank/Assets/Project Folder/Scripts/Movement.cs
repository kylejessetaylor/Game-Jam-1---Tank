using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
public class Movement : MonoBehaviour
{

    private const float accelration = 1.0f;
    private const float friction = 1.0f;
    float velocity = 0;
    float maxSpeed = 10.0f;
    float rotSpeed = 50.0f;

    private Rigidbody rigidbody;
    public XboxController controller;
    // Use this for initialization
    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody>();

    }
    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate ()
    {

        Vector3 moveForward = this.transform.rotation * Vector3.forward;
        Vector3 moveBack = this.transform.rotation * Vector3.back;

        float turn = rotSpeed * Time.deltaTime;

        Quaternion rotationPos = Quaternion.Euler(0, turn, 0);
        Quaternion rotationNeg = Quaternion.Euler(0, -turn, 0);


        //movement.x = XCI.GetAxisRaw(XboxAxis.LeftStickX, XboxController.All);
        //movement.z = XCI.GetAxisRaw(XboxAxis.LeftStickY, controller);

        //if (movement.x > 0)
        //{
        //    rigidbody.velocity += Vector3.right * velocity * Time.deltaTime * 10;
        //}
        //if (movement.x < 0)
        //{
        //    rigidbody.velocity += Vector3.left * velocity * Time.deltaTime * 10;
        //}

        if (rightTiggerDown()== true)
        {
            velocity = 5;
            rigidbody.velocity += moveForward * velocity * Time.deltaTime * 10;         
        }
        else
        {
            velocity = 0;
            if (velocity == 0)
            {
                this.rigidbody.velocity = Vector3.zero;
            }
        }

        if(leftTiggerDown() == true)
        {
            velocity = 5;

            rigidbody.velocity += moveBack * velocity * Time.deltaTime * 10;
        }
        else
        {
            velocity = 0;
            
        }
       
        if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) > 0)
        {
            rigidbody.MoveRotation(rigidbody.rotation * rotationPos);
        }
        if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) < 0)
        {
            rigidbody.MoveRotation(rigidbody.rotation * rotationNeg);
        }

        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;

        }

    }

    public bool rightTiggerDown()
    {
        if (XCI.GetAxisRaw(XboxAxis.RightTrigger, controller) != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool leftTiggerDown()
    {
        if (XCI.GetAxisRaw(XboxAxis.LeftTrigger, controller) != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
