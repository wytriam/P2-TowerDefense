using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Upgrade : MonoBehaviour
{
    public GameObject upgradesToPrefab;
    public float costToUpgrade = 100f;

    [HideInInspector]
    public GameObject scene_manager;


    // Use this for initialization
    void Start ()
    {
        scene_manager = GameObject.FindGameObjectWithTag("SceneManager");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseDown()
    {
        if (scene_manager.GetComponent<SM_level_1>().currentMana >= costToUpgrade)
        {
            upgrade();
            scene_manager.GetComponent<SM_level_1>().currentMana -= costToUpgrade;
        }
    }

    public void upgrade()
    {
        // create the new tower based on the prefab and set it's transform to be equivalent to this transform
        GameObject upgrade = Instantiate(upgradesToPrefab);
        upgrade.transform.position = gameObject.transform.position;
        upgrade.transform.rotation = gameObject.transform.rotation;
        upgrade.transform.localScale = gameObject.transform.localScale;

        // check to see if Tower_Manager exists
        // If it does, deregister all enemies that tower is aware of
        Tower_Manager tm = gameObject.GetComponent<Tower_Manager>();
        if (tm != null)
            tm.deregisterAll();

        // delete the old tower
        Destroy(gameObject);

    }
}
