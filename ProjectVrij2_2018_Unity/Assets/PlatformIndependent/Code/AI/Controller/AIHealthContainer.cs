using System;
using System.Collections.Generic;
using UnityEngine;


public class AIHealthContainer : HealthBasedDamagable_healthContainer{

    //[SerializeField] private Animator animator = null;
    [SerializeField] private AIController aIController = null;



    public override void TakeDamage(float amount) {
        if(this.health > 0) {
            this.health -= amount;
            if(amount > 40 && this.health > 0){
                //this.animator.Play("Stun");
                this.aIController.Stun();
            }
        }
        if(this.health <= 0 && !this.didDie) {
            this.didDie = true;
            this.onDeath.Invoke();
        }
    }


}

