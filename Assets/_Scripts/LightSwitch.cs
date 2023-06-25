using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    GameInputs _inputs;
    bool isInteractPressed;
    [SerializeField] List<Light> assignedLights;
    public bool isOn;
    Light indicatorLight;
     GameObject zone;

    //UI
    [SerializeField] GameObject interactPanel;

    private void Awake()
    {
        _inputs = new GameInputs();
        _inputs.Player.Enable();
        indicatorLight = GetComponentInChildren<Light>();

        updateLights();

        if (isOn)
            indicatorLight.GetComponent<Light>().color = Color.green;
        else indicatorLight.GetComponent<Light>().color = Color.red;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            interactPanel.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (_inputs.Player.Interact.triggered)
            {
                if (isOn)
                {
                    //Turn off lights
                    isOn = false;
                    updateLights();
                    indicatorLight.GetComponent<Light>().color = Color.red;

                }
                else
                {
                    //Turn on lights
                    isOn = true;
                    updateLights();
                    indicatorLight.GetComponent<Light>().color = Color.green;
                }
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            interactPanel.SetActive(false);
        }
    }

    public void updateLights()
    {
        foreach (Light light in assignedLights)
        {
            light.gameObject.SetActive(isOn);
        }
    }
}
