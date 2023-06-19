using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    GameInputs _inputs;

    Rigidbody rb;

    [Header("Player Movement")]
    [SerializeField] Vector2 moveInput;
    [SerializeField] float moveForce = 25f;
    [SerializeField] float topSpeed = 7f;

    [Header("Dash")]
    [SerializeField] float dashForce = 3f;
    [SerializeField] float dashTimer = 3f;
    bool canDash = true;

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        _inputs.Player.Disable();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _inputs = new GameInputs();
        _inputs.Player.Enable();

        _inputs.Player.Dash.performed += x => StartCoroutine(DashTimer());
    }
    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        moveInput = _inputs.Player.Move.ReadValue<Vector2>();

        //rb.velocity = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed;
        if (rb.velocity.magnitude < topSpeed)
        {
            rb.AddForce(new Vector3(moveInput.x, 0f, moveInput.y) * moveForce);
        }
    }

    IEnumerator DashTimer()
    {
        if (canDash == true)
        {
            canDash = false;
            rb.AddForce(new Vector3(moveInput.x, 0f, moveInput.y) * dashForce, ForceMode.Impulse);
            yield return new WaitForSeconds(dashTimer);
            canDash = true;
        }
    }
}
