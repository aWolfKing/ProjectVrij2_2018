using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthbarSlide : MonoBehaviour {

    [SerializeField] private HealthBasedDamagable_healthContainer health = null;
    [SerializeField] private Slider slider = null;


    private void Update() {

        this.slider.value = Mathf.MoveTowards(this.slider.value, 1f/this.health.MaxHealth*this.health.CurrentHealth, Time.deltaTime);

    }

}

