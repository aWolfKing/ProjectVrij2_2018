using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringHitboxTesting : MonoBehaviour {

    [SerializeField] private bool startTest = false;
    [SerializeField] private Collider hitBox = null;

    private void FixedUpdate() {
        if(startTest){
            startTest = false;
            HitboxString_basedDamagable.SendHit(hitBox);
        }
    }

    public void ConfirmHit(){
        MonoBehaviour.print("Hit");
    }	

}
