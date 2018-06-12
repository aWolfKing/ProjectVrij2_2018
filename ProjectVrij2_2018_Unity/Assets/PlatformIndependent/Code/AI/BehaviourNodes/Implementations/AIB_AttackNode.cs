using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIB_AttackNode : MonobehaviourAIBehaviourNode {

    [SerializeField] private AIController aIController = null;

    //[SerializeField] private Transform target = null;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    [SerializeField] private float  targetPosDist = 0.5f,
                                    targetAttackDist = 1f,
                                    target_projectileAttackDist_min = 0f,
                                    target_projectileAttackDist_max = 0f;
    [SerializeField] private float walkTime = 0.5f;

    public override IEnumerator ExecuteNode(MonobehaviourAIBehaviourGraph caller) {
        //MonoBehaviour.print("Attack node");
        NavMeshHit navMeshHit;
        float time = 0;
        var wait = new WaitForSeconds(1f/30f);
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        do {
            if(NavMesh.SamplePosition(/*this.target.position*/ PlayerControl.CharacterPosition + (this.navMeshAgent.transform.position - /*this.target.position*/ PlayerControl.CharacterPosition).normalized, out navMeshHit, this.targetPosDist, 1 << NavMesh.GetAreaFromName("Walkable"))) {
                this.navMeshAgent.SetDestination(navMeshHit.position);
            }
            //time += 1f/30f;
            yield return wait;
            float dist = Vector3.Distance(/*this.target.position*/ PlayerControl.CharacterPosition, this.navMeshAgent.transform.position);
            if(dist <= this.targetAttackDist){
                this.navMeshAgent.SetDestination(this.navMeshAgent.transform.position);
                yield return this.aIController.MeleeAttack();
            }
            else if(dist <= this.target_projectileAttackDist_max && dist > this.target_projectileAttackDist_min) {
                this.navMeshAgent.SetDestination(this.navMeshAgent.transform.position);
                yield return this.aIController.ProjectileAttack();
            }
            if(stopwatch.ElapsedMilliseconds * 1000 >= this.walkTime){
                break;
            }
        }
        while(true);
    }

}

