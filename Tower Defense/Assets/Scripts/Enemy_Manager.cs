﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public GameObject waypoint;
    public float speed = 2.0f;
    public float health = 5f;
    public float manaForKill = 10f;

    private Vector3 lastPos;

    private List<GameObject> nearbyTowers;
    private Mana mana;
    private EnemyCounter enemycounter;

    // Use this for initialization
    void Start()
    {
        nearbyTowers = new List<GameObject>();
        mana = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Mana>();
        enemycounter = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EnemyCounter>();
        enemycounter.register(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        lastPos = gameObject.transform.position;
        if (waypoint != null)
        {
            Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, waypoint.transform.position, speed * Time.deltaTime);
            movement.y = lastPos.y;
            gameObject.transform.position = movement;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.LookAt(waypoint.transform);
        }
        if (health <= 0)
        {
            foreach (GameObject tower in nearbyTowers)
                tower.GetComponent<Tower_Manager>().deregister(gameObject);
            mana.currentMana += manaForKill;
            enemycounter.deregister(gameObject);
            Destroy(gameObject);
        }

    }

    public void deregisterTower(GameObject tower)
    {
        nearbyTowers.Remove(tower);
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject other = coll.gameObject.transform.root.gameObject;
        if (other.tag == "Waypoint")
        {
            //Debug.Log("Enemy_Manager::OnCollisionEnter() - Changing Waypoint");
            other = coll.gameObject.transform.parent.gameObject;
            waypoint = other.gameObject.GetComponent<Waypoint>().nextWaypoint;
        }
        else if (other.tag == "EndPoint")
        {
            //Debug.Log("Enemy_Manager::OnCollisionEnter() - Moving to Beginning of Course");
            mana.currentMana -= 2*health;
            other = coll.gameObject;
            waypoint = other.gameObject.GetComponent<Waypoint>().nextWaypoint;
            gameObject.transform.position = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Waves>().enemySpawnPoint.gameObject.transform.position;
        }
        else
        {
            //Debug.Log("Enemy_Manager::OnCollisionEnter() - Unknown Collision Detected: " + other.tag);
        }
    }

    public void OnTriggerEnter(Collider coll)
    {
        GameObject other = coll.gameObject.transform.root.gameObject;
        if (other.tag == "Tower")
        {
            //Debug.Log("Enemy_Manager::OnTriggerEnter() - Registering with Tower");
            coll.gameObject.GetComponent<Tower_Manager>().register(this.gameObject);
            nearbyTowers.Add(other.gameObject);
        }
        if (other.tag == "Projectile")
        {
            //Debug.Log("Enemy_Manager::OnTriggerEnter - Got hit by projectile");
            other.gameObject.GetComponent<Projectile_Manager>().ParticlesOnDeath();
            Destroy(other.gameObject);
            health--;
        }
    }

    public void OnTriggerExit(Collider coll)
    {
        GameObject other = coll.gameObject.transform.root.gameObject;
        if (other.tag == "Tower")
        {
            // Tell tower enemy is out of range for firing
            //Debug.Log("Enemy_Manager::OnTriggerExit() - Deregistering with Tower");
            coll.gameObject.GetComponent<Tower_Manager>().deregister(this.gameObject);
            nearbyTowers.Remove(other.gameObject);
        }
    }

}
