using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject _settingsPanel;

    [SerializeField]
    private Slider _musicVolumeSlider;

    [SerializeField]
    private MMF_Player _openPanelFeedback;

    [SerializeField]
    private MMF_Player _closePanelFeedback;

    void Start()
    {
        _settingsPanel.SetActive(false);
        SetMusicVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume()
    {
        MMSoundManager.Current.SetVolumeMusic(_musicVolumeSlider.value);
    }

    public void ToggleSfx()
    {

        if (!MMSoundManager.Current.IsMuted(MMSoundManager.MMSoundManagerTracks.Sfx)) // note: it's negated, because isMuted returns for some reason the inverted value?!
        {
            MMSoundManager.Current.UnmuteSfx();
        } else
        {
            MMSoundManager.Current.MuteSfx();
        }
    }

    public void ToggleMusic()
    {
        if (!MMSoundManager.Current.IsMuted(MMSoundManager.MMSoundManagerTracks.Music)) // note: it's negated, because isMuted returns for some reason the inverted value?!
        {
            Debug.Log("Unmuting music");
            MMSoundManager.Current.UnmuteMusic();
        }
        else
        {
            Debug.Log("Muting music");
            MMSoundManager.Current.MuteMusic();
        }
    }

    public void TogglePanel()
    {
        if (_settingsPanel.activeSelf)
        {
            HidePanel();
        }
        else
        {
            ShowPanel();
        }
    }

    public void ShowPanel()
    {
        _openPanelFeedback.PlayFeedbacks();
    }

    public void HidePanel()
    {
        _closePanelFeedback.PlayFeedbacks();
    }
}
