using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float effectDuration = 5f;
    public bool damageOverTime;
    public bool slowing;
    public bool curse;
    public float DoTdamage = 1;
    public float curseIntensity = 2f;

    [HideInInspector]
    public EnemyEffectMgr enemy;
    [HideInInspector]
    public EnemyNavigation nav;
    private float timer = 0;
    private float prevEnemySpeed = 0;

	// Use this for initialization
	void Start ()
    {
        enemy = this.transform.root.gameObject.GetComponent<EnemyEffectMgr>();
        nav = this.transform.root.gameObject.GetComponent<EnemyNavigation>();
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
        {
            if (enemy.isSlowed)         // slowing doesn't stack
                Destroy(gameObject);
            enemy.isSlowed = true;
            prevEnemySpeed = nav.speed;   
            nav.speed /= 2;
        }
        if (curse)
            enemy.damageMultiplier *= 2;
        if (damageOverTime)
            enemy.dotOn = true;
        while (true)
        {
            // put effect here
            timer += 1;
            if (timer > effectDuration)
            {
                if (slowing)
                {
                    nav.speed = prevEnemySpeed;
                    enemy.isSlowed = false;
                }
                if (curse)
                    enemy.damageMultiplier /= 2;
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(1);
        }
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
