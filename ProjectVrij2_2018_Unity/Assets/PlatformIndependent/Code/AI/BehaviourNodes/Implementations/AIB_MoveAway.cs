using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIB_MoveAway : MonobehaviourAIBehaviourNode {

    //[SerializeField] private Transform runFrom = null;
    [SerializeField] private MonobehaviourAIBehaviourNode attackNode = null;
    [SerializeField] private MonobehaviourAIBehaviourNode shriekNode = null;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    [SerializeField] private float  moveAwayDist = 6f,
                                    moveAwayPosRadius = 2f;


    public override IEnumerator ExecuteNode(MonobehaviourAIBehaviourGraph caller) {

        if(CheckDistanceToPlayer() >= 5f){
            yield return this.shriekNode.ExecuteNode(caller);
        }
        else{
            bool canMoveAway = false;
            /*
            NavMeshHit moveAway;            
            float startAngle = UnityEngine.Random.Range(0f, 360f);
            for(int i=0; i<36; i++){
                Vector3 positionToCheck = this.navMeshAgent.transform.position + Quaternion.Euler(Vector3.up * (i * 10 + startAngle)) * Vector3.forward * this.moveAwayDist;
                if(NavMesh.SamplePosition(positionToCheck, out moveAway, this.moveAwayPosRadius, 1 << NavMesh.GetAreaFromName("Walkable"))){
                    canMoveAway = true;
                    this.navMeshAgent.SetDestination(moveAway.position);
                    yield return new WaitForSeconds(0.5f);
                    break;
                }
            }
            */

            Vector3 positionToRunTo = (this.navMeshAgent.transform.position - /*this.runFrom.position*/ PlayerControl.CharacterPosition).normalized * (this.moveAwayDist + this.moveAwayPosRadius);
            NavMeshHit navMeshHit;
            if(NavMesh.SamplePosition(positionToRunTo, out navMeshHit, this.moveAwayPosRadius, 1 << NavMesh.GetAreaFromName("Walkable"))){
                canMoveAway = true;
                this.navMeshAgent.SetDestination(navMeshHit.position);
                yield return new WaitForSeconds(0.5f);
            }

            if(!canMoveAway){
                yield return this.shriekNode.ExecuteNode(caller);
            }

            /*
            if(!canMoveAway){
                MonoBehaviour.print("Attack till death");
                caller.MarkAsLastNodeCall(this.attackNode);
                yield return this.attackNode.ExecuteNode(caller);
            }
            */
        }

        //caller.MarkAsLastNodeCall(this);
        yield return null;
    }



    private float CheckDistanceToPlayer(){
        return Vector3.Distance(/*this.runFrom.position*/ PlayerControl.CharacterPosition, this.navMeshAgent.transform.position)
        ;
    }

    private bool CanMoveAway(){
        return false;
    }

}

