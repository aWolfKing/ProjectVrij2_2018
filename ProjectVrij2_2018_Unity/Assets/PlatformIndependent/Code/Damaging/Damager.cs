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

    private DamageCause cause = DamageCause.Other;
    private float damage = 0;
    public float Damage{ get{ return damage; } }

    public bool CausedByPlayer{ get{ return (cause & DamageCause.Player) == DamageCause.Player; } }
    public bool CausedByEnemy{ get{ return (cause & DamageCause.Enemy) == DamageCause.Enemy; } }

    public DamageCause GetCause(){
        return cause;
    }


    public Damager(DamageCause cause){
        this.cause = cause;
    }

    public Damager(DamageCause cause, float damage) {
        this.cause = cause;
        this.damage = damage;
    }

}
