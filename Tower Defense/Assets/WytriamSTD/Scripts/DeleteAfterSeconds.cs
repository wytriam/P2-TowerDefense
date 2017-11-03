using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterSeconds : MonoBehaviour
{
    public float duration = 1f;

    private float timer;

	// Use this for initialization
	void Start ()
    {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.fixedDeltaTime;
        if (timer > duration)
            Destroy(gameObject);
	}
}
