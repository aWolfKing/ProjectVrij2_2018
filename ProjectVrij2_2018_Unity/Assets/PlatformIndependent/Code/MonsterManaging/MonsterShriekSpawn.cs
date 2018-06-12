using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterShriekSpawn : MonoBehaviour{

    [SerializeField] private Transform spawnCenter = null;
    [SerializeField] private float range = 15f;
    [SerializeField] private GameObject monsterPrefab = null;
    [SerializeField] private Transform shriekPosition = null;
    private static MonsterShriekSpawn instance = null;
    public static Transform ShriekTransform{
        get{
            return instance.shriekPosition;
        }
    }
    public static Vector3 SpawnCenter{
        get{
            return instance.spawnCenter.position;
        }
    }


    private void Awake() {
        instance = this;
    }


    public static void SpawnMonster_static(){
        instance.SpawnMonster();
    }

    public void SpawnMonster(){

        NavMeshHit navMeshHit;
        if(NavMesh.SamplePosition(this.spawnCenter.position, out navMeshHit, this.range, 1 << NavMesh.GetAreaFromName("Walkable"))){
            GameObject monster = GameObject.Instantiate(this.monsterPrefab);
            monster.transform.position = navMeshHit.position;
        }

    }

    #if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(this.spawnCenter.position, this.range);
    }
    #endif

}

