using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactable_stringBased : MonoBehaviour {

    [SerializeField] private List<Damager.DamageCause> interactOnAny = new List<Damager.DamageCause>();

    [SerializeField] private UnityEvent onInteract = null;

    [SerializeField] private float coolDown = 0.5f;
    private bool canInteract = true;



    public void Interact(){
        if(this.canInteract && ShouldInteract(Damager.Current)) {
            this.onInteract.Invoke();
            if(this.coolDown > 0){
                StartCoroutine(CooldownCoroutine());
            }
        }
    }


    private bool ShouldInteract(Damager damager){
        var cause = damager.GetCause();
        foreach(var interactOn in this.interactOnAny){
            if((cause & interactOn) == interactOn){
                return true;
            }
        }
        return false;
    }


    private IEnumerator CooldownCoroutine(){
        this.canInteract = false;
        yield return new WaitForSeconds(this.coolDown);
        this.canInteract = true;
    }



    public void PrintInteraction(){
        MonoBehaviour.print("Interaction");
    }

}
