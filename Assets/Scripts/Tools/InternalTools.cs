using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class InternalTools
{
    public static IEnumerator DoActionDelayed(Action action, float dalay = 0f)
    {
        if (action == null)
            return null;

        var Coroutine = ActionDelayed(action, dalay);
        MainApp.Instance.StartCoroutine(Coroutine);

        return Coroutine;       
    }
    private static IEnumerator ActionDelayed(Action action, float dalay = 0f)
    {
        yield return new WaitForSeconds(dalay);
        action.Invoke();
    }
}
