using UnityEngine;
using UnityEngine.InputSystem;

public class Movements : MonoBehaviour
{

    private Rigidbody _rb;
    private PlayerInput _playerInput;

    private Vector3 inputVector;
    private Vector3 accelerationVector;

    public float movementSpeed = 5f;
    public float turnSpeed = 100f;
    private float turnAmount = 0f;
    private Quaternion turnOffset;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        InputSystem.actions.Disable();
        _playerInput.currentActionMap?.Enable();

        _rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();

        // correction to 3D
        accelerationVector = transform.forward * inputVector.y;

        turnAmount = inputVector.x * turnSpeed * Time.fixedDeltaTime;
        turnOffset = Quaternion.Euler( 0, turnAmount, 0 );
    }

    void Update()
    {
        _rb.AddForce(accelerationVector * movementSpeed, ForceMode.Acceleration);
        _rb.MoveRotation(Quaternion.Normalize(_rb.rotation * turnOffset));
    }
}
