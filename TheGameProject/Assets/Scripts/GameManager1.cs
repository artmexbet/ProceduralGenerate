using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager1 : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(15, 15);

    private Building[,] _grid;
    public int[,] matrix;
    
    private Building _flyingBuilding;
    private Camera _mainCamera;
    
    private int[] _count;
    private int n;
    private GenerateNeighborhood _neighborhood;

    [Header("Префабы")] public List<Building> prefabs;

    private void Awake()
    {
        _grid = new Building[gridSize.x, gridSize.y];
        _mainCamera = Camera.main;
        
        matrix = new int[gridSize.x, gridSize.y];
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                matrix[x, y] = -1;
            }
        }
        
        n = gridSize.x;
        int newN = n * n;
        _count = new[]
        {
            Mathf.RoundToInt(0.3f * newN), 1, Mathf.RoundToInt(0.2f * newN),
            3, 2, 3, Mathf.RoundToInt(0.05f * newN), 2, 2, Mathf.RoundToInt(0.3f * newN),
            Mathf.RoundToInt(0.2f * newN), Mathf.RoundToInt(0.1f * newN), Mathf.RoundToInt(0.3f * newN),
        };

        _neighborhood = GetComponent<GenerateNeighborhood>();
    }
    public void SpawnTile(Building prefab)
    {
        if (_flyingBuilding != null) Destroy(_flyingBuilding.gameObject);
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

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = !(x < 0 || x > gridSize.x - _flyingBuilding.size.x) && 
                                 !(y < 0 || y > gridSize.y - _flyingBuilding.size.y) && 
                                 !IsPlaceTaken(x, y);

                var transform1 = _flyingBuilding.transform;
                transform1.position = new Vector3(x, transform1.position.y, y);
                _flyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceBuilding(x, y);
                }
            }
        }
    }

    private void PlaceBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding.size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.size.y; y++)
            {
                _grid[placeX + x, placeY + y] = _flyingBuilding;
            }
        }
        
        _flyingBuilding.SetNormal();
        _flyingBuilding = null;
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding.size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.size.y; y++)
            {
                if (_grid[placeX + x, placeY + y] != null) return true;
            }
        }
        return false;
    }
    

    List<int> Neighbours(int x, int y)
    {
        List<int> tempMatrix = new List<int>();
        if (x - 1 >= 0 && y - 1 >= 0 && matrix[x - 1, y - 1] != -1) tempMatrix.Add(matrix[x - 1, y - 1]);
        if (x - 1 >= 0 && matrix[x - 1, y] != -1) tempMatrix.Add(matrix[x - 1, y]);
        if (x - 1 >= 0 && y + 1 < n && matrix[x - 1, y + 1] != -1) tempMatrix.Add(matrix[x - 1, y + 1]);
        if (y - 1 >= 0 && matrix[x, y - 1] != -1) tempMatrix.Add(matrix[x, y - 1]);
        if (y + 1 < n && matrix[x, y + 1] != -1) tempMatrix.Add(matrix[x, y + 1]);
        if (x + 1 < n && y - 1 >= 0 && matrix[x + 1, y - 1] != -1) tempMatrix.Add(matrix[x + 1, y - 1]);
        if (x + 1 < n && matrix[x + 1, y] != -1) tempMatrix.Add(matrix[x + 1, y]);
        if (x + 1 < n && y + 1 < n && matrix[x + 1, y + 1] != -1) tempMatrix.Add(matrix[x + 1, y + 1]);
        return tempMatrix;
    }

    int RoadNeighbours(int x, int y, int x0, int y0)
    {
        n = 0;
        if (x - 1 >= 0 && (matrix[x - 1, y] == 12 || matrix[x - 1, y] == -1) && x-1!=x0)
            n++;
        if (x + 1 < n && (matrix[x + 1, y] == 12 || matrix[x + 1, y] == -1) && x + 1 != x0)
            n++;
        if (y - 1 >= 0 && (matrix[x, y - 1] == 12 || matrix[x, y - 1] == -1) && y - 1 != y0)
            n++;
        if (y + 1 < n && (matrix[x, y + 1] == 12 || matrix[x, y + 1] == -1) && y + 1 != y0)
            n++;
        return n;
    }

    public void GenerateMatrix(int seed)
    {
        Random random = new Random(seed);
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (x - 1 >= 0 && matrix[x - 1, y] == 12 && RoadNeighbours(x - 1, y, x, y) < 2)
                    matrix[x, y] = 12;
                else if (x + 1 < n && matrix[x + 1, y] == 12 && RoadNeighbours(x + 1, y, x, y) < 2)
                    matrix[x, y] = 12;
                else if (y - 1 >= 0 && matrix[x, y - 1] == 12 && RoadNeighbours(x, y - 1, x, y) < 2)
                    matrix[x, y] = 12;
                else if (y + 1 < n && matrix[x, y + 1] == 12 && RoadNeighbours(x, y + 1, x, y) < 2)
                    matrix[x, y] = 12;
                else
                {
                    int[] temp = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
                    for (int j = 0; j < 13; j++) if (_count[j] <= 0) temp[j] = 0;
                    var vasdasd = Neighbours(x, y);
                    foreach (int i in vasdasd)
                    {
                        for (int j = 0; j < 13; j++)
                        {
                            if (_neighborhood.matrix[i, j] == 0) temp[j] = 0;
                        }
                    }
                    List<int> choice = new List<int>();
                    for (int i = 0; i < 13; i++)
                    {
                        if (temp[i] != 0) choice.Add(i);
                    }

                    if (choice.Count != 0)
                    {
                        int _temp = random.Next(choice.Count);
                        matrix[x, y] = choice[_temp];
                    }
                    else matrix[x, y] = 0;
                }
                _count[matrix[x, y]]--;
                string tempString = "";
                foreach (var elem in _count)
                {
                    tempString += " " + elem;
                }
                print(tempString);
            }
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (matrix[x, y] != 0)
                {
                    var temp = Instantiate(prefabs[matrix[x, y]]);
                    temp.transform.position = new Vector3(x, 0, y);
                    _grid[x, y] = temp;
                }
            }
        }
    }
}
