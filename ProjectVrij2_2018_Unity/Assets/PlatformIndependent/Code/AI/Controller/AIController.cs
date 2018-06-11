using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIController : MonoBehaviour {

    [SerializeField] private Transform target = null;
    [SerializeField] private Animator animator = null;
    private int anim_heavyAttack = 0,
                anim_lightAttack = 0;
    [SerializeField] private float  damage_heavyAttack = 50f,
                                    damage_lightAttack = 20f;
    [SerializeField] private Collider   hitbox_meleeAttack = null;
    [SerializeField] private Collider   heavy_attack_projectile = null;
    [SerializeField] private UnityEngine.AI.NavMeshAgent navMeshAgent = null;
    [SerializeField] private float projectileSpeed = 9;

    private Collider hitbox_projectileAttack = null;


    private void Awake() {
        this.anim_heavyAttack = Animator.StringToHash("HeavyAttack");
        this.anim_lightAttack = Animator.StringToHash("LightAttack");
    }

    private void FixedUpdate() {
        this.animator.SetBool("isMoving", this.navMeshAgent.velocity.magnitude > 0.1f);
    }


   
    public IEnumerator MeleeAttack(){
        float duration = 2.4f;
        float windup = 1f;
        float endLag = 0.5f;

        this.animator.Play(this.anim_lightAttack);

        //yield return new WaitForSeconds(windup);


        var fixedWait = new WaitForFixedUpdate();


        float w = 0;
        do {
            this.animator.transform.rotation = Quaternion.RotateTowards(this.animator.transform.rotation, Quaternion.LookRotation((this.target.position - this.animator.transform.position).normalized, Vector3.up), 360 * Time.fixedDeltaTime);
            yield return fixedWait;
            w += Time.fixedDeltaTime;
        }
        while(w < windup);


        float t = duration - windup;

        Damager damager = new Damager(Damager.DamageCause.Enemy | Damager.DamageCause.Melee, this.damage_lightAttack);

        do {
            if(t <= 0) { break; }

            Damager.Current = damager;
            HitboxString_basedDamagable.SendHit(this.hitbox_meleeAttack);

            yield return new WaitForFixedUpdate();
            t -= Time.fixedDeltaTime;
        }
        while(true);

        damager.InflictStoredDamage();


        yield return new WaitForSeconds(endLag);
    }

    public IEnumerator ProjectileAttack(){
        float duration = 3f;
        float windup = 1.7f;
        float endLag = 1.35f;

        this.animator.Play(this.anim_heavyAttack);

        //yield return new WaitForSeconds(windup);

        var fixedWait = new WaitForFixedUpdate();


        float w = 0;
        do {
            this.animator.transform.rotation = Quaternion.RotateTowards(this.animator.transform.rotation, Quaternion.LookRotation((this.target.position - this.animator.transform.position).normalized, Vector3.up), 360 * Time.fixedDeltaTime);
            yield return fixedWait;
            w += Time.fixedDeltaTime;
        }
        while(w < windup);


        GameObject projectile = GameObject.Instantiate(this.heavy_attack_projectile.gameObject);
        projectile.transform.position = this.heavy_attack_projectile.transform.position;
        projectile.transform.localScale = this.heavy_attack_projectile.transform.lossyScale;
        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody>().velocity = Quaternion.Euler(this.animator.transform.right * -30) * this.animator.transform.forward * this.projectileSpeed;
        this.hitbox_projectileAttack = projectile.GetComponent<Collider>();


        float t = duration - windup;

        Damager damager = new Damager(Damager.DamageCause.Enemy | Damager.DamageCause.Ranged, this.damage_heavyAttack);


        do {
            if(t <= 0) { break; }

            Damager.Current = damager;
            HitboxString_basedDamagable.SendHit(this.hitbox_projectileAttack);

            yield return new WaitForFixedUpdate();
            t -= Time.fixedDeltaTime;
        }
        while(true);

        damager.InflictStoredDamage();


        yield return new WaitForSeconds(endLag);
        Destroy(projectile);

    }

}

