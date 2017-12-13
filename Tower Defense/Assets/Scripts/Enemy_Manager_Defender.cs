using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager_Defender : MonoBehaviour
{
    TextStuff textyPoo;
    public GameObject waypoint;
    public float speed = 2.0f;
    public float health = 300f;
    public float manaForKill = 10f;

    [HideInInspector]
    public float damageMultiplier = 1f;

    [HideInInspector]
    public GameObject firstWaypoint;

    [HideInInspector]
    public Vector3 spawnPos;

    [HideInInspector]
    public bool isSlowed;

    //private Vector3 lastPos;

    private List<GameObject> nearbyTowers;
    private Mana mana;
    private EnemyCounter enemycounter;
    private SM_tower_defense sm;

    private bool deathStarted = false;

    // Use this for initialization
    void Start()
    {
        textyPoo = FindObjectOfType<TextStuff>();
        nearbyTowers = new List<GameObject>();
        mana = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Mana>();
        enemycounter = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EnemyCounter>();
        sm = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SM_tower_defense>();
        enemycounter.register(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //lastPos = gameObject.transform.position;
        if (waypoint != null)
        {
            Vector3 movement = Vector3.MoveTowards(gameObject.transform.position, waypoint.transform.position, speed * Time.deltaTime);
            gameObject.transform.position = movement;
            gameObject.transform.LookAt(waypoint.transform);
        }
        if (health <= 0)
        {
            if (!deathStarted)
                StartCoroutine("deathSequence");
        }

    }

    IEnumerator deathSequence()
    {
        deathStarted = true;
        // GOOD SOLUTION - send enemy flying upwards to activate OnTriggerExit() in tower
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

    IEnumerator WaitThenMove(float seconds, GameObject object_)
    {
        yield return new WaitForSeconds(seconds);
        waypoint = object_.gameObject.GetComponent<Waypoint>().nextWaypoint;
        yield return null;
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
        if (other.tag == "Waypoint")
        {
            other = coll.gameObject.transform.parent.gameObject;
            StartCoroutine(WaitThenMove(8, other));
        }
        else if (other.tag == "EndPoint")
        {
            mana.currentMana -= 2 * health;
            textyPoo.ResetTo("-" + (2 * health));
            other = coll.gameObject;
            waypoint = firstWaypoint;
            gameObject.transform.position = spawnPos;
        }
    }
}
