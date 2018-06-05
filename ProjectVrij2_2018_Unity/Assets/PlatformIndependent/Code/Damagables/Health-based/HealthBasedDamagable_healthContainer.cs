using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBasedDamagable_healthContainer : MonoBehaviour {

    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private UnityEngine.Events.UnityEvent onDeath = new UnityEngine.Events.UnityEvent();


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


    public void TakeDamage(float amount){
        if(this.health > 0){ 
            this.health -= amount;
        }
        if(this.health <= 0){
            this.onDeath.Invoke();
        }
    }

}
