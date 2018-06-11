using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Death_despawner : MonoBehaviour {

    [SerializeField]private GameObject toDestroy = null;
    [SerializeField]private UnityEngine.AI.NavMeshAgent navMeshAgent = null;
    [SerializeField]private Animator animator = null;
    [SerializeField]private List<GameObject> disableOnDeath = new List<GameObject>();

    public void Die(){
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine(){
        this.animator.Play("Death");
        this.navMeshAgent.enabled = false;
        foreach(var o in this.disableOnDeath){
            o.SetActive(false);
        }
        yield return new WaitForSeconds(5f);
        Destroy(this.toDestroy);
    }

}

