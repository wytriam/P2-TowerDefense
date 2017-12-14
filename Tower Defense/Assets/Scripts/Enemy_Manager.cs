using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{

    TextStuff textyPoo;
    public GameObject waypoint;
    public float health = 5f;
    public float manaForKill = 10f;



    //private Vector3 lastPos;

    private Mana mana;
    private EnemyCounter enemycounter;
    private SM_tower_defense sm;
    private EnemyNavigation nav;
    private EnemyEffectMgr eem;

    private bool deathStarted = false;
    private bool dot = false;

    // Use this for initialization
    void Start()
    {
        textyPoo = FindObjectOfType<TextStuff>();
        mana = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Mana>();
        enemycounter = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EnemyCounter>();
        sm = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SM_tower_defense>();
        nav = GetComponent<EnemyNavigation>();
        eem = GetComponent<EnemyEffectMgr>();
        enemycounter.register(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (!deathStarted)
                StartCoroutine("deathSequence");
        }
        if (eem.dotOn && !dot)
        {
            StartCoroutine("DamageOverTime");
        }

    }

    IEnumerator deathSequence()
    {
        deathStarted = true;
        nav.navEnabled = false;
        // HACKY SOLUTION - send enemy flying upwards to activate OnTriggerExit() in tower
        transform.position = transform.position + new Vector3(0, 1000, 0);
        mana.currentMana += manaForKill;
        if (sm != null)
            sm.score += (int)manaForKill;
        enemycounter.deregister(gameObject);
        AudioSource sound = GetComponent<AudioSource>();
        sound.mute = false;
        sound.Play();
        while (sound.isPlaying)
            yield return null;
        Destroy(gameObject);

    }

    public void OnTriggerEnter(Collider coll)
    {
        GameObject other = coll.gameObject.transform.root.gameObject;
        if (other.tag == "Projectile")
        {
            Projectile_Manager p = other.gameObject.GetComponent<Projectile_Manager>();
            p.ParticlesOnDeath();
            health -= p.damageOnHit * eem.damageMultiplier;
            Instantiate(p.effect, gameObject.transform);
            Destroy(other.gameObject);
        }
        if (other.tag == "Waypoint")
        {
            other = coll.gameObject.transform.parent.gameObject;
            nav.SetWaypoint(other.gameObject.GetComponent<Waypoint>().nextWaypoint);
        }
        else if (other.tag == "EndPoint")
        {
            mana.currentMana -= 2 * health;
            textyPoo.ResetTo("-" + (2 * health));
            other = coll.gameObject;
            nav.SetWaypoint(nav.firstWaypoint);
            gameObject.transform.position = nav.spawnPos;
        }
    }

    IEnumerator DamageOverTime()
    {
        dot = true;
        int timeElapsed = 0;
        while (timeElapsed < 4)
        {
            health -= 1 * eem.damageMultiplier;
            yield return new WaitForSeconds(1);
            timeElapsed++;
        }
        eem.dotOn = false;
        dot = false;
        StopCoroutine("DamageOverTime");
    }
}
