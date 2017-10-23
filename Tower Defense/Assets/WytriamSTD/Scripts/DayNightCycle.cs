using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WytriamSTD
{
    public class DayNightCycle : MonoBehaviour
    {

        public Light sun;
        private Quaternion sunRotation;
        public float speed;

        // Use this for initialization
        void Start()
        {
            sunRotation = sun.transform.rotation;
        }

        void FixedUpdate()
        {
            sun.transform.rotation = sunRotation;
            sunRotation *= Quaternion.Euler(speed * Time.fixedDeltaTime, 0, 0);
        }
    }
}