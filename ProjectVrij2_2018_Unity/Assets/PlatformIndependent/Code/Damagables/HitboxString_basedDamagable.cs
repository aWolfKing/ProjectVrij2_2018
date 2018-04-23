using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitboxString_basedDamagable : MonoBehaviour {


    #region static

    private static List<HitboxString_basedDamagable> allHitboxString_basedDamagable = new List<HitboxString_basedDamagable>();

    public static void SendHit(Collider hitbox){
        SendHit(hitbox, null);
    }
    public static void SendHit(Collider hitbox, List<HitboxString_basedDamagable> ignoreList) {
        foreach(var hitboxString_basedDamagable in allHitboxString_basedDamagable){
            if(ignoreList == null || !ignoreList.Contains(hitboxString_basedDamagable)){
                foreach(var hitboxFunctionPair in hitboxString_basedDamagable.hitBoxes){
                    if(hitboxFunctionPair.hitbox.DoesCollide(hitbox)){
                        HitInfo.damagedDamagable = hitboxString_basedDamagable;
                        HitInfo.hitHitboxFunctionPair = hitboxFunctionPair;
                        hitboxFunctionPair.OnHit.Invoke();
                    }
                }
            }
        }
        HitInfo.damagedDamagable = null;
        HitInfo.hitHitboxFunctionPair = null;
    }

    #endregion



    //[SerializeField] private UnityEvent defaultOnHit = new UnityEvent();

    [System.Serializable]
    public class HitboxFunctionPair{
        public HitboxStringCollection hitbox = null;
        public UnityEvent OnHit = new UnityEvent();
    }

    [SerializeField] private List<HitboxFunctionPair> hitBoxes = new List<HitboxFunctionPair>();


    protected virtual void OnEnable() {
        if(!allHitboxString_basedDamagable.Contains(this)){
            allHitboxString_basedDamagable.Add(this);
        }
    }

    protected virtual void OnDisable() {
        if(allHitboxString_basedDamagable.Contains(this)){
            allHitboxString_basedDamagable.Remove(this);
        }
    }

}
