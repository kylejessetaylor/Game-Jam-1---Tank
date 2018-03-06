using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
public class CanonRot : MonoBehaviour
{

    private Rigidbody rigidbodys;
    public XboxController controller;
    public  float rotSpeed = 50.0f;
    public GameObject shell;
    public Transform fireTransform;
    public float turretHeight;
    private bool fired;
    private float time;
    public GameObject movPos;
    [SerializeField]private int amountOfBullets;
    public List<GameObject> shells;
    public float launchForce = 10.0f;
    public float shellDelay;
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

    public bool Fired
    {
        get
        {
            return fired;
        }

        set
        {
            fired = value;
        }
    }


    // Use this for initialization
    void Awake ()
    {
        Fired = false;
        shells = new List<GameObject>();

        for (int i  = 0; i < amountOfBullets; i++)
        {
            GameObject shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);
            shellInstance.GetComponent<Rigidbody>().velocity += launchForce * fireTransform.forward * Time.deltaTime * 10;
            shellInstance.SetActive(false);
            shells.Add(shellInstance);
        }

        rigidbodys = GetComponent<Rigidbody>();


    }



    // Update is called once per frame
    void Update()
    {
        rigidbodys.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
        time += Time.deltaTime;
        if(time >= shellDelay)
        {
            if (rightTiggerDown())
            {
                ShootBullet();

                time = 0;
            }
            else
            {
                Fired = false;
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
        Fired = true;

        for (int i = 0; i < shells.Count; i++)
        {
            if(!shells[i].activeInHierarchy)
            {
                shells[i].transform.position = fireTransform.position;
                shells[i].transform.rotation = fireTransform.rotation;
                shells[i].SetActive(true);
                break;
            }
        }

       // GameObject shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);

       // shellInstance.GetComponent<Rigidbody>().velocity += launchForce * fireTransform.forward * Time.deltaTime * 10;
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
