using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class MenuSettings : MonoBehaviour
{
    
    public TMP_Dropdown resolutionDropdown;

    public AudioMixer MasterAudio;

    Resolution[] resolutions;

    private void Start()
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height
                && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }
        //options.Reverse();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }



    public void ChangeGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void SetMasterVolume(float volume)
    {
        MasterAudio.SetFloat("MasterMixer", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        MasterAudio.SetFloat("EffectsMixer", volume);
    }

    public void SetMusicVolume(float volume)
    {
        MasterAudio.SetFloat("MusicMixer", volume);
    }
}
