using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private Coroutine coroutine;

    public LayerMask layerMask;

    public float autoClickTime = 1;
    public int damage = 1;
    public float critical = 0;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.Player.clickEvent += Attack;
    }

    void Attack()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3 (mousePosition.x, mousePosition.y, 10f));
       
        if (Physics2D.Raycast(mousePos, Vector2.zero, 0f, layerMask))
        {
            //TODO: 추후 애니메이션과 특정 행동 추가 예정
            Debug.Log("클릭클릭");
            animator.SetTrigger("Attack");
            GameManager.Instance.Monster.monsterController.TakeDamage(damage);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            GameManager.Instance.Player.clickEvent?.Invoke();
        }
    }
    
    public void AutoAttack()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        else
        {
            coroutine = StartCoroutine(AutoClick());
        }
    }
    
    IEnumerator AutoClick()
    {
        while (true)
        {
            GameManager.Instance.Player.clickEvent?.Invoke();
            yield return new WaitForSeconds(autoClickTime);
        }
    }
}
