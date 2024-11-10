using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Idamgeable
{
    void TakeDamage(int damage);
}

public class Monster : MonoBehaviour, Idamgeable
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        
    }
}
