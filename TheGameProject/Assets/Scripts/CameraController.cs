using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _cameraTransformation;
    public float movementSpeed = 1;

    [Header("Ограничения")] public Vector2 upperLimit = new Vector2(3, 10);
    public Vector2 rightLimit;
    public Vector2 leftLimit;
    
    private Vector3 _mousePos;
    private  Camera _goCamera;
    private float _myAngle = 0;
    
    void Awake()
    {
        _cameraTransformation = GetComponentInChildren<Transform>();
        _goCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Далее блок кода, отвечающий за движение камеры
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");
        var upMul = Input.GetAxis("Upper");
        
        // var position = transform.position;
        if (rightLimit.x <= transform.position.x + 2 * dx && transform.position.x + 2 * dx <= rightLimit.y)
            transform.Translate(new Vector3(movementSpeed * dx, 0, 0));
        if (upperLimit.x <= transform.position.y + 2 *upMul && transform.position.y + 2 * upMul <= upperLimit.y) 
            transform.Translate(new Vector3(0, movementSpeed * upMul, 0));
        if (leftLimit.x <= transform.position.z + 2 * dy && transform.position.z + 2 * dy <= leftLimit.y)
            transform.Translate(new Vector3(0, 0, movementSpeed * dy));
        // transform.Translate(new Vector3(movementSpeed * dx, movementSpeed * upMul, movementSpeed * dy));
        // position = new Vector3(Mathf.Clamp(position.x + movementSpeed * dx, rightLimit.x, rightLimit.y),
        //     Mathf.Clamp(position.y + movementSpeed * upMul, upperLimit.x, upperLimit.y),
        //     Mathf.Clamp(position.z + movementSpeed * dy, leftLimit.x, leftLimit.y));
        // transform.position = position;
        
        // Блок вращения камеры
        if (Input.GetMouseButton(1))
        {
            _mousePos = Input.mousePosition;
            _myAngle = ((_mousePos.x - (Screen.width / 2)) / Screen.width);
            transform.Rotate(transform.up, _myAngle);
        }
    }
}
