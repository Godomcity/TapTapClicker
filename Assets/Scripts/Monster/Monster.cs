using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    private void Awake()
    {
        GameManager.Instance.Monster = this;
    }
}
