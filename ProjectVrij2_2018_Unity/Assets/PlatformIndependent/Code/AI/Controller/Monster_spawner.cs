using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster_spawner : MonoBehaviour {

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
        this.animator.Play("Spawn");
        yield return new WaitForSeconds(4.4f);
        this.navMeshAgent.enabled = true;
        foreach(var o in this.toEnable){
            o.SetActive(true);
        }
    }

}
