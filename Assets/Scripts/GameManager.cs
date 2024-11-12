using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamgeable
{
    void TakeDamage(int damage);
}

public class GameManager : MonoBehaviour
{
    public Action deathMonster;
    public Action useGold;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }

    private Player player;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    private Monster monster;

    public Monster Monster
    {
        get { return monster; }
        set {  monster = value; }
    }

    private StageManager stage;

    public StageManager Stage
    { 
        get { return stage; } 
        set { stage = value; } 
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance == this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        LoadGame();
    }

    void LoadGame()
    {
        GameData gameData = SaveLoadManager.Instance.LoadAsJson();
        if (gameData != null)
        {
            if (player != null)
            {
                player.controller.damage = gameData.damage;
                player.controller.critical = gameData.critical;
                player.controller.autoClickTime = gameData.autoClickTime;
            }
            else if (stage != null)
            {
                stage.curentMonsterNumber = gameData.curentMonsterNumber;
                stage.gold = gameData.gold;
                stage.stage = gameData.stage;
            }
        }
        else
        {
            Debug.LogWarning("저장된 데이터가 없습니다. 새 게임을 시작합니다.");
            StartNewGame();
        }
    }

    private void StartNewGame()
    {
        SaveGame();
    }

    void SaveGame()
    {
        SaveLoadManager.Instance.UpdateGameData(0, 1, 0, 1, 1, 0);
        SaveLoadManager.Instance.SaveAsJson();
    }
}
