using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Settings : MonoBehaviour
{
    Volume settingsVolume;

    [Header("VIDEO SETTINGS")]
    [Header("Resolution")]
    [SerializeField] int width = 1920;
    [SerializeField] int height = 1080;
    [SerializeField] TMP_Dropdown dropdownRes;

    [Header("Window Mode")]
    [SerializeField] string windowMode = "FULLSCREEN";
    [SerializeField] TMP_Dropdown dropdownWindow;

    [Header("Vsync")]
    [SerializeField] bool vsync = false;
    [SerializeField] Toggle vsyncToggle;

    [Header("Brightness")]
    [SerializeField] float brightness = 0.5f;
    [SerializeField] Slider brightnessSlider;

    [Header("FPS Counter")]
    [SerializeField] bool fpsCounter = false;
    [SerializeField] Toggle fpsCounterToggle;

    [Header("UI Colour")]
    [SerializeField] string UIcolour = "RED";
    [SerializeField] TMP_Dropdown UIcolourDropdown;

    [Header("GRAPHICS SETTINGS")]
    [Header("Volumetric Fog")]
    [SerializeField] bool volumeFog = true;
    [SerializeField] TMP_Dropdown volumeFogDropdown;

    [Header("Ambient Occlusion")]
    [SerializeField] bool AO = true;
    [SerializeField] TMP_Dropdown aoDropdown;

    [Header("Screen Space Reflections")]
    [SerializeField] bool SSR = true;
    [SerializeField] TMP_Dropdown SSRDropdown;

    [Header("Bloom")]
    [SerializeField] bool bloom = true;
    [SerializeField] TMP_Dropdown bloomDropdown;

    [Header("Chromatic Aberration")]
    [SerializeField] bool cA = true;
    [SerializeField] TMP_Dropdown caDropdown;

    [Header("Contact Shadows")]
    [SerializeField] bool contactShadows = true;
    [SerializeField] TMP_Dropdown contactShadowsDropdown;

    [Header("Micro Shadows")]
    [SerializeField] bool microShadows = true;
    [SerializeField] TMP_Dropdown microShadowsDropdown;

    [Header("Shadow Resolution")]
    [SerializeField] string shadowResolution = "";
    [SerializeField] TMP_Dropdown shadowResolutionDropdown;

    [Header("Screen Space Shadows")]
    [SerializeField] bool SSS = true;
    [SerializeField] TMP_Dropdown SssDropdown;

    [Header("AUDIO SETTINGS")]
    [Header("Music Volume")]
    [SerializeField] float musicVol = 1f;
    [SerializeField] Slider musicVolSlider;

    [Header("SFX Volume")]
    [SerializeField] float sfxVol = 1f;
    [SerializeField] Slider sfxVolSlider;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ApplySettings()
    {
        //Resolution
        switch (dropdownRes.value)
        {
            case 0:
                {
                    width = 1280;
                    height = 720;
                    if (dropdownWindow.value == 0)
                        Screen.SetResolution(1280, 720, FullScreenMode.ExclusiveFullScreen);
                    if(dropdownWindow.value == 1)
                        Screen.SetResolution(1280, 720, FullScreenMode.Windowed);

                    break;
                }
            case 1:
                {
                    width = 1366;
                    height = 768;
                    if (dropdownWindow.value == 0)
                        Screen.SetResolution(1366, 768, FullScreenMode.ExclusiveFullScreen);
                    if (dropdownWindow.value == 1)
                        Screen.SetResolution(1366, 768, FullScreenMode.Windowed);

                    break;
                }
            case 2:
                {
                    width = 1600;
                    height = 900;
                    if (dropdownWindow.value == 0)
                        Screen.SetResolution(1600, 900, FullScreenMode.ExclusiveFullScreen);
                    if (dropdownWindow.value == 1)
                        Screen.SetResolution(1600, 900, FullScreenMode.Windowed);

                    break;
                }
            case 3:
                {
                    width = 1920;
                    height = 1080;
                    if (dropdownWindow.value == 0)
                        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
                    if (dropdownWindow.value == 1)
                        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);

                    break;
                }
            case 4:
                {
                    width = 2048;
                    height = 1152;
                    if (dropdownWindow.value == 0)
                        Screen.SetResolution(2048, 1152, FullScreenMode.ExclusiveFullScreen);
                    if (dropdownWindow.value == 1)
                        Screen.SetResolution(2048, 1152, FullScreenMode.Windowed);

                    break;
                }
            case 5:
                {
                    width = 2560;
                    height = 1440;
                    if (dropdownWindow.value == 0)
                        Screen.SetResolution(2560, 1440, FullScreenMode.ExclusiveFullScreen);
                    if (dropdownWindow.value == 1)
                        Screen.SetResolution(2560, 1440, FullScreenMode.Windowed);

                    break;
                }
            case 6:
                {
                    width = 3840;
                    height = 2160;
                    if (dropdownWindow.value == 0)
                        Screen.SetResolution(3840, 2160, FullScreenMode.ExclusiveFullScreen);
                    if (dropdownWindow.value == 1)
                        Screen.SetResolution(3840, 2160, FullScreenMode.Windowed);

                    break;
                }
        }

        //Vsync
        if (vsyncToggle.isOn == true)
        {
            QualitySettings.vSyncCount = 1;
            vsync = false;
        }
        else if (vsyncToggle.isOn == false)
        {
            QualitySettings.vSyncCount = 0;
            vsync = false;
        }

        //Brightness

        //FPS Counter

        //UIColour

        //Volumetric Fog
        if (volumeFogDropdown.value == 0)
        {
            volumeFog = true;
        }
        else volumeFog = false;

        //AO
        if(aoDropdown.value == 0)
        {
            AO = true;
        }
        else AO = false;

        //SSR
        if (SSRDropdown.value == 0)
        {
            SSR = true;
        }
        else SSR = false;

        //bloom
        if (bloomDropdown.value == 0)
        {
            bloom = true;
        }
        else bloom = false;

        //CA
        if (caDropdown.value == 0)
        {
            cA = true;
        }
        else cA = false;
        
        //AO
        if (microShadowsDropdown.value == 0)
        {
            microShadows = true;
        }
        else microShadows = false;

        //MicroShadows
        if (microShadowsDropdown.value == 0)
        {
            microShadows = true;
        }
        else microShadows = false;

        //Shadow Res
        //if (shadowResolutionDropdown.value == 0)
        //{
        //    shadowResolution = true;
        //}
        //else shadowResolution = false;

        //SSS
        if (SssDropdown.value == 0)
        {
            SSS = true;
        }
        else SSS = false;

        musicVol = musicVolSlider.value;
        sfxVol = sfxVolSlider.value;
    }

    private void OnLevelWasLoaded()
    {
        Volume volume = null ;
        GameObject.FindWithTag("GraphicsSettings").TryGetComponent<Volume>(out volume);

        if (volume != null)
        {
            Fog fog;
            if (volume.profile.TryGet<Fog>(out fog))
            {
                fog.active = volumeFog;
            }

            AmbientOcclusion ao;
            if (volume.profile.TryGet<AmbientOcclusion>(out ao))
            {
                ao.active = AO;
            }

            ScreenSpaceReflection ssr;
            if (volume.profile.TryGet<ScreenSpaceReflection>(out ssr))
            {
                ssr.active = SSR;
            }

            Bloom blm;
            if (volume.profile.TryGet<Bloom>(out blm))
            {
                blm.active = bloom;
            }

            ChromaticAberration chromaticAberration;
            if (volume.profile.TryGet<ChromaticAberration>(out chromaticAberration))
            {
                chromaticAberration.active = cA;
            }

            ContactShadows contctShadows;
            if (volume.profile.TryGet<ContactShadows>(out contctShadows))
            {
                contctShadows.active = contactShadows;
            }

            MicroShadowing microShadowing;
            if (volume.profile.TryGet<MicroShadowing>(out microShadowing))
            {
                microShadowing.active = microShadows;
            }

            List<AudioSource> allMusic = new List<AudioSource>();
            var foundAudioSources = FindObjectsOfType<AudioSource>();

            foreach (AudioSource source in foundAudioSources)
            {
                source.volume = musicVol;
            }
        }
    }
}
