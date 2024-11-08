using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;

    private Action clickEvent;
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        clickEvent += Attack;
    }

    void Attack()
    {
       Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
       Debug.Log("클릭클릭");
       if (hit)
       {
           //TODO: 추후 애니메이션과 특정 행동 추가 예정
       }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            clickEvent?.Invoke();
        }
    }
    
}
