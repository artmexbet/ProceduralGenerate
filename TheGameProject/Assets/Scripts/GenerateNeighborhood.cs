﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNeighborhood : MonoBehaviour
{
    public int[,] matrix =
    {
        //0  1  2  3  4  5  6  7  8  9  10 11 12
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, //0 пустота
        {1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1}, //1 шаман
        {1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 1}, //2 лес
        {1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1}, //3 поляна
        {1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1}, //4 ритуальный
        {1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1}, //5 кухня
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, //6 парк
        {1, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1}, //7 арена
        {1, 1, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 1}, //8 ресторан
        {1, 1, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1}, //9 бедные
        {1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1}, //10 средние
        {1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1}, //11 богатые
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}  //12 дороги
    };
}
