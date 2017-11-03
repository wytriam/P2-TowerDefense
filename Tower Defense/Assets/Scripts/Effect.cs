using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public bool damageOverTime;
    public float DoTdamage = 1;
    public bool slowing;
    public float effectDuration = 5f;
    public bool curse;

    [HideInInspector]
    public Enemy_Manager enemy;
    private float timer = 0;

	// Use this for initialization
	void Start ()
    {
        enemy = this.transform.root.gameObject.GetComponent<Enemy_Manager>();
        if (enemy == null)
        {
            Debug.Log("Not attached to an enemey");
            Destroy(gameObject);
        }

        StartCoroutine("effect");
	}
	
    IEnumerator effect()
    {
        if (slowing)
            enemy.speed /= 2;
        while (true)
        {
            // put effect here
            if (damageOverTime)
            {
                enemy.health -= DoTdamage;
            }
            timer += 1;
            if (timer > effectDuration)
                Destroy(gameObject);
            yield return new WaitForSeconds(1);
        }
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
