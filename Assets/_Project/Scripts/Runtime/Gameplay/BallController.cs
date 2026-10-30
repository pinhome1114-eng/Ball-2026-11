using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            moveInput = Vector2.zero;
            return;
        }

        float horizontal = 0f;
        float vertical = 0f;

        if (keyboard.aKey.isPressed) horizontal -= 1f;
        if (keyboard.dKey.isPressed) horizontal += 1f;
        if (keyboard.sKey.isPressed) vertical -= 1f;
        if (keyboard.wKey.isPressed) vertical += 1f;

        moveInput = Vector2.ClampMagnitude(new Vector2(horizontal, vertical), 1f);
    }

    private void FixedUpdate()
    {
        Vector3 force = new Vector3(moveInput.x, 0f, moveInput.y) * moveForce;
        rb.AddForce(force, ForceMode.Force);
    }
}
