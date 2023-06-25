using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

enum menuTabs
{
    Start = 0,
    Settings,
    Credits,
    Exit
}

public class MainMenu : MonoBehaviour
{
    [SerializeField] menuTabs selectedOption;
    GameInputs gameInputs;
    [SerializeField] Image cursor;
    [SerializeField] Canvas settingsCanvas;
    [SerializeField] Canvas creditsCanvas;

    [SerializeField] Animator backAnim;
    [SerializeField] Image loadIcon;
    [SerializeField] AudioSource music;

    private void OnDisable()
    {
        gameInputs.Game.MenuNavigationUp.performed -= x => UpperSelection();
        gameInputs.Game.MenuNavigationDown.performed -= x => LowerSelection();

        gameInputs.Game.Disable();
    }

    private void OnEnable()
    {
        //gameInputs.Game.MenuNavigationUp.performed += x => UpperSelection();
        //gameInputs.Game.MenuNavigationDown.performed += x => LowerSelection();

        gameInputs.Game.Enable();
    }

    private void Awake()
    {
        gameInputs = new GameInputs();
        gameInputs.Game.Enable();

        selectedOption = menuTabs.Start;
        highlightStart();

        gameInputs.Game.MenuNavigationUp.performed += x => UpperSelection();
        gameInputs.Game.MenuNavigationDown.performed += x => LowerSelection();
        gameInputs.Game.Select.performed += x => EnterSelectedMenu();
    }

    void UpperSelection()
    {
        if ((int)selectedOption > 0)
        {
            selectedOption--;

            switch ((int)selectedOption)
            {
                case 0: highlightStart(); break;
                case 1: highlightSettings(); break;
                case 2: highlightCredits(); break;
                case 3: highlightExit(); break;
            }
        }
    }
    void LowerSelection()
    {
        if ((int)selectedOption < 3)
        {
            selectedOption++;

            switch ((int)selectedOption)
            {
                case 0: highlightStart(); break;
                case 1: highlightSettings(); break;
                case 2: highlightCredits(); break;
                case 3: highlightExit(); break;
            }
        }
    }

    void EnterSelectedMenu()
    {
        switch ((int)selectedOption)
        {
            case 0: ClickStart(); break;
            case 1: ClickSettings(); break;
            case 2: ClickCredits(); break;
            case 3: ClickExit(); break;
        }
    }

    public void highlightStart()
    {
        selectedOption = menuTabs.Start;
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(270, 180, 0);
    }

    public void ClickStart()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        backAnim.SetTrigger("SettingsClick");
        StartCoroutine(StopMusic());
        StartCoroutine(timerPlay());
    }

    IEnumerator timerPlay()
    {
        loadIcon.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene(1);
    }

    IEnumerator StopMusic()
    {
        while (music.volume > 0)
        {
            music.volume = music.volume - 0.02f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void highlightSettings()
    {
        selectedOption = menuTabs.Settings;
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(270, 60, 0);
    }

    public void ClickSettings()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        backAnim.SetTrigger("SettingsClick");
        StartCoroutine(SettingsTimer());
    }

    IEnumerator SettingsTimer()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        settingsCanvas.gameObject.SetActive(true);
        settingsCanvas.enabled = true;
        this.gameObject.SetActive(false);
    }

    public void highlightCredits()
    {
        selectedOption = menuTabs.Credits;
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(270, -60, 0);
    }

    public void ClickCredits()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        backAnim.SetTrigger("SettingsClick");
        StartCoroutine(CreditsTimer());
    }

    IEnumerator CreditsTimer()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        creditsCanvas.gameObject.GetComponent<Canvas>().enabled = true;
        this.gameObject.SetActive(false);
    }

    public void highlightExit()
    {
        selectedOption = menuTabs.Exit;
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(270, -180, 0);
    }

    public void ClickExit()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
