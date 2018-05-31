using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager {

    private static Damager m_current = null;
    public static Damager Current{
        get{
            if(m_current == null){
                return new Damager(DamageCause.Other);
            }
            return m_current;
        }
        set{
            m_current = value;
        }
    }

    [System.Flags]
    public enum DamageCause{ 
        Player = 1 << 0, 
        Enemy = Player << 1, 
        Environment = Enemy << 1, 
        Explosive = Environment << 1, 
        Melee = Explosive << 1,
        Ranged = Melee << 1,
        Other = Ranged << 1
    }


    private Dictionary<HealthBasedDamagable_healthContainer, float> storedDamageDone = new Dictionary<HealthBasedDamagable_healthContainer, float>();

    private DamageCause cause = DamageCause.Other;
    private float damage = 0;
    public float Damage{ 
        get{ 
            return this.damage; 
        } 
        set{
            this.damage = value;
        }
    }

    public bool CausedByPlayer{ get{ return (this.cause & DamageCause.Player) == DamageCause.Player; } }
    public bool CausedByEnemy{ get{ return (this.cause & DamageCause.Enemy) == DamageCause.Enemy; } }

    public DamageCause GetCause(){
        return this.cause;
    }


    public Damager(DamageCause cause){
        this.cause = cause;
    }

    public Damager(DamageCause cause, float damage) {
        this.cause = cause;
        this.damage = damage;
    }


    /// <summary>
    /// Stores what amount of damage this hit should have done to the target. If the target is hit multiple times, the highest vlue will be inflicted.
    /// </summary>
    /// <param name="healthContainer"></param>
    /// <param name="dmg"></param>
    public void StoreDamageDone(HealthBasedDamagable_healthContainer healthContainer, float dmg){
        if(!this.storedDamageDone.ContainsKey(healthContainer)){
            this.storedDamageDone.Add(healthContainer, dmg);
        }
        else{
            this.storedDamageDone[healthContainer] = Mathf.Max(this.storedDamageDone[healthContainer], dmg);
        }
    }


    /// <summary>
    /// Inflicts the stored damage and clears that list.
    /// </summary>
    public void InflictStoredDamage(){
        foreach(var pair in this.storedDamageDone){
            pair.Key.TakeDamage(pair.Value);
        }
        this.storedDamageDone.Clear();
    }

    /// <summary>
    /// Clears all stored damage without doing anything with it.
    /// </summary>
    public void ClearStoredDamage(){
        this.storedDamageDone.Clear();
    }

}
