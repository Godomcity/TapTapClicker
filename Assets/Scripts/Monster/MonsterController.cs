using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour, IDamgeable
{
    private MonsterData data;
    Animator animator;
    int health;

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
        animator.SetTrigger("Hit");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        animator.SetTrigger("Death");
        GameManager.Instance.Stage.gold += data.getGold;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
        GameManager.Instance.deathMonster?.Invoke();
    }
}
