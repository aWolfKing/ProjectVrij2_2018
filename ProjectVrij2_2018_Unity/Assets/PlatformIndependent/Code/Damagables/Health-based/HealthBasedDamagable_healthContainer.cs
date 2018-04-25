using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBasedDamagable_healthContainer : MonoBehaviour {

    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private UnityEngine.Events.UnityEvent onDeath = new UnityEngine.Events.UnityEvent();

    public void TakeDamage(float amount){
        if(health > 0){ 
            health -= amount;
        }
        if(health <= 0){
            onDeath.Invoke();
        }
    }

}
