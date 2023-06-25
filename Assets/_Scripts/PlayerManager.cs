using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerManager : MonoBehaviour
{
    GameInputs _inputs;
    GameManager gameManager;

    Rigidbody rb;
    Camera cam;
    Volume volume;

    [Header("Player Stats")]
    [SerializeField] public float health = 100f;
    [SerializeField] bool inLight = false;
    [SerializeField] bool hasKey = false;

    float damage = 0.6f;
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

    [Header("Health")]
    [SerializeField] Image healthIndicator;

    [Header("UI")]
    [SerializeField] GameObject interactPanel;
    [SerializeField] Canvas canvasFoundKey; 

    private void OnDisable()
    {
        _inputs.Player.Disable();
    }
    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
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

        healthIndicator.fillAmount = health/100;

        if(health <= 0)
        {
            StartCoroutine(gameManager.GameOver());
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
                    dashIndicator.fillAmount += 0.0068f;
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

    IEnumerator foundKey()
    {
        canvasFoundKey.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        canvasFoundKey.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(col.gameObject);
            StartCoroutine(foundKey());
        }

        if (col.CompareTag("End"))
        {
            gameManager.End();
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("DynamicLights"))
        {
            inLight = true;
        }
        else inLight = false;

        if ((col.CompareTag("OutsideDoor") && interactPanel.gameObject.activeSelf == false) || (col.CompareTag("Door") && interactPanel.gameObject.activeSelf == false))
        {
            interactPanel.gameObject.SetActive(true);
        }

        if(col.CompareTag("OutsideDoor") && _inputs.Player.Interact.IsPressed())
        {
            SceneManager.LoadScene("House_DownLevel");
        }

        if ((col.CompareTag("Door") && hasKey) && _inputs.Player.Interact.IsPressed())
        {
            SceneManager.LoadScene("House_UpLevel");
        }
        else if (col.CompareTag("Door") && !hasKey && _inputs.Player.Interact.IsPressed())
        {
            Debug.Log("You need to find a Key");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("DynamicLights"))
        {
            inLight = false;
        }

        if ((col.CompareTag("OutsideDoor") && interactPanel.gameObject.activeSelf == true) || (col.CompareTag("Door") && interactPanel.gameObject.activeSelf == true))
        {
            interactPanel.gameObject.SetActive(false);
        }
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
                yield return new WaitForSeconds(0.005f);
                damageTimer = null;

            }
        }
        else
        {
            if (health < 100)
            {
                health = health + 0.5f;
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
