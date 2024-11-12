using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameData
{
    // �������� ����
    public int curentMonsterNumber;
    public int stage;
    public int gold;

    // �÷��̾� ����
    public float autoClickTime;
    public int damage;
    public float critical;

    public GameData()
    {
        curentMonsterNumber = 0;
        stage = 1;
        gold = 500;
        autoClickTime = 1;
        damage = 1;
        critical = 0;
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
        Debug.Log("JSON ���� �Ϸ�");
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
            Debug.Log("JSON �ε� �� ���� �߻�" + e.Message);
        }

        return new GameData();
    }

    public void UpdateGameData(int cutMonsterNum, int stageNum, int goldNum, float autoClickNum, int damageNum, float criticalNum)
    {
        gameData.damage = damageNum;
        gameData.critical = criticalNum;
        gameData.autoClickTime = autoClickNum;
        gameData.curentMonsterNumber = cutMonsterNum;
        gameData.stage = stageNum;
        gameData.gold = goldNum;
    }
}
