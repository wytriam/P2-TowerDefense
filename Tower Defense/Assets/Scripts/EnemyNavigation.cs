using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    public GameObject waypoint;
    public float speed = 2.0f;

    [HideInInspector]
    public GameObject firstWaypoint;

    [HideInInspector]
    public Vector3 spawnPos;

    [HideInInspector]
    public bool movingAllowed = true;

    // Update is called once per frame
    void Update()
    {
        //lastPos = gameObject.transform.position;
        if (movingAllowed && waypoint != null)
        {
            Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, waypoint.transform.position, speed * Time.deltaTime);
            gameObject.transform.position = movement;
            gameObject.transform.LookAt(waypoint.transform);
        }

    }
}
