using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxString : MonoBehaviour {

    [SerializeField] private Transform  point0 = null,
                                        point1 = null;
    public Vector3 Point0{ get{ return point0.position; } }
    public Vector3 Point1{ get{ return point1.position; } }
          
    
    #if UNITY_EDITOR
    public void DrawGizmos(){
        if(Point0 != null && Point1 != null){ 
            Color colorWas = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Point0, Point1);
            Gizmos.color = colorWas;
        }
    }
    #endif


}
