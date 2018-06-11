using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour{

    [SerializeField] private string onDeathScene = "mainMenu";
    [SerializeField] private Animator animator = null;
    [SerializeField] private List<GameObject> toDisable = new List<GameObject>();



    public void Die(){
        StartCoroutine(DieCoroutine());
    }



    private IEnumerator DieCoroutine(){

        foreach(var o in this.toDisable){
            o.SetActive(false);
        }

        this.animator.Play("Death");
        yield return new WaitForSeconds(2.5f);
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(this.onDeathScene);
    }

}

