using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// movementSpeed - скорость передвижения камеры
    /// *Limit - ограничения камеры
    /// sensitivity - чувствительность вращения камеры
    /// </summary>
    
    
    private Transform _cameraTransformation;
    public float movementSpeed = 1;
    // private Rigidbody _rb;

    [Header("Ограничения")] public Vector2 upperLimit = new Vector2(3, 10);
    private Vector2 _rightLimit;
    private Vector2 _leftLimit;

    private Vector3 _mousePos;
    private float _myAngle = 0;
    public float sensitivity = 1;

    private GameManager _gm;
    
    void Awake()
    {
        _cameraTransformation = GetComponentInChildren<Transform>();
        _gm = GameObject.Find("Generator").GetComponent<GameManager>();
        GameObject.Find("Ground").transform.localScale = new Vector3(_gm.gridSize.x / 10, 1, _gm.gridSize.y / 10);
        _rightLimit = new Vector2(-2, _gm.gridSize.x + 2);
        _leftLimit = new Vector2(-2, _gm.gridSize.y + 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Далее блок кода, отвечающий за движение камеры
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");
        var upMul = Input.GetAxis("Upper");
        
        transform.Translate(new Vector3(movementSpeed * dx, movementSpeed * upMul, movementSpeed * dy));
        var position = transform.position;
        // Этот блок - ограничение позиции камеры
        if (position.x < _rightLimit.x)
            position = new Vector3(_rightLimit.x, position.y, position.z);
        else if (position.x > _rightLimit.y) 
            position = new Vector3(_rightLimit.y, position.y, position.z);
        if (position.y < upperLimit.x)
            position = new Vector3(position.x, upperLimit.x, position.z);
        else if (position.y > upperLimit.y)
            position = new Vector3(position.x, upperLimit.y, position.z);
        if  (position.z < _leftLimit.x)
            position = new Vector3(position.x, position.y, _leftLimit.x);
        else if (position.z > _leftLimit.y)
            position = new Vector3(position.x, position.y, _leftLimit.y);
        
        transform.position = position;
        
        // Блок вращения камеры
        if (Input.GetMouseButton(1))
        {
            _mousePos = Input.mousePosition;
            _myAngle = sensitivity * ((_mousePos.x - (Screen.width / 2)) / Screen.width);
            transform.Rotate(transform.up, _myAngle);
        }
    }
}
