using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIB_MoveAway : MonobehaviourAIBehaviourNode {

    [SerializeField] private MonobehaviourAIBehaviourNode attackNode = null;
    [SerializeField] private MonobehaviourAIBehaviourNode shriekNode = null;



    public override IEnumerator ExecuteNode(MonobehaviourAIBehaviourGraph caller) {

        if(CheckDistanceToPlayer() >= 5f){
            yield return this.shriekNode.ExecuteNode(caller);
        }
        else{
            if(!this.CanMoveAway()){
                caller.MarkAsLastNodeCall(this.attackNode);
                yield return this.attackNode.ExecuteNode(caller);
            }
        }

        caller.MarkAsLastNodeCall(this);
        yield return null;
    }



    private float CheckDistanceToPlayer(){
        return 0;
    }

    private bool CanMoveAway(){
        return false;
    }

}

