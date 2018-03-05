using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
public class CanonRot : MonoBehaviour
{

    public Rigidbody rigidbodys;
    public XboxController controller;
    float rotSpeed = 50.0f;


    // Use this for initialization
    void Awake ()
    {
        rigidbodys = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate ()
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
}
