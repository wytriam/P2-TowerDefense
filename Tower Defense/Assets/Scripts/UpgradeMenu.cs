using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

    [HideInInspector]
    public Tower_Upgrade towerToUpgrade;

    public GameObject[] magicTower;
    public GameObject[] fireTower;
    public GameObject[] slowingTower;
    public GameObject[] curseTower;

    private GameObject menu;
    private int towerTier;
    //private SM_tower_defense sm;
    
    // Use this for initialization
	void Start ()
    {
        menu = transform.GetChild(0).gameObject;
        //sm = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SM_tower_defense>();
        menu.SetActive(false);
	}
	
    public void openMenu(Tower_Upgrade tower, int tier)
    {
        menu.SetActive(true);
        towerTier = tier - 1;
        towerToUpgrade = tower;
    }

    public void closeMenu()
    {
        menu.SetActive(false);
    }

    public void selectMagicTower()
    {
        towerToUpgrade.setCostandPrefab(100, magicTower[towerTier]);
        towerToUpgrade.checkUpgrade();
        menu.SetActive(false);
    }

    public void selectFireTower()
    {
        towerToUpgrade.setCostandPrefab(150 * (towerTier + 1), fireTower[towerTier]);
        towerToUpgrade.checkUpgrade();
        menu.SetActive(false);
    }

    public void selectSlowingTower()
    {
        towerToUpgrade.setCostandPrefab(200 * (towerTier + 1), slowingTower[towerTier]);
        towerToUpgrade.checkUpgrade();
        menu.SetActive(false);
    }

    public void selectCurseTower()
    {
        towerToUpgrade.setCostandPrefab(300 * (towerTier + 1), curseTower[towerTier]);
        towerToUpgrade.checkUpgrade();
        menu.SetActive(false);
    }
}
