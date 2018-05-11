using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour {

    [SerializeField] private bool attack = false;
    private bool canAttack = true;

    [SerializeField] private float  minAngle = -15f,
                                    maxAngle = 100f;

    [SerializeField] private Collider hitBox = null;
    [SerializeField] private Transform root = null;

    private Damager damager = null;


    private void Update() {
        if(this.attack){
            this.attack = false;
            Attack();
        }
    }

    public void Attack(){
        if(this.canAttack){
            canAttack = false;
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine(){

        if(this.damager == null){
            this.damager = new Damager(Damager.DamageCause.Player | Damager.DamageCause.Melee, 40f);
        }

        damager.ClearStoredDamage();

        var waitForFixedUpdate = new WaitForFixedUpdate();
        float f = 0;
        do{
            bool doEnd = false;
            if(f >= 1f){
                f = 1f;
                doEnd = true;
            }

            root.rotation = Quaternion.Lerp(Quaternion.Euler(0, this.minAngle, 0), Quaternion.Euler(0, this.maxAngle, 0), f);
            Damager.Current = this.damager;
            HitboxString_basedDamagable.SendHit(this.hitBox);

            f += Time.fixedDeltaTime;
            yield return waitForFixedUpdate;
            if(doEnd){
                break;
            }
        }
        while(true);

        damager.InflictStoredDamage();

        yield return new WaitForEndOfFrame();
        canAttack = true;
    }

}
