using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster_spawner : MonoBehaviour {

    [SerializeField]private float activateRadius = 15f;
    [SerializeField]private GameObject tPose = null;
    [SerializeField]private Animator animator = null;
    [SerializeField]private NavMeshAgent navMeshAgent = null;
    [SerializeField]private List<GameObject> toEnable = new List<GameObject>();



    private void Awake() {
        this.navMeshAgent.enabled = false;
        foreach(var o in this.toEnable){
            o.SetActive(false);
        }
        Spawn();
    }


    public void Spawn(){
        StartCoroutine(SpawnCoroutine());    
    }


    private IEnumerator SpawnCoroutine(){
        this.tPose.SetActive(false);
        var wait = new WaitForFixedUpdate();
        do{
            yield return wait;
            if(Vector3.Distance(this.tPose.transform.position, PlayerControl.CharacterPosition) <= this.activateRadius){
                break;
            }
        }
        while(true);
        this.tPose.SetActive(true);
        this.animator.Play("Spawn");
        yield return new WaitForSeconds(4.4f);
        this.navMeshAgent.enabled = true;
        foreach(var o in this.toEnable){
            o.SetActive(true);
        }
    }

}
