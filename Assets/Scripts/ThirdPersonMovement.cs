using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{

    PlayerInput playerInput;
    InputAction moveAction, jumpAction;
    //CharacterController controller;
    public float speed = 3.0f;
    public float lookSpeed = 5.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float gravity = -9.81f;
    // Start is called before the first frame update
    void Start()
    {
        //controller = GameObject.FindObjectOfType<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        jumpAction = playerInput.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        LookAround();
    }

    public void Moving() {
        // float verticalAxis = Input.GetAxis("Vertical");
        // float horizontalAxis = Input.GetAxis("Horizontal");
        // Vector3 movement = new Vector3(horizontalAxis, 0, verticalAxis).normalized;
        // if(movement.magnitude >= 0.1f) {
        //     float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        //     float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        //     transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //     Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //     controller.Move(moveDir.normalized * speed * Time.deltaTime);
        // }
        // float rotationInput = Input.GetAxis("Mouse X");

        Vector2 direction = moveAction.ReadValue<Vector2>();
        //Vector3 movement = moveAction.ReadValue<Vector3>();
        Vector3 movement = new Vector3(direction.x, 0, direction.y).normalized;
        if(movement.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += movement * speed * Time.deltaTime;
        }

    }

    public void LookAround() {
        float rotationInput = Input.GetAxis("Mouse X");

    }
}
