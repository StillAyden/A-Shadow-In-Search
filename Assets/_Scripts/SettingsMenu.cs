using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

enum settingsTabs
{
    Video = 0,
    Graphics,
    Audio,
    Controls
}

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] settingsTabs currentTab;
    GameInputs gameInputs;
    [SerializeField] Image cursor;

    [Header("Video")]
    [SerializeField] GameObject videoPanel;

    [Header("Graphics")]
    [SerializeField] GameObject graphicsPanel;

    [Header("Audio")]
    [SerializeField] GameObject audioPanel;

    [Header("Controls")]
    [SerializeField] GameObject controlsPanel;

    private void OnDisable()
    {
        gameInputs.Game.Disable();
    }

    private void Awake()
    {
        gameInputs = new GameInputs();
        gameInputs.Game.Enable();

        currentTab = settingsTabs.Video;
        GoToVideo();

        gameInputs.Game.TabNavigationLeft.performed += x => LeftMenu();
        gameInputs.Game.TabNavigationRight.performed += x => RightMenu();
    }
    public void GoToVideo()
    {
        currentTab = settingsTabs.Video;
        videoPanel.SetActive(true);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(false);
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(486, -173, 0);

    }
    public void GoToGraphics()
    {
        currentTab = settingsTabs.Graphics;
        videoPanel.SetActive(false);
        graphicsPanel.SetActive(true);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(false);
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(805, -173, 0);
    }
    public void GoToAudio()
    {
        currentTab = settingsTabs.Audio;
        videoPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(true);
        controlsPanel.SetActive(false);
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(1120, -173, 0);
    }
    public void GoToControls()
    {
        currentTab = settingsTabs.Controls;
        videoPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(true);
        cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(1430, -173, 0);
    }

    private void LeftMenu()
    {
        if ((int)currentTab > 0)
        {
            currentTab--;

            switch ((int)currentTab)
            {
                case 0: GoToVideo(); break;
                case 1: GoToGraphics(); break;
                case 2: GoToAudio(); break;
                case 3: GoToControls(); break;
            }
        }
    }

    private void RightMenu()
    {
        if((int)currentTab < 3)
        {
            currentTab++;

            switch ((int)currentTab)
            {
                case 0: GoToVideo(); break;
                case 1: GoToGraphics(); break;
                case 2: GoToAudio(); break;
                case 3: GoToControls(); break;
            }
        }
    }
}
