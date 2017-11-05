using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject upgradeMenuGUI;
    public string TowerType;

    private UpgradeMenu menu;

    // Use this for initialization
    void Start()
    {
        menu = upgradeMenuGUI.GetComponent<UpgradeMenu>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter called");
        menu.displayPrice(TowerType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit called");
        menu.hidePrice();
    }

    void OnMouseOver()
    {
        Debug.Log("OnMouseOver called");
        menu.displayPrice(TowerType);
    }

    void OnMouseExit()
    {
        menu.hidePrice();
    }
}
