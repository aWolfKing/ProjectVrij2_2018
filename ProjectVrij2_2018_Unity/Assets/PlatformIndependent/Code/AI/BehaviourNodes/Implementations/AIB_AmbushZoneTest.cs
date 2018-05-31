using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIB_AmbushZoneTest : MonobehaviourAIBehaviourNode {

    [SerializeField] private MonobehaviourAIBehaviourNode attackNode = null;



    public override IEnumerator ExecuteNode(MonobehaviourAIBehaviourGraph caller) {

        if(this.PlayerIsInAmbushZone() || this.PlayerIsInLineOfSight()){
            yield return this.attackNode.ExecuteNode(caller);
        }

        caller.MarkAsLastNodeCall(null);
        yield return null;
    }



    private bool PlayerIsInLineOfSight(){
        return false;
    }

    private bool PlayerIsInAmbushZone(){
        return true;
    }

}

