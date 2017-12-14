using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    public Vector3 waypoint;
    public float speed = 2.0f;
    public float randomRange = 1;

    [HideInInspector]
    public GameObject firstWaypoint;

    [HideInInspector]
    public Vector3 spawnPos;

    [HideInInspector]
    public bool navEnabled = true;

    void Start()
    {
        speed *= Random.Range(0.9f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //lastPos = gameObject.transform.position;
        if (navEnabled)
        {
            Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, waypoint, speed * Time.deltaTime);
            gameObject.transform.position = movement;
            gameObject.transform.LookAt(waypoint);
        }

    }

    public void SetWaypoint(GameObject _waypoint)
    {
        waypoint =  new Vector3(_waypoint.transform.position.x + Random.Range(-randomRange, randomRange),
                                _waypoint.transform.position.y, //+ Random.Range(-randomRange, randomRange),
                                _waypoint.transform.position.z + Random.Range(-randomRange, randomRange));

    }
}
