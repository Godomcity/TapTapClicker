using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public Action clickEvent;
    public Queue<Action> actionQueue = new Queue<Action>();
    
    private void Awake()
    {
        controller = GetComponent<PlayerController>(); 
        PlayerManager.Instance.Player = this;
    }
}
