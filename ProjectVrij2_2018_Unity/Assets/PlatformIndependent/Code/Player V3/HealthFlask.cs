using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthFlask : MonoBehaviour{

    [SerializeField] private HealthBasedDamagable_healthContainer health = null;
    [SerializeField] private Image flaskUI = null;
    [SerializeField] private float healAmount = 30;
    private bool canUse = true;

    public void Use(){
        if(this.canUse && this.health.CurrentHealth < this.health.MaxHealth){
            this.canUse = false;
            this.health.TakeDamage(-Mathf.Clamp(this.healAmount, 0, this.health.MaxHealth - this.health.CurrentHealth));
            this.flaskUI.enabled = false;
        }
    }

}
