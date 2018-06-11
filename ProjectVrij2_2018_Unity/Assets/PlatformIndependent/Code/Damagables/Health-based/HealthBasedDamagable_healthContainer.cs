using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBasedDamagable_healthContainer : MonoBehaviour {

    [SerializeField] protected float health = 100;
    [SerializeField] protected float maxHealth = 100;

    [SerializeField] protected UnityEngine.Events.UnityEvent onDeath = new UnityEngine.Events.UnityEvent();
    protected bool didDie = false;


    public float CurrentHealth{
        get{
            return this.health;
        }
    }

    public float MaxHealth{
        get{
            return this.maxHealth;
        }
    }


    public virtual void TakeDamage(float amount){
        if(this.health > 0){ 
            this.health -= amount;
        }
        if(this.health <= 0 && !this.didDie){
            this.didDie = true;
            this.onDeath.Invoke();
        }
    }

}
