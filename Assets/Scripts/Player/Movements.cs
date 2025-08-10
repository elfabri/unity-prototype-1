using UnityEngine;
using UnityEngine.InputSystem;

public class Movements : MonoBehaviour
{

    private Rigidbody _rb;
    private PlayerInput _playerInput;

    private Vector3 inputVector;
    private float accelerationInput;
    private Vector3 accelerationVector = Vector3.zero;

    public float maxMovementSpeed = 25f;
    public Vector3 velocity = Vector3.zero;
    public float deccSpeed = -20f;
    public float turnSpeed = 50f;
    private float turnAmount = 0f;
    private Quaternion turnOffset;
    private InputAction accInputAction;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        InputSystem.actions.Disable();

        // activate default action map, set on editor
        _playerInput.currentActionMap?.Enable();
        accInputAction = _playerInput.actions.FindAction("Acceleration");

        _rb = GetComponent<Rigidbody>();
    }

    public void OnAcceleration(InputValue value)
    {
        accelerationInput = value.Get<float>();
    }

    public void OnTurn(InputValue v)
    {
        inputVector = v.Get<Vector2>();

        turnAmount = inputVector.x * turnSpeed * Time.fixedDeltaTime;
        turnOffset = Quaternion.Euler( 0, turnAmount, 0 );
    }

    void Update()
    {

        if (accInputAction.IsPressed())
        {
            accelerationVector = transform.forward * accelerationInput;
            if (accelerationInput > 0)
            {
                _rb.linearVelocity = Vector3.SmoothDamp(_rb.linearVelocity, accelerationVector * maxMovementSpeed, ref velocity, 0.5f);
            }
            else
            {
                _rb.linearVelocity = Vector3.SmoothDamp(_rb.linearVelocity, Vector3.zero, ref velocity, 0.8f);
            }
        }
        else
        {
            // deccelerating
            _rb.linearVelocity = Vector3.SmoothDamp(_rb.linearVelocity, Vector3.zero, ref velocity, 1.5f);
        }

        if (_rb.linearVelocity.magnitude > 0.1)
        {
            _rb.MoveRotation(Quaternion.Normalize(_rb.rotation * turnOffset));
        }
    }
}
