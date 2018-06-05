using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIB_HealthCheck : MonobehaviourAIBehaviourNode {

    [SerializeField] private HealthBasedDamagable_healthContainer healthContainer = null;

    [SerializeField] private MonobehaviourAIBehaviourNode   ambushZoneTestNode = null,
                                                            moveAwayFromPlayerNode = null;

    public override IEnumerator ExecuteNode(MonobehaviourAIBehaviourGraph caller) {
        if(this.healthContainer.CurrentHealth > 0){
            ///> 15% health
            if(1f/this.healthContainer.MaxHealth*this.healthContainer.CurrentHealth >= 0.15){
                yield return this.ambushZoneTestNode.ExecuteNode(caller);
            }
            else{
                yield return this.moveAwayFromPlayerNode.ExecuteNode(caller);
            }
        }
        else{
            caller.MarkAsLastNodeCall(null);
            yield return null;
        }
    }

}

