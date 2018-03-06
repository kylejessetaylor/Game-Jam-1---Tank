using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    //Explosion Audio
    public AudioClip mineExplosion;

    //Whipped Cream
    public GameObject projectile;

    //Delay time for explosion effect
    public float explosionDelay;

    //Reference Material
    public Material referenceMaterial;
    //Material Fading
    private Material mat;
    public float mineFadeTime;
    public float startFadeDelay;

    public bool fading;

    //Bugfix multiple Mines & 


    // Use this for initialization
    void Start ()
    {
        fading = true;
        //Instantiates new material
        mat = Instantiate(referenceMaterial);
        GetComponent<MeshRenderer>().material = mat;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (fading == true)
        {
            //Mine hides
            ColorTransition(-1);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Mine shows
            //StartCoroutine(FadeAfterTime(startFadeDelay));
            Color color = mat.color;
            color.a = 1;
            mat.color = color;
            //Starts timer for explosion
            StartCoroutine(ExplodeAfterTime(explosionDelay+startFadeDelay, this.gameObject));
        }
    }

    //multiplier = -1, means object fades
    //multiplier = 1, means object shows
    public void ColorTransition(int multiplier)
    {
        //Increase alpha
        Color color = mat.color;
        color.a += multiplier * Mathf.Pow(explosionDelay, -1)*Time.deltaTime * Mathf.Pow(mineFadeTime, -1);
        mat.color = color;
        if (color.a <= 0 && fading == true)
        {
            color.a = 0;
            fading = false;
        }
    }
    //Execution Delay
    IEnumerator ExplodeAfterTime(float time, GameObject thisObject)
    {
        //suspend execution for X seconds
        yield return new WaitForSeconds(time);

        //Audio
        AudioSource.PlayClipAtPoint(mineExplosion, transform.position);

        //Projectiles
        projectile.transform.position = thisObject.transform.position;
        projectile.transform.parent = null;
        projectile.SetActive(true);

        //"Destroys" Mine
        Color color = mat.color;
        color.a = 0;
        mat.color = color;
        GetComponent<Collider>().enabled = false;
    }

    //Fade Delay
    IEnumerator FadeAfterTime(float time)
    {
        //suspend execution for X seconds
        yield return new WaitForSeconds(time);
        ColorTransition(1);
    }
}
