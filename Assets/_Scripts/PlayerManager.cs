using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{
    GameInputs _inputs;

    Rigidbody rb;
    Camera cam;
    Volume volume;

    [Header("Player Stats")]
    [SerializeField] float health = 100f;
    [SerializeField] bool inLight = false;

    float damage = 0.5f;
    Coroutine damageTimer = null;

    [Header("Player Movement")]
    [SerializeField] Vector2 moveInput;
    [SerializeField] float moveForce = 25f;
    [SerializeField] float topSpeed = 7f;

    [Header("Dash")]
    [SerializeField] float dashForce = 3f;
    [SerializeField] float dashTimer = 3f;
    bool canDash = true;
    [SerializeField] Image dashIndicator;
    bool dashReload = false;

    private void OnDisable()
    {
        _inputs.Player.Disable();
    }
    private void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        _inputs = new GameInputs();
        _inputs.Player.Enable();
        volume = GameObject.FindWithTag("GraphicsSettings").GetComponent<Volume>();

        _inputs.Player.Dash.performed += x => StartCoroutine(DashTimer());

        damageTimer = null;
    }
    private void FixedUpdate()
    {
        Move();

        if (damageTimer == null)
        {
            damageTimer = StartCoroutine(TakeDamage());
        }
    }
    private void Move()
    {
        moveInput = _inputs.Player.Move.ReadValue<Vector2>();

        if (rb.velocity.magnitude < topSpeed)
        {
            Vector3 moveDirection;
            moveDirection = (cam.transform.right * moveInput.x * moveForce) + (cam.transform.up * moveInput.y * moveForce);
            rb.AddForce(moveDirection);
        }
    }

    IEnumerator updateDashUI()
    {
        if (dashReload == false)
        {
            while ((dashIndicator.fillAmount <= 1) && (dashIndicator.fillAmount > 0))
            {
                dashIndicator.fillAmount -= 3.33f;
                yield return new WaitForFixedUpdate();
            }
            dashReload = true;
            
            if(dashReload)
            {
                while(dashIndicator.fillAmount != 1)
                {
                    dashIndicator.fillAmount += 0.0065f;
                    yield return new WaitForFixedUpdate();
                }
                dashReload = false;
            }
        }




    }

    IEnumerator DashTimer()
    {
        if (canDash == true)
        {
            canDash = false;
            rb.AddForce((cam.transform.right * moveInput.x + cam.transform.up * moveInput.y) * dashForce, ForceMode.Impulse);
            StartCoroutine(updateDashUI());
            yield return new WaitForSeconds(dashTimer);
            canDash = true;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("DynamicLights"))
        {
            inLight = true;
        }
        else inLight = false;
    }
    IEnumerator TakeDamage()
    {
        if (inLight)
        {
            if(health > 0)
            {
                health = health - damage;
                Vignette temp;
                if(volume.profile.TryGet<Vignette>(out temp))
                {
                    temp.intensity = new ClampedFloatParameter(((100f - health) / 100f), 0, 1, true);
                }
                yield return new WaitForSeconds(0.1f);
                damageTimer = null;

            }
        }
        else
        {
            if (health < 100)
            {
                health = health + 1;
                Vignette temp;
                if (volume.profile.TryGet<Vignette>(out temp))
                {
                    temp.intensity = new ClampedFloatParameter(((100f - health)/100f), 0, 1, true);
                }
                yield return new WaitForSeconds(0.1f);
                damageTimer = null;
            }
        }

        
        
    }
}
