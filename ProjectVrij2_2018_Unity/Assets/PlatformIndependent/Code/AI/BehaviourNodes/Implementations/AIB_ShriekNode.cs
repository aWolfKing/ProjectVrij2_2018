using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIB_ShriekNode : MonobehaviourAIBehaviourNode {

    [SerializeField] private Animator animator = null;
    [SerializeField] private AIB_AttackNode attackNode = null;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    private bool hasShrieked = false;
    //[SerializeField] private Transform shriekPosition = null;

    public override IEnumerator ExecuteNode(MonobehaviourAIBehaviourGraph caller) {
        if(!this.hasShrieked){
            this.hasShrieked = true;

            if(/*this.shriekPosition*/ MonsterShriekSpawn.ShriekTransform != null){
                this.navMeshAgent.SetDestination(/*this.shriekPosition*/ MonsterShriekSpawn.ShriekTransform.position);
                var wait = new WaitForSeconds(0.10f);
                do {
                    if(Vector3.Distance(/*this.shriekPosition*/ MonsterShriekSpawn.ShriekTransform.position, this.navMeshAgent.transform.position) <= 3f){
                        this.navMeshAgent.SetDestination(this.navMeshAgent.transform.position);
                        MonoBehaviour.print("TargetShriek!");
                        break;
                    }
                    yield return wait;
                }
                while(true);
            }
            else{ 
                this.navMeshAgent.SetDestination(this.navMeshAgent.transform.position);
                MonoBehaviour.print("Shriek!");
            }


            float f = 0;
            var waitF = new WaitForFixedUpdate();
            do {
                this.animator.transform.rotation = Quaternion.RotateTowards(this.animator.transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane((MonsterShriekSpawn.SpawnCenter - this.animator.transform.position).normalized, Vector3.up), Vector3.up), 360 * Time.fixedDeltaTime * 2);
                yield return waitF;
                f += Time.fixedDeltaTime;
                if(f >= 0.5f){
                    break;
                }
            }
            while(true);


            this.animator.Play("HeavyAttack");
            yield return new WaitForSeconds(2f);
            MonsterShriekSpawn.SpawnMonster_static();
            yield return new WaitForSeconds(3f);
            caller.MarkAsLastNodeCall(this.attackNode);
            this.attackNode.ExecuteNode(caller);
        }
        yield return 0;
    }

}

