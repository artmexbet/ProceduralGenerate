using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Test : MonoBehaviour
{
    private Random _rand;

    [SerializeField] private int seed;
    void Start()
    {
        _rand = new Random(seed);
        print(_rand.Next(100));
    }
    
    void Update()
    {
        
    }
}
