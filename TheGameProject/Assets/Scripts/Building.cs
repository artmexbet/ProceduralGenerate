using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer mainRenderer;
    public Vector2Int size = Vector2Int.one;
    private Color _baseColor;

    public void Awake()
    {
        _baseColor = mainRenderer.material.color;
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
}
