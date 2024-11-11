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
            Debug.Log("���Ͱ� �׾���.");
            Debug.Log("��带 ȹ���ߴ�");
        }
    }

    void Die()
    {

    }
}
