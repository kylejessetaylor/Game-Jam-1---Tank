using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public LayerMask collisionMask;
   [SerializeField] private GameObject CanonRotation;
    public Transform bullet;
    public GameObject player;
    //PlayerHP playerHP;
    public int bulletDamage;
    public float shellDespawnTime;
    // Use this for initialization
    void Awake()
    {
    //    playerHP = GetComponent<PlayerHP>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * CanonRotation.GetComponent<CanonRot>().bulletSpeed);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime * CanonRotation.GetComponent<CanonRot>().bulletSpeed + 0.1f, collisionMask))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }

        Attack();
    }


    void Attack()
    {
     //   if (player.GetComponent<PlayerHP>().health > 0)
     //   {
     //       playerHP.TakeDamage(bulletDamage);
     //  }
    }

    void OnEnable()
    {
        Invoke("Destroy", shellDespawnTime);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
