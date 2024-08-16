using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{

    PlayerInput playerInput;
    InputAction moveAction, jumpAction;

    private Rigidbody rb;
    //CharacterController controller;
    public float speed = 3.0f;
    public float dashForce = 2.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float jumpForce = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        //controller = GameObject.FindObjectOfType<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        moveAction = playerInput.actions.FindAction("Move");
        jumpAction = playerInput.actions.FindAction("Jump");
    }

    private void OnEnable() {

    }

    private void OnDisable() {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Moving();
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
        Vector3 movement = new Vector3(direction.x, 0, direction.y);
        if(movement.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += moveDir.normalized * speed * Time.deltaTime;
        }

    }

    public void Jump() {
        if(IsGrounded()) {
            Debug.Log("jump goes here lol");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Dash() {
        //Quaternion.Euler(0f, transform.rotation.y, 0f) * 
        //rb.AddForce(Vector3.forward * dashForce, ForceMode.Impulse);
        if(IsGrounded()) {
            rb.AddRelativeForce(Vector3.forward * dashForce * 2, ForceMode.Impulse);
        }
        else
        rb.AddRelativeForce(Vector3.forward * dashForce, ForceMode.Impulse);
    }

    private bool IsGrounded() {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, 1.5f)) {
            Debug.Log("Touching Ground!");
            return true;
        }
        else {
            Debug.Log("Somethings wrong");
            return false;
        } 
    }
    
    private void OnApplicationFocus(bool focus) {
        if(focus) {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
