using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthFlask : MonoBehaviour{

    [SerializeField] private HealthBasedDamagable_healthContainer health = null;
    [SerializeField] private Image flaskUI = null;
    [SerializeField] private Text text = null;
    [SerializeField] private float healAmount = 30;
    [SerializeField] private int amount = 1;



    private void Start() {
        this.UpdateUI();
    }

    public void Use(){
        if(this.amount > 0 && this.health.CurrentHealth < this.health.MaxHealth){
            this.amount--;
            this.health.TakeDamage(-Mathf.Clamp(this.healAmount, 0, this.health.MaxHealth - this.health.CurrentHealth));
            this.text.text = this.amount.ToString();
            if(this.amount == 0){ 
                this.flaskUI.enabled = false;
                this.text.text = "";
            }
        }
    }


    public void UpdateUI(){
        if(this.amount > 0){
            this.text.text = this.amount.ToString();
            this.flaskUI.enabled = true;
        }
        else{
            this.text.text = "";
            this.flaskUI.enabled = false;
        }
    }

}
