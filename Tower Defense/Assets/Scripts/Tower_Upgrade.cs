using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Upgrade : MonoBehaviour
{
    public GameObject upgradesToPrefab;
    public float costToUpgrade = 100f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseDown()
    {
        upgrade();
    }

    public void upgrade()
    {
        // create the new tower based on the prefab and set it's transform to be equivalent to this transform
        GameObject upgrade = Instantiate(upgradesToPrefab);
        upgrade.transform.position = gameObject.transform.position;
        upgrade.transform.rotation = gameObject.transform.rotation;
        upgrade.transform.localScale = gameObject.transform.localScale;
        // delete the old tower
        Destroy(gameObject);

    }
}
