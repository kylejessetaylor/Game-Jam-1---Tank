using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
public class Movement : MonoBehaviour
{

    public float velocity = 0;
    public float maxSpeed = 10.0f;
    public float rotSpeed = 50.0f;

    private Rigidbody rigidbodys;
    public XboxController controller;
    // Use this for initialization
    void Awake ()
    {
        rigidbodys = GetComponent<Rigidbody>();

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
            GetComponent<Rigidbody>().velocity += moveForward * velocity * Time.deltaTime * 10;         
        }
        else
        {
            velocity = 0;

        }

        if(leftTiggerDown() == true)
        {
            velocity = 5;

            GetComponent<Rigidbody>().velocity += moveBack * velocity * Time.deltaTime * 10;
        }
        else
        {
            velocity = 0;
   
        }
       
        if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) > 0)
        {
            rigidbodys.MoveRotation(rigidbodys.rotation * rotationPos);
        }
        if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) < 0)
        {
            rigidbodys.MoveRotation(rigidbodys.rotation * rotationNeg);
        }

        if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
           rigidbodys.velocity = rigidbodys.velocity.normalized * maxSpeed;

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

    //By Kyle. Death by Mines
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Projectile")
        {
            //------ [Ryan Edit Start] ------//
            //to return isHit bool to UI Manager
            gameObject.GetComponent<IsHitScript>().IsHit = true;
            //------ [Ryan Edit End] ------//

            gameObject.SetActive(false);
        }
    }

}
