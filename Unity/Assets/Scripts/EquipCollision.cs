using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using System;
using System.Linq;

public class EquipCollision : MonoBehaviour
{   
    public long PlayerId;   
    ParticleSystem ps;   
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        InvokeRepeating("SetFalseCollision", 5.0f, 3.0f);
    }

    
    void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);        
        
        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(255, 0, 0, 255);
            enter[i] = p;
        }

        // iterate through the particles which exited the trigger and make them green
        //for (int i = 0; i < numExit; i++)
        //{
        //    ParticleSystem.Particle p = exit[i];
        //    p.startColor = new Color32(0, 255, 0, 255);
        //    exit[i] = p;
        //}

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, enter);
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, exit);
    }

    void OnParticleCollision(GameObject other)
    {      
        if (other.name == "FireTanK")
        {
            Debug.Log(other.name+ "---------OnParticleCollision-------");
            Client.Instance.DoSetifCollision(true, PlayerId);
        }
    }

    void SetFalseCollision()
    {
        Debug.Log("---------SetFalseCollision-------");
        Client.Instance.DoSetifCollision(false, PlayerId);
    }
 }
