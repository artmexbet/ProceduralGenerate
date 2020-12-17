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
    private Building _flyingBuilding;
    private Camera _mainCamera;

    private void Awake()
    {
        seed =  Random.Range(1000, 9999);
        _gm = GameObject.Find("Generator").GetComponent<GameManager>();
        _mainCamera = Camera.main;
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

    public void SpawnTile(Building prefab)
    {
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding);
        }
        _flyingBuilding = Instantiate(prefab);
    }

    private void Update()
    {
        if (_flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                _flyingBuilding.transform.position = worldPosition;
            }
        }
    }
}
