using UnityEngine;

public class TextMove : MonoBehaviour
{
    private void Start()
    {
        transform.position = FindObjectOfType<ManaText>().gameObject.transform.position + new Vector3(0, 14, 0);
    }

    private void Update()
    {
        gameObject.transform.position += new Vector3(0, 2, 0) * Time.deltaTime;
        gameObject.transform.LookAt(FindObjectOfType<Camera>().transform);
        gameObject.transform.rotation *= Quaternion.Euler(0, 180, 0);
    }
}