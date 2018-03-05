using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
public class CanonRot : MonoBehaviour
{

    private Rigidbody rigidbodys;
    public XboxController controller;
    public  float rotSpeed = 50.0f;
    public Rigidbody shell;
    public Transform fireTransform;
    public float turretHeight;


    public float launchForce = 10.0f;

    public float bulletSpeed
    {
        get
        {
            return this.launchForce;
        }
        set
        {
            this.launchForce = value;
        }
    }

    private bool fired;
    public float time;

    public GameObject movPos;

    // Use this for initialization
    void Awake ()
    {
        rigidbodys = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbodys.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;
        time += Time.deltaTime;
        if(time >= 1)
        {
            if (rightTiggerDown())
            {
                ShootBullet();

                time = 0;
            }
            else
            {
                fired = false;
            }
        }

        this.transform.position = movPos.transform.position + new Vector3(0, turretHeight, 0);

    }

    void FixedUpdate()
    {
        float turn = rotSpeed * Time.deltaTime;
        Quaternion rotationPos = Quaternion.Euler(0, turn, 0);
        Quaternion rotationNeg = Quaternion.Euler(0, -turn, 0);

        if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) > 0)
        {
            rigidbodys.MoveRotation(rigidbodys.rotation * rotationPos);
        }
        if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) < 0)
        {
            rigidbodys.MoveRotation(rigidbodys.rotation * rotationNeg);
        }
    }


    private void ShootBullet()
    {
        fired = true;
        Rigidbody shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation) as Rigidbody;

        shellInstance.velocity = launchForce * fireTransform.forward;
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
}
