using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioClip bounceSound;

    public LayerMask collisionMask;
   [SerializeField] private GameObject CanonRotation;
    public GameObject player;
    //PlayerHP playerHP;
    public int bulletDamage;
    public float shellDespawnTime;

    private bool isDead;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        isDead = false;
        player = GameObject.FindGameObjectWithTag("Player");
		AudioSource.PlayClipAtPoint (bounceSound, transform.position);


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

			//Bounce Sound
			AudioSource.PlayClipAtPoint (bounceSound, transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //------ [Ryan Edit Start] ------//
            //to return isHit bool to UI Manager
            collision.gameObject.GetComponent<IsHitScript>().IsHit = true;

            //------ [Ryan Edit End] ------//

            //Destroy(collision.gameObject);
            //Made by Kyle. To destroy bullet
			AudioSource.PlayClipAtPoint(deathSound, transform.position);
			gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        Invoke("Destroy", shellDespawnTime);

    }

    void Destroy()
    {
        //Audio
//        AudioSource.PlayClipAtPoint(deathSound, transform.position);

        gameObject.SetActive(false);
    }
}
