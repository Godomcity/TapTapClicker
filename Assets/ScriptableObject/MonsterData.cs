using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "NewMonster")]

public class MonsterData : ScriptableObject
{
    [Header("MonsterInfo")]
    public int health;
    public GameObject monsterPrefabs;
    public int getGold;
}
