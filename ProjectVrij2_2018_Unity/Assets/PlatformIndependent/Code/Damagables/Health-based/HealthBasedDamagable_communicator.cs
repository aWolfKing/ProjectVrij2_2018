using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBasedDamagable_communicator : MonoBehaviour {

    [SerializeField] private HealthBasedDamagable_healthContainer healthContainer = null;

    [SerializeField] private float damageMultiplier = 1f;

    //[SerializeField] private Damager.DamageCause immumeTo = Damager.DamageCause.Enemy;
    [SerializeField] private List<Damager.DamageCause> immumeTo = new List<Damager.DamageCause>() { Damager.DamageCause.Enemy};

    public void TakeHit(){
        if(!IsImmumeTo(Damager.Current.GetCause())){ //(Damager.Current.GetCause() & immumeTo) != immumeTo){
            //healthContainer.TakeDamage(Damager.Current.Damage * damageMultiplier);
            Damager.Current.StoreDamageDone(healthContainer, Damager.Current.Damage * this.damageMultiplier);
        }
    }

    public bool IsImmumeTo(Damager.DamageCause cause){
        for(int i=0; i<immumeTo.Count; i++){
            if((cause & immumeTo[i]) == immumeTo[i]){
                return true;
            }
        }
        return false;
    }

}
