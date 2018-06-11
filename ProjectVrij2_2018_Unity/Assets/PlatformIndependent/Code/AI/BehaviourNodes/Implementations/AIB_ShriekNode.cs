using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIB_ShriekNode : MonobehaviourAIBehaviourNode {

    [SerializeField] private AIB_AttackNode attackNode = null;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    private bool hasShrieked = false;
    [SerializeField] private Transform shriekPosition = null;

    public override IEnumerator ExecuteNode(MonobehaviourAIBehaviourGraph caller) {
        if(!this.hasShrieked){
            this.hasShrieked = true;

            if(this.shriekPosition != null){
                this.navMeshAgent.SetDestination(this.shriekPosition.position);
                var wait = new WaitForSeconds(0.10f);
                do {
                    if(Vector3.Distance(this.shriekPosition.position, this.navMeshAgent.transform.position) <= 1f){
                        this.navMeshAgent.SetDestination(this.navMeshAgent.transform.position);
                        MonoBehaviour.print("TargetShriek!");
                    }
                    yield return wait;
                }
                while(true);
            }
            else{ 
                this.navMeshAgent.SetDestination(this.navMeshAgent.transform.position);
                MonoBehaviour.print("Shriek!");
            }



            yield return new WaitForSeconds(3f);
            caller.MarkAsLastNodeCall(this.attackNode);
            this.attackNode.ExecuteNode(caller);
        }
        yield return 0;
    }

}

