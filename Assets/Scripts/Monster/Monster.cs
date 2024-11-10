using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;
    public MonsterController monsterController;

    private void Awake()
    {
        GameManager.Instance.Monster = this;
        monsterSpawner = GetComponent<MonsterSpawner>();
    }

    private void Start()
    {
        monsterController = GetComponentInChildren<MonsterController>();
    }
}
