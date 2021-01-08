using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum PersonType
{
    Rich, 
    Middle, 
    Poor
}

public enum PersonState
{
    Ok,
    Wounded
}

public enum SocialStatus
{
    Child,
    WorkingPerson,
    UnemployedPerson,
    Retire
}

public enum Preferences
{
    SafeHunting,
    UnsafeHunting
}

public class Person : MonoBehaviour
{
    [Header("Характеристики")] 
    [Range(0, 100)] public int health = 100;
    [Range(0, 100)] public int hunger = 100;
    [Range(0, 100)] public int happy = 100;

    [Header("Возможность отнимать здоровье")]
    public bool canHealthDestroy = true;

    public bool canHungerDestroy = true;
    public bool canHappyDestroy = true;

    [Header("Состояние человека")] public PersonType personType = PersonType.Rich;
    public PersonState personState = PersonState.Ok;
    public SocialStatus socialStatus = SocialStatus.Child;
    public Preferences preferences;

    private NavMeshAgent _navMeshAgent;

    public bool _busy = false;

    [Range(0, 99)] public int old;

    public Building home;

    public Building target = null;
    private Animator _animator;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        
        switch (Random.Range(0, 2))
        {
            case 0:
                preferences = Preferences.SafeHunting;
                break;
            case 1:
                preferences = Preferences.UnsafeHunting;
                break;
        }

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canHealthDestroy) StartCoroutine(HealthDestroyer());
        if (canHungerDestroy) StartCoroutine(HungerDestroyer());
        if (canHappyDestroy) StartCoroutine(HappyDestroyer());

        if (health < 30 && !_busy)
        {
            MoveToHeal();
        }
        if (hunger < 30 && !_busy)
        {
            personState = PersonState.Wounded;
            MoveToEat();
        }
        else personState = PersonState.Ok;
        if (happy < 30 && Vector3.Distance(transform.position,
            Distance("happy").transform.position) >= 1 && !_busy) MoveToHappy();
        if (health >= 30 && hunger >= 30 && happy >= 30 && !_busy && 
            Vector3.Distance(transform.position, home.transform.position) >= 1) MoveToHome();
    }

    IEnumerator HealthDestroyer()
    {
        int damage = 0;
        if (personState == PersonState.Wounded) damage += 1;
        if (personType == PersonType.Poor) damage += 1;
        if (happy > 30) damage -= 1;
        if (hunger >= 30) damage -= 1;
        canHealthDestroy = false;
        health -= damage;
        yield return new WaitForSeconds(10);
        canHealthDestroy = true;
    }

    IEnumerator HungerDestroyer()
    {
        canHungerDestroy = false;
        hunger--;
        yield return new WaitForSeconds(15);
        canHungerDestroy = true;
    }

    IEnumerator HappyDestroyer()
    {
        canHappyDestroy = false;
        happy--;
        yield return new WaitForSeconds(10);
        canHappyDestroy = true;
    }

    void MoveToEat()
    {
        _busy = true;
        Building building = Distance("eat");
        target = building;
        _navMeshAgent.SetDestination(building.transform.position);
        _animator.SetTrigger("Walk");
    }
    void MoveToHeal()
    {
        _busy = true;
        Building building = Distance("heal");
        target = building;
        _navMeshAgent.SetDestination(building.transform.position);
        _animator.SetTrigger("Walk");
    }
    void MoveToHappy()
    {
        _busy = true;
        Building building = Distance("happy");
        target = building;
        _navMeshAgent.SetDestination(building.transform.position);
        _animator.SetTrigger("Walk");
    }

    void MoveToHome()
    {
        target = home;
        _navMeshAgent.SetDestination(home.transform.position);
        _animator.SetTrigger("Walk");
    }

    Building Distance(string category)
    {
        List<Building> temp = GameManager.PoorHappy;
        if (category == "eat")
        {
            if (personType == PersonType.Poor) temp = GameManager.PoorFood;
            else if (personType == PersonType.Middle) temp = GameManager.MidFood;
            else if (personType == PersonType.Rich) temp = GameManager.RichFood;
        }
        else if (category == "heal")
        {
            temp = GameManager.Heal;
        }
        else if (category == "happy")
        {
            if (personType == PersonType.Poor) temp = GameManager.PoorHappy;
            else if (personType == PersonType.Middle) temp = GameManager.MidHappy;
            else if (personType == PersonType.Rich) temp = GameManager.RichHappy;
        }
        else
        {
            print("Неправильная категория!");
            return null;
        }

        float minDistance = 1000000f;
        Building nearestBuilding = new Building();
        foreach (var building in temp)
        {
            float distance = Vector3.Distance(transform.position, building.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestBuilding = building;
            }
        }
        return nearestBuilding;
    }

    public void SetSocialStatus()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                socialStatus = SocialStatus.Child;
                break;
            case 1:
                socialStatus = SocialStatus.WorkingPerson;
                break;
            case 2:
                socialStatus = SocialStatus.UnemployedPerson;
                break;
            case 3:
                socialStatus = SocialStatus.Retire;
                break;
        }
    }

    public void SetHome()
    {
        switch (personType)
        {
            case PersonType.Poor:
                home = GameManager.PoorHouse[Random.Range(0, GameManager.PoorHouse.Count)];
                break;
            case PersonType.Middle:
                home = GameManager.MidHouse[Random.Range(0, GameManager.MidHouse.Count)];
                break;
            case PersonType.Rich:
                home = GameManager.RichHouse[Random.Range(0, GameManager.RichHouse.Count)];
                break;
        }
    }

    public void SetHome(Building house)
    {
        if (house.type == BuildingType.House && personType == house.personType)
        {
            home = house;
        }
        else
        {
            print("Отвали!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            if (other.GetComponent<Building>() == target)
            {
                target = null;
                _busy = false;
                other.GetComponent<Building>().AddBonus(this);
                print("Рвроылвлц");
            }
        }
    }

    public void AddEat(int bonus)
    {
        hunger += bonus;
    }

    public void AddHealth(int bonus)
    {
        health += bonus;
    }

    public void AddHappy(int bonus)
    {
        happy += bonus;
    }
}
