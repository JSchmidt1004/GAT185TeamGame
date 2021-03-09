using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShipData", menuName = "GameData/Enemy")]
public class EnemyShipData : ScriptableObject
{
    public float speed = 5.0f;
    public float turnRate = 5.0f;
    public float fireRange = 20.0f;
    public string targetTag;
}
