using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoButton : MonoBehaviour
{
    private Coroutine coroutine;
    
    public void AutoClickButton()
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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            Debug.Log("클릭클릭");
            yield return new WaitForSeconds(1);
        }
    }
}
