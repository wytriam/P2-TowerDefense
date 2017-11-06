using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Upgrade : MonoBehaviour
{
    public GameObject upgradesToPrefab;
    public float costToUpgrade = 100f;
    public int towerTier;
    public bool useMenu = false;
    public GameObject upgradeMenu;

    [HideInInspector]
    public GameObject scene_manager;


    // Use this for initialization
    void Start ()
    {
        scene_manager = GameObject.FindGameObjectWithTag("SceneManager");
	}
	
    void OnMouseDown()
    {
        if (useMenu)
            upgradeMenu.GetComponent<UpgradeMenu>().openMenu(this, towerTier);
        else
            checkUpgrade();
    }

    public void setCostandPrefab(int cost, GameObject towerPrefab)
    {
        costToUpgrade = cost;
        upgradesToPrefab = towerPrefab; 
    }

    public void checkUpgrade()
    {
        if (scene_manager.GetComponent<Mana>().currentMana >= costToUpgrade)
        {
            upgrade();
        }
        else
        {
            scene_manager.GetComponent<SM_tower_defense>().announce("You do not have enough mana. \nThis upgrade costs " + costToUpgrade + " mana.");
        }
    }

    private void upgrade()
    {
        // create the new tower based on the prefab and set it's transform to be equivalent to this transform
        GameObject upgrade = Instantiate(upgradesToPrefab);
        upgrade.transform.position = gameObject.transform.position;
        upgrade.transform.rotation = gameObject.transform.rotation;
        upgrade.transform.localScale = gameObject.transform.localScale;

        scene_manager.GetComponent<Mana>().currentMana -= costToUpgrade;
        if(scene_manager.GetComponent<SM_tower_defense>() != null)
            scene_manager.GetComponent<SM_tower_defense>().displayMana();

        // delete the old tower
        Destroy(gameObject);
    }
}
