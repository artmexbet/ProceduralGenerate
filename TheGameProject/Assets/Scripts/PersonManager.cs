using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PersonManager : MonoBehaviour
{
    public static List<Person> PoorPersons = new List<Person>();
    public static List<Person> MidPersons = new List<Person>();
    public static List<Person> RichPersons = new List<Person>();
    [Header("Префабы людей")] public List<Person> persons;

    private bool _spawned = false;

    private void Update()
    {
        if (!_spawned)
        {
            StartCoroutine(Spawn());
        }
    }

    public void StartSpawn()
    {
        for (int i = 0; i < Random.Range(Mathf.RoundToInt(GameManager.PoorHouse.Count * 0.3f),
            GameManager.PoorHouse.Count); i++)
        {
            var tempPerson = Instantiate(persons[0]);
            // var position = GameManager.PoorHouse[Random.Range(0, GameManager.PoorHouse.Count)].gameObject.transform.position;
            tempPerson.transform.position = new Vector3(Random.Range(1, 50), 0.15f, Random.Range(1, 50));
            tempPerson.personType = PersonType.Poor;
            tempPerson.socialStatus = SocialStatus.WorkingPerson;
            tempPerson.SetHome();
            PoorPersons.Add(tempPerson);
        }
        for (int i = 0; i < Random.Range(Mathf.RoundToInt(GameManager.MidHouse.Count * 0.3f),
            GameManager.MidHouse.Count); i++)
        {
            var tempPerson = Instantiate(persons[1]);
            var position = GameManager.MidHouse[Random.Range(0, GameManager.MidHouse.Count)].gameObject.transform.position;
            tempPerson.transform.position = new Vector3(Random.Range(1, 50), 0.15f, Random.Range(1, 50));
            tempPerson.personType = PersonType.Middle;
            tempPerson.socialStatus = SocialStatus.WorkingPerson;
            tempPerson.SetHome();
            MidPersons.Add(tempPerson);
        }
        for (int i = 0; i < Random.Range(Mathf.RoundToInt(GameManager.RichHouse.Count * 0.3f),
            GameManager.RichHouse.Count); i++)
        {
            var tempPerson = Instantiate(persons[2]);
            var position = GameManager.RichHouse[Random.Range(0, GameManager.RichHouse.Count)].gameObject.transform.position;
            tempPerson.transform.position = new Vector3(Random.Range(1, 50), 0.15f, Random.Range(1, 50));
            tempPerson.personType = PersonType.Rich;
            tempPerson.socialStatus = SocialStatus.WorkingPerson;
            tempPerson.SetHome();
            RichPersons.Add(tempPerson);
        }
    }

    IEnumerator Spawn()
    {
        _spawned = false;
        yield return new WaitForSeconds(600);
        for (int i = 0; i < PoorPersons.Count * 0.1f; i++)
        {
            var tempPerson = Instantiate(persons[0]);
            var position = GameManager.PoorHouse[Random.Range(0, GameManager.PoorHouse.Count)].gameObject.transform.position;
            tempPerson.transform.position = new Vector3(Random.Range(1, 50), 0.15f, Random.Range(1, 50));
            tempPerson.SetSocialStatus();
            tempPerson.SetHome();
            tempPerson.personType = PersonType.Poor;
            PoorPersons.Add(tempPerson);
        }
        for (int i = 0; i < MidPersons.Count * 0.1f; i++)
        {
            var tempPerson = Instantiate(persons[1]);
            var position = GameManager.MidHouse[Random.Range(0, GameManager.MidHouse.Count)].gameObject.transform.position;
            tempPerson.transform.position =new Vector3(Random.Range(1, 50), 0.15f, Random.Range(1, 50));
            tempPerson.SetSocialStatus();
            tempPerson.personType = PersonType.Middle;
            MidPersons.Add(tempPerson);
        }
        for (int i = 0; i < RichPersons.Count * 0.1f; i++)
        {
            var tempPerson = Instantiate(persons[2]);
            var position = GameManager.RichHouse[Random.Range(0, GameManager.RichHouse.Count)].gameObject.transform.position;
            tempPerson.transform.position = new Vector3(Random.Range(1, 50), 0.15f, Random.Range(1, 50));
            tempPerson.SetSocialStatus();
            tempPerson.personType = PersonType.Rich;
            RichPersons.Add(tempPerson);
        }
        _spawned = true;
    }
}
