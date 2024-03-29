﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHP : MonoBehaviour
{
    public Volume ppVolume;
    public Vignette vignette;
    public static float HP = 1;
    public float vignetteIntensity = 0;
    public int quantityOfZombies = 0;
    private bool inContactWithZombie = false;
    private bool loosingLife = false;
    private bool canRegen = false;
    void Start()
    {
        ppVolume.profile.TryGet<Vignette>(out vignette);
        HP = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (inContactWithZombie && !loosingLife)
            StartCoroutine(LoseLife());

        if(vignetteIntensity < 0 && canRegen)
        {
            StartCoroutine("RegenLife");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Zombie otherZ = other.GetComponent<Zombie>();
            if (otherZ != null)
            {
                if(!otherZ.isDead)
                {
                    bool canCount = !otherZ.isAttackingPlayerHp;
                    if (canCount && otherZ.haveSeenPlayer)
                    {
                        otherZ.isAttackingPlayerHp = true;
                        inContactWithZombie = true;
                        quantityOfZombies++;
                    }
                }
            }
        }
    }

    private IEnumerator LoseLife()
    {
        loosingLife = true;
        while (inContactWithZombie && GetComponent<Player>().playerIsAlive)
        {
            HP = HP - 0.03f;
            vignetteIntensity = HP - 1;
            if(vignetteIntensity < -1)
            {
                vignetteIntensity = -1;
            }
            vignette.intensity.value = vignetteIntensity * -1;
            yield return new WaitForSeconds(0.2f);

            if(HP < 0)
            {
                HP = 0;
                CallGameOver();
            }
        }
        loosingLife = false;
    }

    private IEnumerator RegenLife()
    {
        canRegen = false;
        while(!inContactWithZombie && vignetteIntensity < 0 && GetComponent<Player>().playerIsAlive)
        {
            vignetteIntensity += 0.02f;
            HP = HP + 0.04f;
            if (vignetteIntensity > 0)
                vignetteIntensity = 0;
            if (HP > 1)
                HP = 1;
            vignette.intensity.value = vignetteIntensity * -1;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Zombie otherZ = other.GetComponent<Zombie>();
            if (otherZ != null)
            {
                if (!otherZ.isDead)
                {
                    if (other.GetComponent<NavMeshAgent>().remainingDistance > 2)
                    {
                        otherZ.isAttackingPlayerHp = false;
                        ZombieKilled();
                    }
                }
                
            }
        }
    }

    public void ZombieKilled()
    {
        quantityOfZombies--;
        if (quantityOfZombies < 1)
        {
            quantityOfZombies = 0;
            inContactWithZombie = false;
            canRegen = true;
        }
    }

    private void CallGameOver()
    {
        GetComponent<Ragdoll>().Die();
        GetComponent<Player>().Die();
    }
}
