using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum BuildingType
{
    Eat,
    Heal,
    Happy,
    House,
    Other
}

public class Building : MonoBehaviour
{
    public Renderer mainRenderer;
    public Vector2Int size = Vector2Int.one;
    private Color _baseColor;

    public BuildingType type = BuildingType.Other;
    public PersonType personType = PersonType.Poor;

    public int bonus = 15;

    public void Awake()
    {
        _baseColor = mainRenderer.material.color;
        if (type == BuildingType.Heal) GameManager.Heal.Add(this);
        switch (personType)
        {
            case PersonType.Rich:
                if (type == BuildingType.Eat) GameManager.RichFood.Add(this);
                else if(type == BuildingType.Happy) GameManager.RichHappy.Add(this);
                else if(type == BuildingType.House) GameManager.RichHouse.Add(this);
                break;
            case PersonType.Middle:
                if (type == BuildingType.Eat) GameManager.MidFood.Add(this);
                else if(type == BuildingType.Happy) GameManager.MidHappy.Add(this);
                else if(type == BuildingType.House) GameManager.MidHouse.Add(this);
                break;
            case PersonType.Poor:
                if (type == BuildingType.Eat) GameManager.PoorFood.Add(this);
                else if(type == BuildingType.Happy) GameManager.PoorHappy.Add(this);
                else if(type == BuildingType.House) GameManager.PoorHouse.Add(this);
                break;
        }
    }

    public void SetTransparent(bool available)
    {
        if (available) mainRenderer.material.color = Color.green;
        else mainRenderer.material.color = Color.red;
    }

    public void SetNormal()
    {
        mainRenderer.material.color = _baseColor;
    }

    public void OnDrawGizmos()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(1f, 0f, 0.93f, 0.3f);
                else Gizmos.color = new Color(1f, 0.25f, 0f, 0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
            }
        }
    }

    public void AddBonus(Person person)
    {
        switch (type)
        {
            case BuildingType.Eat:
                AddEat(person);
                break;
            case BuildingType.Happy:
                AddHappy(person);
                break;
            case BuildingType.Heal:
                AddHealth(person);
                break;
        }
    }
    public void AddEat(Person person)
    {
        person.AddEat(bonus);
    }
    public void AddHappy(Person person)
    {
        person.AddHappy(bonus);
    }
    public void AddHealth(Person person)
    {
        person.AddHealth(bonus);
    }
}
