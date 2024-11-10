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
        // TODO : ���� �´� �ִϸ��̼� �߰�
        if (data.health <= 0)
        {
            // TODO : ���� �״� �ִϸ��̼� �߰�
            // TODO : ���Ͱ� ������ ��� ȹ��
            Debug.Log("���Ͱ� �׾���.");
            Debug.Log("��带 ȹ���ߴ�");
        }
    }
}
