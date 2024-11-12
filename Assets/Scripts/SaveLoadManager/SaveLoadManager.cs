using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameData
{
    // 스테이지 정보
    public int curentMonsterNumber;
    public int stage;
    public int gold;

    // 플레이어 정보
    public float autoClickTime;
    public int damage;
    public float critical;

    public int attackUpgrade = 50;
    public int criticalUpgrade = 50;
    public int autoClickUpgrade = 50;

    public GameData()
    {
        curentMonsterNumber = 0;
        stage = 1;
        gold = 500;
        autoClickTime = 1;
        damage = 1;
        critical = 0;
        attackUpgrade = 50;
        criticalUpgrade = 50;
        autoClickUpgrade = 50;
    }

}

public class SaveLoadManager : MonoBehaviour
{
    public string savePath;
    private static SaveLoadManager instance;
    private GameData gameData;

    public static SaveLoadManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("SaveLoadManager").AddComponent<SaveLoadManager>();
            }
            return instance;
        }
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
            if(instance == this)
            {
                Destroy(gameObject);
            }
        }

        savePath = Path.Combine(Application.persistentDataPath, "savedata.dat");
        gameData = new GameData();
    }

    public void SaveAsJson()
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(savePath + ".joson", json);
        Debug.Log("JSON 저장 완료");
    }

    public GameData LoadAsJson()
    {
        try
        {
            string path = savePath + ".joson";
            if(File.Exists(path))
            {
                string json = File.ReadAllText(path);
                gameData = JsonUtility.FromJson<GameData>(json);
                return gameData;
            }
        }
        catch (Exception e)
        {
            Debug.Log("JSON 로드 중 오류 발생" + e.Message);
        }

        return new GameData();
    }

    public void ShopGameData(int attackGold,int criticalGold,int autoClickGold)
    {
        gameData.attackUpgrade = attackGold;
        gameData.criticalUpgrade = criticalGold;
        gameData.autoClickUpgrade = autoClickGold;
    }

    public void PlayerGameData(float autoClickNum, int damageNum, float criticalNum)
    {
        gameData.damage = damageNum;
        gameData.critical = criticalNum;
        gameData.autoClickTime = autoClickNum;
    }

    public void StageGameData(int cutMonsterNum, int stageNum, int goldNum)
    {
        gameData.curentMonsterNumber = cutMonsterNum;
        gameData.stage = stageNum;
        gameData.gold = goldNum;
    }

    public void UpdateGameData(int attackGold, int criticalGold, int autoClickGold, float autoClickNum, int damageNum, float criticalNum, int cutMonsterNum, int stageNum, int goldNum)
    {
        gameData.attackUpgrade = attackGold;
        gameData.criticalUpgrade = criticalGold;
        gameData.autoClickUpgrade = autoClickGold;

        gameData.damage = damageNum;
        gameData.critical = criticalNum;
        gameData.autoClickTime = autoClickNum;

        gameData.curentMonsterNumber = cutMonsterNum;
        gameData.stage = stageNum;
        gameData.gold = goldNum;
    }
}
