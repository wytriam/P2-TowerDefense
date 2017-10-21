using System.Collections;
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
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        else if (other.tag == "EndPoint")
        {
            Debug.Log("Enemy_Manager::OnCollisionEnter() - Destroying self");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Enemy_Manager::OnCollisionEnter() - Unknown Collision Detected: " + other.tag);
        }
    }

    public void OnTriggerEnter(Collider coll)
    {
        GameObject other = coll.gameObject;
        if (other.tag == "Tower")
        {
            Debug.Log("Enemy_Manager::OnCollisionEnter() - Registering with Tower");
            coll.gameObject.GetComponent<Tower_Manager>().register(this.gameObject);
        }
    }

    public void OnTriggerExit(Collider coll)
    {
        GameObject other = coll.gameObject;
        if (other.tag == "Tower")
        {
            // Tell tower enemy is out of range for firing
            Debug.Log("Enemy_Manager::OnTriggerExit() - Deregistering with Tower");
            coll.gameObject.GetComponent<Tower_Manager>().deregister(this.gameObject);
        }
    }
}
