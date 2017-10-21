using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Manager : MonoBehaviour
{
    public float maxHeight = 6f; //after reach this height, the projectile finds the nearest enemy and attacks it
    public float speed = 4f;

    private bool seekAndDestroy;
    private Vector3 lastPos;
    private GameObject target;


	// Use this for initialization
	void Start ()
    {
        seekAndDestroy = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        lastPos = gameObject.transform.position;
        if (seekAndDestroy)
        {
            if (target != null)
            {
                Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
                movement.y = lastPos.y;
                gameObject.transform.position = movement;
            }
        }
        else if (lastPos.y >= maxHeight)
        {
            // find the nearest enemy and prepare to attack it
            seekAndDestroy = true;
            findTarget();
        }
        else
        {
            // move up
            Vector3 movement = new Vector3(0, speed / 2, 0);
            gameObject.transform.position += movement;
        }
	}

    void findTarget()
    {
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
    }
}
