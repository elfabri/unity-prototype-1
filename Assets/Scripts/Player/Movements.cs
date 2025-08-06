using UnityEngine;
using UnityEngine.InputSystem;

public class Movements : MonoBehaviour
{

    private Rigidbody _rb;
    public PlayerInput playerInput;
    private Vector3 rawInputMovement;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputMovement = value.Get<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        _rb.MovePosition(transform.position + rawInputMovement);
    }
}
