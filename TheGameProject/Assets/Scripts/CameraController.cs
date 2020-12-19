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
    public Vector2 rightLimit;
    public Vector2 leftLimit;

    private Vector3 _mousePos;
    private float _myAngle = 0;
    public float sensitivity = 1;
    
    void Awake()
    {
        _cameraTransformation = GetComponentInChildren<Transform>();
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
        if (position.x < rightLimit.x)
            position = new Vector3(rightLimit.x, position.y, position.z);
        else if (position.x > rightLimit.y) 
            position = new Vector3(rightLimit.y, position.y, position.z);
        if (position.y < upperLimit.x)
            position = new Vector3(position.x, upperLimit.x, position.z);
        else if (position.y > upperLimit.y)
            position = new Vector3(position.x, upperLimit.y, position.z);
        if  (position.z < leftLimit.x)
            position = new Vector3(position.x, position.y, leftLimit.x);
        else if (position.z > leftLimit.y)
            position = new Vector3(position.x, position.y, leftLimit.y);
        
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
