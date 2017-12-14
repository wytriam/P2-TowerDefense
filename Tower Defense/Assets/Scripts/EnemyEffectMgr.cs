using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectMgr : MonoBehaviour
{
    [HideInInspector]
    public float damageMultiplier = 1f;

    [HideInInspector]
    public bool dotOn = false;

    [HideInInspector]
    public bool isSlowed = false;
}
