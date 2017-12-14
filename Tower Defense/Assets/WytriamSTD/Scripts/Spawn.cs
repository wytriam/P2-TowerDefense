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
        public bool spawnRandomly;
        public float randomRange;
        public Vector3 spawnScale = Vector3.one;

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

        public GameObject spawn()
        {
            GameObject go;
            if (spawnRandomly)
            {
                Vector3 randomSpawn = new Vector3(transform.position.x + Random.Range(-randomRange, randomRange),
                                                    transform.position.y + Random.Range(-randomRange, randomRange),
                                                    transform.position.z + Random.Range(-randomRange, randomRange));
                go = Instantiate(spawnPrefab, randomSpawn, gameObject.transform.rotation);
                go.transform.localScale = spawnScale;
                return go;
            }
            go = Instantiate(spawnPrefab, gameObject.transform.position, gameObject.transform.rotation);
            go.transform.localScale = spawnScale;
            return go;
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
