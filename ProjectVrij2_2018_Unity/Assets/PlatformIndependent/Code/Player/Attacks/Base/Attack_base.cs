using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class Attack_base : MonoBehaviour {

    [System.Serializable]
    public class AttackFrame{
        public float time_from = 0f;
        public float time_till = 0f;
        public SphereHitbox[] hitboxes = new SphereHitbox[] { };
    }

    public List<AttackFrame> attackFrames = new List<AttackFrame>();
    public float attackDuration = 1f;


    protected virtual void Awake(){
        foreach (var frame in attackFrames) {
            foreach (var hitbox in frame.hitboxes) {
                if (hitbox.transform == null) { 
                    hitbox.transform = transform;
                }
            }
        }
    }


    protected virtual void Update(){
        #if UNITY_EDITOR
        foreach (var frame in attackFrames) {
            foreach (var hitbox in frame.hitboxes) {
                if (hitbox.transform == null) {
                    hitbox.transform = transform;
                }
            }
        }
        #endif
    }


    #if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected() {
        Color colorWas = Gizmos.color;

        int i = 1;
        foreach(var frame in attackFrames){

            Gizmos.color = Color.HSVToRGB(0.3f, 1-(1f / attackDuration * frame.time_from), 1f);

            foreach(var hitbox in frame.hitboxes){
                Gizmos.DrawWireSphere(hitbox.WorldPosition, hitbox.radius);
                UnityEditor.Handles.Label(hitbox.WorldPosition, frame.time_from + "s" + " - " + frame.time_till + "s");
            }

            i++;
        }

        Gizmos.color = colorWas;
    }
    #endif


}
