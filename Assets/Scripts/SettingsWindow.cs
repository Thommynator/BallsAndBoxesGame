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
    private Slider _sfxVolumeSlider;

    [SerializeField]
    private MMF_Player _openPanelFeedback;

    [SerializeField]
    private MMF_Player _closePanelFeedback;

    [SerializeField]
    private Image _musicImage;

    [SerializeField]
    private Sprite _musicOnSprite;

    [SerializeField]
    private Sprite _musicOffSprite;

    [SerializeField]
    private Image _sfxImage;

    [SerializeField]
    private Sprite _sfxOnSprite;

    [SerializeField]
    private Sprite _musicOnOffSprite;

    void Start()
    {
        _settingsPanel.SetActive(false);
        SetMusicVolume();
        SetSfxVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume()
    {
        MMSoundManager.Current.SetVolumeMusic(_musicVolumeSlider.value);
    }

    public void SetSfxVolume()
    {
        MMSoundManager.Current.SetVolumeSfx(_sfxVolumeSlider.value);
        MMSoundManager.Current.SetVolumeUI(_sfxVolumeSlider.value);
    }

    public void ToggleMusic()
    {
        if (!MMSoundManager.Current.IsMuted(MMSoundManager.MMSoundManagerTracks.Music)) // note: it's negated, because isMuted returns for some reason the inverted value?!
        {
            MMSoundManager.Current.UnmuteMusic();
            _musicImage.sprite = _musicOnSprite;
            _musicVolumeSlider.interactable = true;
        }
        else
        {   
            MMSoundManager.Current.MuteMusic();
            _musicImage.sprite = _musicOffSprite;
            _musicVolumeSlider.interactable = false;
        }
    }

    public void ToggleSfx()
    {

        if (!MMSoundManager.Current.IsMuted(MMSoundManager.MMSoundManagerTracks.Sfx)) // note: it's negated, because isMuted returns for some reason the inverted value?!
        {
            MMSoundManager.Current.UnmuteSfx();
            MMSoundManager.Current.UnmuteUI();
            _sfxImage.sprite = _sfxOnSprite;
            _sfxVolumeSlider.interactable = true;
        }
        else
        {
            MMSoundManager.Current.MuteSfx();
            MMSoundManager.Current.MuteUI();
            _sfxImage.sprite = _musicOnOffSprite;
            _sfxVolumeSlider.interactable = false;
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
