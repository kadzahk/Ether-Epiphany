using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown ResDropdown;
    public AudioMixer audioMixer;

    private void Start()
    {
        int current_res = 0;
        resolutions = Screen.resolutions;
        ResDropdown.ClearOptions();
        List<string> ResOptions = new List<string>();

        for(int i = 0; i < resolutions.Length; i++)
        {
            string Opt = resolutions[i].width + " x " + resolutions[i].height;
            ResOptions.Add(Opt);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                current_res = i;
            }
        }

        ResDropdown.AddOptions(ResOptions);
        ResDropdown.value = current_res;
        ResDropdown.RefreshShownValue();
    }

    // SetVolume is called to change game music volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
    }

    // SetResolution is called to change the resolution view
    public void SetResolution(int index_r)
    {
        Resolution resolution = resolutions[index_r];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // SetQuality is called to change game graphics quality
    public void SetQuality(int index_q)
    {
        QualitySettings.SetQualityLevel(index_q);
    }

    // SetFullscreen is called to change fullscreen
    public void SetFullscreen(bool is_full)
    {
        Screen.fullScreen = is_full;
    }
}
