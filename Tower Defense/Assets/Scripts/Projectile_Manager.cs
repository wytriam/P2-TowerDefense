﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Manager : MonoBehaviour
{
    public float maxHeight = 6f; //after reach this height, the projectile finds the nearest enemy and attacks it
    public float speed = 4f;
    public float duration = 2f;
    public int damageOnHit = 1;
    public float maxDist = 100f;
    public GameObject deathSplosion;
    public GameObject effect;

    private bool seekAndDestroy;
    private Vector3 lastPos;
    private Vector3 startingPos;
    private GameObject target;
    private float lifeTimer = 0f;


	// Use this for initialization
	void Start ()
    {
        seekAndDestroy = false;
        startingPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        lastPos = gameObject.transform.position;
        lifeTimer += Time.fixedDeltaTime;
        if (lifeTimer > duration)
        {
            Destroy(gameObject);
        }

        if (seekAndDestroy)
        {
            if (target != null)
            {
                Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
                gameObject.transform.position = movement;
            }
            else
            {
                //Debug.Log("Projectile_Manager::Update() - No target found; Destroying Self");
                Destroy(gameObject);
            }
        }
        else if (lastPos.y >= startingPos.y + maxHeight)
        {
            // find the nearest enemy and prepare to attack it
            seekAndDestroy = true;
            findTarget();
        }
        else
        {
            // move up
            Vector3 movement = new Vector3(0, 10f, 0) * Time.deltaTime;
            gameObject.transform.position += movement;
        }
	}

    void findTarget()
    {
        //Debug.Log("Projectile_Manager::findTarget() - Looking for Target");
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = -1f;
        foreach (GameObject go in targets)
        {
            float tempDis = (go.transform.position - gameObject.transform.position).sqrMagnitude;
            if (distance == -1f)
            {
                target = go;
                distance = tempDis;
            }
            else if (tempDis < distance)
            {
                target = go;
                distance = tempDis;
            }
        }
        gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

        // destroys projectile if target is too far away
        if (distance > maxDist)
            Destroy(gameObject);
    }

    public void ParticlesOnDeath()
    {
        Instantiate(deathSplosion, lastPos, Quaternion.Euler(0, 0, 0));
    }
}
