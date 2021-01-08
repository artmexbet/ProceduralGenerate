using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAdder : MonoBehaviour
{
    public int eatBonus = 15;
    public void AddEat(Person person)
    {
        person.AddEat(eatBonus);
    }
}
