using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {

    public float shellDespawnTime;

    void OnEnable ()
    {
        Invoke("Destroy", shellDespawnTime);
	}
	
	void Destroy ()
    {
        gameObject.SetActive(false);
	}
}
