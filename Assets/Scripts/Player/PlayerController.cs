using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private Coroutine coroutine;

    public float autoClickTime = 1;
    public int damage = 1;
    public float critical = 0;
    public LayerMask layerMask;
    public GameObject particleObj;
    private ParticleSystem particle;

    bool autoAttack = false;

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
       if (!autoAttack)
       {
            if (Physics2D.Raycast(mousePos, Vector2.zero, 0f, layerMask))
            {
                EventBus.Publish("Sword");
                animator.SetTrigger("Attack");
                GameObject go = Instantiate(particleObj);
                particle = go.GetComponent<ParticleSystem>();
                particle.Play();
                go.transform.position = mousePos;
                int rndValue = UnityEngine.Random.Range(0, 100);

                if (rndValue < critical)
                {
                    GameManager.Instance.Monster.monsterController.TakeDamage(damage * 2);
                }
                else
                {
                    GameManager.Instance.Monster.monsterController.TakeDamage(damage);
                }
                Destroy(go, 1f);
            }
       }
        else
        {
            EventBus.Publish("Sword");
            animator.SetTrigger("Attack");

            int rndValue = UnityEngine.Random.Range(0, 100);

            if (rndValue < critical)
            {
                GameManager.Instance.Monster.monsterController.TakeDamage(damage * 2);
            }
            else
            {
                GameManager.Instance.Monster.monsterController.TakeDamage(damage);
            }
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
        EventBus.Publish("Click");
        autoAttack = !autoAttack;
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
