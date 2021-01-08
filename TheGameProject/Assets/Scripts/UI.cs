using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public bool played = false;

    public void Play(GameObject eventObject)
    {
        if (played)
        {
            eventObject.GetComponentInChildren<Text>().text = "Play";
            print("Stopped");
            Time.timeScale = 0;
        }
        else
        {
            eventObject.GetComponentInChildren<Text>().text = "Stop";
            print("Played");
            Time.timeScale = 1;
        }
        played = !played;
    }
}
