using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Manager : MonoBehaviour
{
    public float rateOfFire = 2.0f;

    private WytriamSTD.Spawn spawnScript;
    public bool isFiring = false;
    [HideInInspector]
    public bool canFire = true;
    [HideInInspector]
    public bool beingBlocked = false;

    private AudioSource sound;

    // Use this for initialization
    void Start ()
    {
        spawnScript = GetComponent<WytriamSTD.Spawn>();
        sound = GetComponent<AudioSource>();
	}
	
    IEnumerator firing()
    {
        isFiring = true;        
        while(isFiring)
        {
            shoot();
            yield return new WaitForSeconds(1 / rateOfFire);
        }
    }

    public void shoot()
    {
        sound.mute = false;
        sound.Play();
        spawnScript.spawn();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canFire) return;
        if (other.gameObject.tag == "Enemy")
            if (!isFiring)
                StartCoroutine("firing");
    }

    void OnTriggerStay(Collider other)
    {
        if (!canFire) return;
        if (other.gameObject.tag == "Enemy")
            if (!isFiring)
                StartCoroutine("firing");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            if (isFiring)
            {
                StopCoroutine("firing");
                isFiring = false;
            }
    }

    // disables the tower for 5 seconeds
    public void disableTower()
    {
        canFire = false;
        if (isFiring)
        {
            StopCoroutine("firing");
            isFiring = false;
        }
        StartCoroutine("TowerPowerCycle");
    }

    IEnumerator TowerPowerCycle()
    {
        yield return new WaitForSeconds(5);
        canFire = true;
        beingBlocked = false;
        StopCoroutine("TowerPowerCycle");
    }
}
