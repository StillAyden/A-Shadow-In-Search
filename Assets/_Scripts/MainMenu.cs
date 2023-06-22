using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

    private void OnDisable()
    {
        gameInputs.Game.MenuNavigationUp.performed -= x => UpperSelection();
        gameInputs.Game.MenuNavigationDown.performed -= x => LowerSelection();

        gameInputs.Game.Disable();
    }

    private void Awake()
    {
        gameInputs = new GameInputs();
        gameInputs.Game.Enable();

        selectedOption = menuTabs.Start;
        highlightStart();

        gameInputs.Game.MenuNavigationUp.performed += x => UpperSelection();
        gameInputs.Game.MenuNavigationDown.performed += x => LowerSelection();
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

    public void highlightStart()
    {
        selectedOption = menuTabs.Start;
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(270, 180, 0);
    }

    public void highlightSettings()
    {
        selectedOption = menuTabs.Settings;
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(270, 60, 0);
    }

    public void highlightCredits()
    {
        selectedOption = menuTabs.Credits;
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(270, -60, 0);
    }

    public void highlightExit()
    {
        selectedOption = menuTabs.Exit;
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(270, -180, 0);
    }
}
