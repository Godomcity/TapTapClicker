using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour, IDamgeable
{
    public MonsterData data;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        data.health -= damage;
        // TODO : 몬스터 맞는 애니메이션 추가
        if (data.health <= 0)
        {
            // TODO : 몬스터 죽는 애니메이션 추가
            // TODO : 몬스터가 죽으면 골드 획득
            Debug.Log("몬스터가 죽었다.");
            Debug.Log("골드를 획득했다");
        }
    }
}
