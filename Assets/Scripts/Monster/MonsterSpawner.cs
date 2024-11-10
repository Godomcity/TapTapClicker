using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public MonsterData data;

    private void Start()
    {
        Instantiate(data.monsterPrefabs, GameManager.Instance.Monster.transform);
    }
}
