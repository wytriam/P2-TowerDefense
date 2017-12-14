using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bomber : MonoBehaviour
{
    public float health = 5f;
    public float manaForKill = 10f;
    public GameObject explosionSystem;

    [HideInInspector]
    public float damageMultiplier = 1f;

    [HideInInspector]
    public bool isSlowed;

    private List<GameObject> nearbyTowers;
    private Mana mana;
    private EnemyCounter enemycounter;
    private SM_tower_defense sm;
    private EnemyNavigation nav;

    private bool deathStarted = false;
    private bool isBombing = false;

    // Use this for initialization
    void Start()
    {
        nearbyTowers = new List<GameObject>();
        mana = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Mana>();
        enemycounter = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EnemyCounter>();
        sm = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SM_tower_defense>();
        nav = GetComponent<EnemyNavigation>();
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

    }

    IEnumerator deathSequence()
    {
        // end the bomber sequence if running
        if (isBombing)
            StopCoroutine("bomberSequence");

        deathStarted = true;
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
            health -= p.damageOnHit * damageMultiplier;
            Instantiate(p.effect, gameObject.transform);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Waypoint")
        {
            other = coll.gameObject.transform.parent.gameObject;
            nav.SetWaypoint(other.gameObject.GetComponent<Waypoint>().nextWaypoint);
        }
        else if (other.tag == "EndPoint")
        {
            mana.currentMana -= 2 * health;
            other = coll.gameObject;
            nav.SetWaypoint(nav.firstWaypoint);
            gameObject.transform.position = nav.spawnPos;
        }
        else if (other.tag == "Tower")
        {
            // don't blow up if tower is already disabled
            if (!other.gameObject.GetComponent<Tower_Manager>().canFire) return;
            // decide whether or not to blow up. For our purposes, we will go boom immeadiately
            // also fire off the bomber particle effect
            StartCoroutine("bomberSequence", other.gameObject);
        }
    }

    IEnumerator bomberSequence(GameObject tower)
    {
        isBombing = true;
        // get really close to that tower
        while (Vector3.Distance(transform.position, tower.transform.position) > 2)
        {
            Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, tower.transform.position, nav.speed * Time.deltaTime);
            gameObject.transform.position = movement;
            gameObject.transform.LookAt(tower.transform);
            yield return null;
        }
        Instantiate(explosionSystem, transform.position, Quaternion.Euler(0, 0, 0));
        tower.GetComponent<Tower_Manager>().disableTower();

        // HACKY SOLUTION - send enemy flying upwards to activate OnTriggerExit() in tower
        transform.position = transform.position + new Vector3(0, 1000, 0);
        enemycounter.deregister(gameObject);
        AudioSource sound = GetComponent<AudioSource>();
        sound.mute = false;
        sound.Play();
        while (sound.isPlaying)
            yield return null;
        Destroy(gameObject);
    }
}
