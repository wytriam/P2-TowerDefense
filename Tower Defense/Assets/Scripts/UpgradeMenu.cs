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

    public Text priceDisplay;

    public int baseMagicCost = 100;
    public int baseFireCost = 150;
    public int baseSlowingCost = 200;
    public int baseCurseCost = 300;

    private GameObject menu;
    private int towerTier;
    
    // Use this for initialization
	void Start ()
    {
        menu = transform.GetChild(0).gameObject;
        priceDisplay.enabled = false;
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
        towerToUpgrade.setCostandPrefab(baseMagicCost * (towerTier + 1), magicTower[towerTier]);
        towerToUpgrade.checkUpgrade();
        menu.SetActive(false);
    }

    public void selectFireTower()
    {
        towerToUpgrade.setCostandPrefab(baseFireCost * (towerTier + 1), fireTower[towerTier]);
        towerToUpgrade.checkUpgrade();
        menu.SetActive(false);
    }

    public void selectSlowingTower()
    {
        towerToUpgrade.setCostandPrefab(baseSlowingCost * (towerTier + 1), slowingTower[towerTier]);
        towerToUpgrade.checkUpgrade();
        menu.SetActive(false);
    }

    public void selectCurseTower()
    {
        towerToUpgrade.setCostandPrefab(baseCurseCost * (towerTier + 1), curseTower[towerTier]);
        towerToUpgrade.checkUpgrade();
        menu.SetActive(false);
    }

    public void displayPrice(string towerType)
    {
        switch(towerType)
        {
            case "Magic":
                priceDisplay.text = (baseMagicCost * (towerTier + 1)).ToString() + " Mana";
                break;
            case "Fire":
                priceDisplay.text = (baseFireCost * (towerTier + 1)).ToString() + " Mana";
                break;
            case "Slowing":
                priceDisplay.text = (baseSlowingCost * (towerTier + 1)).ToString() + " Mana";
                break;
            case "Curse":
                priceDisplay.text = (baseCurseCost * (towerTier + 1)).ToString() + " Mana";
                break;
        }
        priceDisplay.enabled = true;
    }

    public void hidePrice()
    {
        priceDisplay.enabled = false;
    }

}
