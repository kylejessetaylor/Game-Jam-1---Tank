using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public LayerMask collisionMask;
   [SerializeField] private GameObject CanonRot;
    public Transform bullet;
    private float rotSpeed = 1;
    // Use this for initialization
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * CanonRot.GetComponent<CanonRot>().bulletSpeed);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime * CanonRot.GetComponent<CanonRot>().bulletSpeed + 0.1f, collisionMask))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }
    }
}
