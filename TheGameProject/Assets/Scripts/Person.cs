using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum PersonType
{
    Rich, 
    Middle, 
    Poor
};


public class Person : MonoBehaviour
{
    [Header("Характеристики")]
    public int health = 100;
    public int hunger = 100;
    public int happy = 100;

    [Header("Возможность отнимать здоровье")]
    public bool canHealthDestroy = true;
    public bool canHungerDestroy = true;
    public bool canHappyDestroy = true;

    public PersonType personType = PersonType.Rich;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (canHealthDestroy) StartCoroutine(HealthDestroyer());
        if (canHungerDestroy) StartCoroutine(HungerDestroyer());
        if (canHappyDestroy) StartCoroutine(HappyDestroyer());
        if (health < 30)
        {
            
        }
    }

    IEnumerator HealthDestroyer()
    {
        canHealthDestroy = false;
        health--;
        yield return new WaitForSeconds(30);
        canHealthDestroy = true;
    }

    IEnumerator HungerDestroyer()
    {
        canHungerDestroy = false;
        hunger--;
        yield return new WaitForSeconds(35);
        canHungerDestroy = true;
    }

    IEnumerator HappyDestroyer()
    {
        canHappyDestroy = false;
        happy--;
        yield return new WaitForSeconds(40);
        canHappyDestroy = true;
    }
}
