using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UI : MonoBehaviour
{
    public int seed;
    private bool _played = false;
    private GameManager _gm;

    private void Awake()
    {
        seed =  Random.Range(1000, 9999);
        _gm = GameObject.Find("Generator").GetComponent<GameManager>();
    }

    public void Generate()
    {
        _gm.Generate(seed);
    }

    public void Play(GameObject eventObject)
    {
        if (_played)
        {
            eventObject.GetComponentInChildren<Text>().text = "Play";
            print("Stopped");
        }
        else
        {
            eventObject.GetComponentInChildren<Text>().text = "Stop";
            print("Played");
            
        }
        _played = !_played;
    }
}
