using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MonobehaviourAIBehaviourGraph : MonoBehaviour {

    [NonSerialized] private MonobehaviourAIBehaviourNode lastNode = null;
    [SerializeField] private MonobehaviourAIBehaviourNode startNode = null;



    private void OnEnable() {
        StartCoroutine(TreeCoroutine());
    }

    private void OnDisable() {
        StopCoroutine(TreeCoroutine());
    }

    private IEnumerator TreeCoroutine(){
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        yield return this.startNode.ExecuteNode(this);
        do {
            if(stopwatch.ElapsedMilliseconds >= 33.33f){
                stopwatch.Reset();
                stopwatch.Start();
                if(this.lastNode != null){
                    yield return this.lastNode.ExecuteNode(this);
                }
                else if(this.startNode != null){ 
                    yield return this.startNode.ExecuteNode(this);
                }
            }
        }
        while(true);
    }



    public void StartFromBegin(){
        if(this.startNode != null){
            StartCoroutine(this.startNode.ExecuteNode(this));
        }
    }

    public void StartFromLastNode(){
        if(this.lastNode != null){
            StartCoroutine(this.lastNode.ExecuteNode(this));
        }
    }

    public void MarkAsLastNodeCall(MonobehaviourAIBehaviourNode node){
        this.lastNode = node;
    }

}

