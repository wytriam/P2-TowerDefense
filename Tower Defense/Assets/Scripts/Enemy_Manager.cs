﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public GameObject waypoint;
    public float speed = 2.0f;
    Vector3 lastPos;

    // Use this for initialization
    void Start()
    {

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
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject other = coll.gameObject;
        if (other.tag == "Waypoint")
        {
            Debug.Log("Enemy_Manager::OnCollisionEnter() - Changing Waypoint");
            other = coll.gameObject.transform.parent.gameObject;
            waypoint = other.gameObject.GetComponent<Waypoint>().nextWaypoint;
        }
        else if (other.tag == "Tower")
        {
            Debug.Log("Enemy_Manager::OnCollisionEnter() - Destroying self");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Enemy_Manager::OnCollisionEnter() - Unknown Collision Detected");
        }
    }
}
