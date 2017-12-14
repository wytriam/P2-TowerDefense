using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaText : MonoBehaviour
{
    Camera cam;
    public GameObject textItem;
    private Vector3 startingPosition;
    // Use this for initialization
    void Start()
    {
        startingPosition = transform.position;
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddText(string text)
    {
        GameObject obj = Instantiate(textItem);
        obj.transform.position = new Vector3(0, 14, 0);
        TextMesh mesh = obj.GetComponent<TextMesh>();
        mesh.text = text;
        mesh.color = new Color(1, 0, 0, 1);
    }
}
