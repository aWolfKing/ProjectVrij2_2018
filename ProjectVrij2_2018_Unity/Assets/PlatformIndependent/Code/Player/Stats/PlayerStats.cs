using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour {

    private static PlayerStats _this = null;

    [SerializeField] private float  health = 100,
                                    maxHealth = 100;
    [SerializeField] private UnityEvent onDeath = new UnityEvent();

    [SerializeField] private float  stamina = 100,
                                    maxStamina = 100;

    private void OnEnable() {
        _this = this;
    }

    public static void TakeDamage(float dmg){
        if(_this.health > 0){ 
            _this.health -= dmg;
            if(_this.health <= 0){
                _this.onDeath.Invoke();
            }
        }
    }

}
