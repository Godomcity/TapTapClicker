using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterController monsterController;

    private void Awake()
    {
        GameManager.Instance.Monster = this;
    }

    private void Start()
    {
        monsterController = GetComponentInChildren<MonsterController>();
    }
}
