using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextStuff : MonoBehaviour {
    TextMesh manaStatusText;
    Camera cam;
    private Vector3 startingPosition;
	// Use this for initialization
	void Start () {
        startingPosition = transform.position;
        manaStatusText = GetComponentInChildren<TextMesh>();
        cam = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        manaStatusText.transform.LookAt(cam.transform);
        if (manaStatusText.text != "")
        {
            StartCoroutine(WaitForKill());
            manaStatusText.transform.position += (new Vector3(0, 1, 0) * Time.deltaTime);
        }
        else
        {
            manaStatusText.transform.position = startingPosition;
        }
	}

    IEnumerator WaitForKill()
    {
        yield return new WaitForSeconds(1);
        manaStatusText.text = "";
    }

    public void ResetTo(string text)
    {
        manaStatusText.transform.position = startingPosition;
        manaStatusText.text = text;
    }
}
