using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

enum settingsTabs
{
    Video,
    Graphics,
    Audio,
    Controls
}

enum videoSettings 
{ 
    none,
    Resolution,
    windowMode,
    Vsync,
    Brightness,
    FPS,
    UICol
}
enum graphicsSettings 
{
    none,
    Volumetrics,
    AO,
    SSR,
    Bloom,
    Chromatic,
    ContactShadows,
    MicroShadows,
    ShadowResolution,
    SSS
}
enum audioSettings 
{
    none,
    MusicVol,
    SFXVol,
}

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] settingsTabs currentTab;
    [SerializeField] videoSettings currentVideoSetting;
    [SerializeField] graphicsSettings currentGraphicsSetting;
    [SerializeField] audioSettings currentAudioSetting;

    GameInputs gameInputs;
    [SerializeField] Image tabCursor;
    [SerializeField] Image verticalCursor;

    [SerializeField] Animator backAnim;
    [SerializeField] Canvas menuCanvas;

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

    private void OnEnable()
    {
        gameInputs.Game.Enable();
    }

    private void Awake()
    {
        gameInputs = new GameInputs();
        gameInputs.Game.Enable();

        currentTab = settingsTabs.Video;
        GoToVideo();

        currentVideoSetting = videoSettings.none;
        currentGraphicsSetting = graphicsSettings.none;
        currentAudioSetting = audioSettings.none;

        gameInputs.Game.TabNavigationLeft.performed += LeftMenu;
        gameInputs.Game.TabNavigationRight.performed += RightMenu;

        gameInputs.Game.UnpauseAndBack.performed += x => BackToMenu();

    }

    #region SettingsTabs
    public void GoToVideo()
    {
        currentTab = settingsTabs.Video;
        videoPanel.SetActive(true);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(false);
        tabCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(486, -173, 0);

        currentVideoSetting = videoSettings.none;
        currentGraphicsSetting = graphicsSettings.none;
        currentAudioSetting = audioSettings.none;
        verticalCursor.gameObject.SetActive(false);



        gameInputs.Game.MenuNavigationUp.performed -= UpGraphicsSettings;
        gameInputs.Game.MenuNavigationDown.performed -= DownGraphicsSettings;

        gameInputs.Game.MenuNavigationUp.performed -= UpAudioSettings;
        gameInputs.Game.MenuNavigationDown.performed -= DownAudioSettings;

        gameInputs.Game.MenuNavigationUp.performed += UpVideoSettings;
        gameInputs.Game.MenuNavigationDown.performed += DownVideoSettings;

    }
    public void GoToGraphics()
    {
        currentTab = settingsTabs.Graphics;
        videoPanel.SetActive(false);
        graphicsPanel.SetActive(true);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(false);
        tabCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(805, -173, 0);

        currentVideoSetting = videoSettings.none;
        currentGraphicsSetting = graphicsSettings.none;
        currentAudioSetting = audioSettings.none;
        verticalCursor.gameObject.SetActive(false);


        gameInputs.Game.MenuNavigationUp.performed -= UpVideoSettings;
        gameInputs.Game.MenuNavigationDown.performed -= DownVideoSettings;

        gameInputs.Game.MenuNavigationUp.performed -= UpAudioSettings;
        gameInputs.Game.MenuNavigationDown.performed -= DownAudioSettings;
        
        gameInputs.Game.MenuNavigationUp.performed += UpGraphicsSettings;
        gameInputs.Game.MenuNavigationDown.performed += DownGraphicsSettings;
    }
    public void GoToAudio()
    {
        currentTab = settingsTabs.Audio;
        videoPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(true);
        controlsPanel.SetActive(false);
        tabCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(1120, -173, 0);

        currentVideoSetting = videoSettings.none;
        currentGraphicsSetting = graphicsSettings.none;
        currentAudioSetting = audioSettings.none;
        verticalCursor.gameObject.SetActive(false);

        gameInputs.Game.MenuNavigationUp.performed -= UpVideoSettings;
        gameInputs.Game.MenuNavigationDown.performed -= DownVideoSettings;

        gameInputs.Game.MenuNavigationUp.performed -= UpGraphicsSettings;
        gameInputs.Game.MenuNavigationDown.performed -= DownGraphicsSettings;

        gameInputs.Game.MenuNavigationUp.performed += UpAudioSettings;
        gameInputs.Game.MenuNavigationDown.performed += DownAudioSettings;
    }
    public void GoToControls()
    {
        currentTab = settingsTabs.Controls;
        videoPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(true);
        tabCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(1430, -173, 0);

        gameInputs.Game.MenuNavigationUp.performed -= UpVideoSettings;
        gameInputs.Game.MenuNavigationDown.performed -= DownVideoSettings;

        gameInputs.Game.MenuNavigationUp.performed -= UpGraphicsSettings;
        gameInputs.Game.MenuNavigationDown.performed -= DownGraphicsSettings;

        gameInputs.Game.MenuNavigationUp.performed -= UpAudioSettings;
        gameInputs.Game.MenuNavigationDown.performed -= DownAudioSettings;

        currentVideoSetting = videoSettings.none;
        currentGraphicsSetting = graphicsSettings.none;
        currentAudioSetting = audioSettings.none;
        verticalCursor.gameObject.SetActive(false);
    }

    private void LeftMenu(InputAction.CallbackContext context)
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

    private void RightMenu(InputAction.CallbackContext context)
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
    #endregion

    #region VideoSettings

    public void GoToResolution()
    {
        verticalCursor.gameObject.SetActive(true);
        currentVideoSetting = videoSettings.Resolution;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -370, 0);
    }
    public void GoToWindowMode()
    {
        verticalCursor.gameObject.SetActive(true);
        currentVideoSetting = videoSettings.windowMode;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -430, 0);
    }
    public void GoToVsync()
    {
        verticalCursor.gameObject.SetActive(true);
        currentVideoSetting = videoSettings.Vsync;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -490, 0);
    }
    public void GoToBrightness()
    {
        verticalCursor.gameObject.SetActive(true);
        currentVideoSetting = videoSettings.Brightness;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -550, 0);
    }
    public void GoToFPS()
    {
        verticalCursor.gameObject.SetActive(true);
        currentVideoSetting = videoSettings.FPS;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -610, 0);
    }
    public void GoToUIColour()
    {
        verticalCursor.gameObject.SetActive(true);
        currentVideoSetting = videoSettings.UICol;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -670, 0);
    }

    private void UpVideoSettings(InputAction.CallbackContext context)
    {
        if ((int)currentVideoSetting > 1)
        {
            currentVideoSetting--;

            switch ((int)currentVideoSetting)
            {
                case 1: GoToResolution(); break;
                case 2: GoToWindowMode(); break;
                case 3: GoToVsync(); break;
                case 4: GoToBrightness(); break;
                case 5: GoToFPS(); break;
                case 6: GoToUIColour(); break;
            }
        }
    }

    private void DownVideoSettings(InputAction.CallbackContext context)
    {
        if ((int)currentVideoSetting < 6)
        {
            verticalCursor.gameObject.SetActive(true);
            currentVideoSetting++;

            switch ((int)currentVideoSetting)
            {
                case 1: GoToResolution(); break;
                case 2: GoToWindowMode(); break;
                case 3: GoToVsync(); break;
                case 4: GoToBrightness(); break;
                case 5: GoToFPS(); break;
                case 6: GoToUIColour(); break;
            }
        }
    }
    #endregion

    #region GraphicsSettings

    public void GoToVolumetricFog()
    {
        verticalCursor.gameObject.SetActive(true);
        currentGraphicsSetting = graphicsSettings.Volumetrics;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -370, 0);
    }
    public void GoToAO()
    {
        verticalCursor.gameObject.SetActive(true);
        currentGraphicsSetting = graphicsSettings.AO;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -430, 0);
    }
    public void GoToSSR()
    {
        verticalCursor.gameObject.SetActive(true);
        currentGraphicsSetting = graphicsSettings.SSR;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -490, 0);
    }
    public void GoToBloom()
    {
        verticalCursor.gameObject.SetActive(true);
        currentGraphicsSetting = graphicsSettings.Bloom;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -550, 0);
    }
    public void GoToChromatic()
    {
        verticalCursor.gameObject.SetActive(true);
        currentGraphicsSetting = graphicsSettings.Chromatic;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -610, 0);
    }
    public void GoToContactShadows()
    {
        verticalCursor.gameObject.SetActive(true);
        currentGraphicsSetting = graphicsSettings.ContactShadows;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -670, 0);
    }
    public void GoToMicroShadows()
    {
        verticalCursor.gameObject.SetActive(true);
        currentGraphicsSetting = graphicsSettings.MicroShadows;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -730, 0);
    }
    public void GoToShadowResolution()
    {
        verticalCursor.gameObject.SetActive(true);
        currentGraphicsSetting = graphicsSettings.ShadowResolution;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -790, 0);
    }
    public void GoToSSS()
    {
        verticalCursor.gameObject.SetActive(true);
        currentGraphicsSetting = graphicsSettings.SSS;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -850, 0);
    }

    private void UpGraphicsSettings(InputAction.CallbackContext context)
    {
        if ((int)currentGraphicsSetting > 1)
        {
            currentGraphicsSetting--;

            switch ((int)currentGraphicsSetting)
            {
                case 1: GoToVolumetricFog(); break;
                case 2: GoToAO(); break;
                case 3: GoToSSR(); break;
                case 4: GoToBloom(); break;
                case 5: GoToChromatic(); break;
                case 6: GoToContactShadows(); break;
                case 7: GoToMicroShadows(); break;
                case 8: GoToShadowResolution(); break;
                case 9: GoToSSS(); break;
            }
        }
    }

    private void DownGraphicsSettings(InputAction.CallbackContext context)
    {
        if ((int)currentGraphicsSetting < 9)
        {
            verticalCursor.gameObject.SetActive(true);
            currentGraphicsSetting++;

            switch ((int)currentGraphicsSetting)
            {
                case 1: GoToVolumetricFog(); break;
                case 2: GoToAO(); break;
                case 3: GoToSSR(); break;
                case 4: GoToBloom(); break;
                case 5: GoToChromatic(); break;
                case 6: GoToContactShadows(); break;
                case 7: GoToMicroShadows(); break;
                case 8: GoToShadowResolution(); break;
                case 9: GoToSSS(); break;
            }
        }
    }
    #endregion

    #region AudioSettings

    public void GoToMusicVolume()
    {
        verticalCursor.gameObject.SetActive(true);
        currentAudioSetting = audioSettings.MusicVol;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -370, 0);
    }
    public void GoToSFXVolume()
    {
        verticalCursor.gameObject.SetActive(true);
        currentAudioSetting = audioSettings.SFXVol;
        verticalCursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(950, -430, 0);
    }

    private void UpAudioSettings(InputAction.CallbackContext context)
    {
        if ((int)currentAudioSetting > 1)
        {
            currentAudioSetting--;

            switch ((int)currentAudioSetting)
            {
                case 1: GoToMusicVolume(); break;
                case 2: GoToSFXVolume(); break;
            }
        }
    }

    private void DownAudioSettings(InputAction.CallbackContext context)
    {
        if ((int)currentAudioSetting < 2)
        {
            verticalCursor.gameObject.SetActive(true);
            currentAudioSetting++;

            switch ((int)currentAudioSetting)
            {
                case 1: GoToMusicVolume(); break;
                case 2: GoToSFXVolume(); break;
            }
        }
    }
    #endregion

    public void BackToMenu()//InputAction.CallbackContext context
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        backAnim.SetTrigger("BackToMenu");
        StartCoroutine(menuTimer());
    }

    IEnumerator menuTimer()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        menuCanvas.enabled = true;
        menuCanvas.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
