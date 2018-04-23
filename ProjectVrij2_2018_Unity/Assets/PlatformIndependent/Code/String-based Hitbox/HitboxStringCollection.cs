using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxStringCollection : MonoBehaviour {

    [SerializeField] private List<HitboxString> hitboxStrings = new List<HitboxString>();

    public bool DoesCollide(Collider otherHitbox){
        RaycastHit hit;
        foreach(var hitboxString in hitboxStrings){
            Vector3 stringDirection = (hitboxString.Point1 - hitboxString.Point0);
            if(otherHitbox.Raycast(new Ray(hitboxString.Point0, stringDirection), out hit, stringDirection.magnitude)){
                return true;
            }
        }
        return false;
    }


    #if UNITY_EDITOR
    protected virtual void OnDrawGizmos() {
        foreach(var hitboxString in hitboxStrings){
            if(hitboxString != null){ 
                hitboxString.DrawGizmos();
            }
        }
    }
    #endif


}
