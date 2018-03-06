using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHitScript : MonoBehaviour
{
    private bool isHit;
    // Use this for initialization
    void Start()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsHit
    {
        get { return isHit; }
        set { isHit = value; }
    }
}
