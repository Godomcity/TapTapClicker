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
}
