using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WytriamSTD
{
    public class Spawn : MonoBehaviour
    {
        public GameObject spawnPrefab;
        public bool spawnAtStart;
        public bool spawnWhenTriggered;

        // Use this for initialization
        void Start()
        {
            if (spawnAtStart)
                spawn();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void spawn()
        {
            GameObject go = Instantiate(spawnPrefab, gameObject.transform.position, gameObject.transform.rotation);
            go.transform.localScale = gameObject.transform.localScale;
        }

        public Vector3 getSpawnPos()
        {
            return gameObject.transform.position;
        }

        public Quaternion getSpawnRot()
        {
            return gameObject.transform.rotation;
        }

        public void setSpawnPrefab(GameObject _spawnPrefab)
        {
            spawnPrefab = _spawnPrefab;
        }
    }
}
