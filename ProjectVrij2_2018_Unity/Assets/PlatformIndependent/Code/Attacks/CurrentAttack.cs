using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrentAttack {

    private static UnityEvent onStart = new UnityEvent();
    private static UnityEvent onEnd = new UnityEvent();

    public static void ListenToOnAttackStart(UnityAction onStart){
        CurrentAttack.onStart.AddListener(onStart);
    }
    public static void ListenToOnAttackEnd(UnityAction onEnd){
        CurrentAttack.onEnd.AddListener(onEnd);
    }
    public static void StopListening(UnityAction action){
        CurrentAttack.onStart.RemoveListener(action);
        CurrentAttack.onEnd.RemoveListener(action);
    }
 
}
