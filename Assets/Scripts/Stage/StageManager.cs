using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public MonsterData[] monsters;
    public MonsterData monsterData;
    private GameObject rndMonster;
    private int curentMonsterNumber;
    private int maxMonsterNumber = 10;
    private int stage = 1;

    private void Awake()
    {
        GameManager.Instance.Stage = this;
    }

    private void Start()
    {
        int rndMonsterIndex = Random.Range(0, monsters.Length);
        monsterData = monsters[rndMonsterIndex];
        rndMonster = monsters[rndMonsterIndex].monsterPrefabs;

        Instantiate(rndMonster, GameManager.Instance.Monster.transform);
    }

    void Init()
    {
        curentMonsterNumber = 0;

        foreach (MonsterData monster in monsters)
        {
            monster.health *= stage;
        }
    }
}
