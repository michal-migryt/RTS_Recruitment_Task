using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float cameraMovementSpeed;
    [SerializeField] private float rotationSpeed;
    [Header("Transform position range")]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax; 
    [SerializeField] private float zMin; 
    [SerializeField] private float zMax;
    private InputActions inputActions;
    
    private void Awake() {
        _camera = Camera.main;
    }
    private void Start() {
        inputActions = GameController.instance._InputActions;
    }
    void Update()
    {
        MoveCamera();
        RotateCamera();
    }

    // potentially speed up movement when key pressed for longer
    public void MoveCamera()
    {
        Vector2 movement = inputActions.Player.CameraMovement.ReadValue<Vector2>();
        // essential usage of AngleAxis to make sure that movement is applied correctly horizontally and vertically when camera is rotated
        Vector3 movementVector = Quaternion.AngleAxis(_camera.transform.eulerAngles.y, Vector3.up) * new Vector3(movement.x, 0, movement.y);
        _camera.transform.position = ClampedPositionVector(movementVector);
        
    }
    // potentially speed up rotation when key pressed for longer
    public void RotateCamera()
    {
        float rotation = inputActions.Player.CameraRotation.ReadValue<float>() * rotationSpeed * Time.deltaTime;
        _camera.transform.Rotate(0, rotation ,0, Space.World);
    }
    // clamp camera position between min and max values
    private Vector3 ClampedPositionVector(Vector3 movement)
    {
        Vector3 tempPos = _camera.transform.position;
        tempPos += movement*Time.deltaTime*cameraMovementSpeed;
        tempPos = new Vector3(Mathf.Clamp(tempPos.x, xMin, xMax), tempPos.y, Mathf.Clamp(tempPos.z, zMin, zMax));
        return tempPos;
    }
}
