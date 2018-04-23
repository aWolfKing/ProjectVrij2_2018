using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SphereHitbox {

    public Transform transform = null;
    public Vector3 localPosition = Vector3.zero;
    public float radius = 1f;

    public Vector3 WorldPosition{
        get{
            if(transform == null){ return localPosition; }
            return transform.TransformPoint(localPosition);
        }
    }

    public bool Overlaps(SphereHitbox hitbox){
        return Vector3.Distance(this.transform.TransformPoint(this.localPosition), hitbox.transform.TransformPoint(hitbox.localPosition)) <= this.radius + hitbox.radius;
    }
	
}
