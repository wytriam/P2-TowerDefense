﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager_Defender : MonoBehaviour
{
    TextStuff textyPoo;
    public float health = 300f;
    public float manaForKill = 10f;

    [HideInInspector]
    public float damageMultiplier = 1f;

    [HideInInspector]
    public bool isSlowed;

    private List<GameObject> nearbyTowers;
    private Mana mana;
    private EnemyCounter enemycounter;
    private SM_tower_defense sm;
    private EnemyNavigation nav;

    private bool deathStarted = false;

    // Use this for initialization
    void Start()
    {
        textyPoo = FindObjectOfType<TextStuff>();
        nearbyTowers = new List<GameObject>();
        mana = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Mana>();
        enemycounter = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EnemyCounter>();
        sm = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SM_tower_defense>();
        nav = GetComponent<EnemyNavigation>();
        enemycounter.register(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (!deathStarted)
                StartCoroutine("deathSequence");
        }

    }

    IEnumerator deathSequence()
    {
        deathStarted = true;
        // GOOD SOLUTION - send enemy flying upwards to activate OnTriggerExit() in tower
        transform.position = transform.position + new Vector3(0, 1000, 0);
        mana.currentMana += manaForKill;
        if (sm != null)
            sm.score += (int)manaForKill;
        enemycounter.deregister(gameObject);
        AudioSource sound = GetComponent<AudioSource>();
        sound.mute = false;
        sound.Play();
        while (sound.isPlaying)
            yield return null;
        Destroy(gameObject);

    }

    IEnumerator WaitThenMove(float seconds)
    {
        nav.movingAllowed = false;
        yield return new WaitForSeconds(seconds);
        nav.movingAllowed = true;
        StopCoroutine("WaitThenMove");
    }

    public void OnTriggerEnter(Collider coll)
    {
        GameObject other = coll.gameObject.transform.root.gameObject;
        if (other.tag == "Projectile")
        {
            Projectile_Manager p = other.gameObject.GetComponent<Projectile_Manager>();
            p.ParticlesOnDeath();
            health -= p.damageOnHit * damageMultiplier;
            Instantiate(p.effect, gameObject.transform);
            Destroy(other.gameObject);
        }
        if (other.tag == "Waypoint")
        {
            other = coll.gameObject.transform.parent.gameObject;
            nav.waypoint = other.gameObject.GetComponent<Waypoint>().nextWaypoint;
        }
        else if (other.tag == "EndPoint")
        {
            mana.currentMana -= 2 * health;
            textyPoo.ResetTo("-" + (2 * health));
            other = coll.gameObject;
            nav.waypoint = nav.firstWaypoint;
            gameObject.transform.position = nav.spawnPos;
        }
        else if (other.tag == "Tower")
        {
            StartCoroutine(WaitThenMove(8));
        }
    }
}