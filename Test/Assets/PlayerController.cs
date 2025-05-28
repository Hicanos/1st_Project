using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed = 5;
    private Vector2 curMovementInput;

    [Header("Look")]
    private Transform Player;
    private Transform CameraContainer;
    private float minXLook = -85;
    private float maxXLook = 85;
    private float camCurXRot;
    private float lookSensitivity = 0.1f;
    private Vector2 mouseDelta;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Player = transform;
        CameraContainer = transform.GetChild(0);
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        CameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        Player.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    void Move()
    {
        Vector3 dir = Player.forward * curMovementInput.y + Player.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}