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
            PlayerManager.Instance.Player.clickEvent?.Invoke();
            yield return new WaitForSeconds(1);
        }
    }
}
