using System;
using System.Collections.Generic;
using UnityEngine;


public class EnemyLockon : MonoBehaviour{

    private static List<EnemyLockon> lockons = new List<EnemyLockon>();
    [SerializeField] private bool canLockon = true;
    [SerializeField] private float shared_lockonAngle = 40f;
    private static EnemyLockon currentLockon = null;
    public static EnemyLockon CurrentLockon{ 
        get{
            return currentLockon;
        }
    }


    private void OnEnable() {
        if(this.canLockon){
            if(!lockons.Contains(this)){
                lockons.Add(this);
            }
        }
    }

    private void OnDisable() {
        if(lockons.Contains(this)){
            lockons.Remove(this);
        }
    }



    public static void FindLockon(Vector3 pos, Vector3 dir){
        EnemyLockon closest = null;
        foreach(var lockon in lockons){
            float angle = Vector3.Angle(dir, (lockon.transform.position - pos).normalized);
            if(angle <= lockon.shared_lockonAngle){
                if(closest == null || (closest != null && angle < Vector3.Angle((closest.transform.position - pos).normalized, dir))){
                    closest = lockon;
                }
            }
        }
        currentLockon = closest;
    }

    public static void ResetLockon(){
        currentLockon = null;
    }

}

