using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour, IDamgeable
{
    private MonsterData data;
    Animator animator;
    private int health;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        data = GameManager.Instance.Stage.monsterData;
        health = data.health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (data.health <= 0)
        {
            Die();
            Debug.Log("¸ó½ºÅÍ°¡ Á×¾ú´Ù.");
            Debug.Log("°ñµå¸¦ È¹µæÇß´Ù");
        }
    }

    void Die()
    {

    }
}
