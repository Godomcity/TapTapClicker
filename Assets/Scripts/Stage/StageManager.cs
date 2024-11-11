using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public MonsterData[] monsters;
    public MonsterData monsterData;
    private GameObject rndMonster;
    private int curentMonsterNumber;
    private int maxMonsterNumber = 10;
    private int stage = 1;
    public int gold = 500;

    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] TextMeshProUGUI monsterNumberText;
    [SerializeField] TextMeshProUGUI goldText;

    private void Awake()
    {
        GameManager.Instance.Stage = this;
        GameManager.Instance.deathMonster += AddMonsterNumber;
        GameManager.Instance.useGold += UpdateGold;
    }

    private void Start()
    {
        MonsterSpawn();
        stageText.text = $"Stage : {stage.ToString()}";
        monsterNumberText.text = $"{curentMonsterNumber} / {maxMonsterNumber}";
        goldText.text = $"Gold : {gold.ToString()}G";
    }

    void MonsterSpawn()
    {
        int rndMonsterIndex = Random.Range(0, monsters.Length);
        monsterData = monsters[rndMonsterIndex];
        rndMonster = monsters[rndMonsterIndex].monsterPrefabs;

        GameObject go = Instantiate(rndMonster, GameManager.Instance.Monster.transform);
        GameManager.Instance.Monster.monsterController = go.GetComponent<MonsterController>();
    }

    void Init()
    {
        curentMonsterNumber = 0;

        foreach (MonsterData monster in monsters)
        {
            monster.health *= stage;
            monster.getGold *= stage;
        }
    }

    void AddMonsterNumber()
    {
        if(curentMonsterNumber <= maxMonsterNumber - 1)
        {
            GameManager.Instance.Monster.monsterController = null;
            curentMonsterNumber++;
            monsterNumberText.text = $"{curentMonsterNumber} / {maxMonsterNumber}";
            goldText.text = $"Gold : {gold.ToString()}";
            MonsterSpawn();
        }
        else
        {
            stage++;
            stageText.text = $"Stage : {stage.ToString()}";
            MonsterSpawn();
            Init();
            goldText.text = $"Gold : {gold.ToString()}";
            monsterNumberText.text = $"{curentMonsterNumber} / {maxMonsterNumber}";
        }
    }

    void UpdateGold()
    {
        goldText.text = $"Gold : {gold.ToString()}G";
    }
}
