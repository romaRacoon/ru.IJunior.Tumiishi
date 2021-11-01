using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Sprite[] _sprites;

    private bool _isPlayingMusic = false;
    private Image[] _imageButtons = new Image[2];

    private void Start()
    {
        _audioSource.playOnAwake = false;
        _audioSource.loop = true;

        _imageButtons[0] = _musicButton.GetComponent<Image>();
        _imageButtons[1] = _soundButton.GetComponent<Image>();
    }

    public void ClickMusicButton()
    {
        if (_isPlayingMusic == false)
        {
            _isPlayingMusic = true;
            _audioSource.playOnAwake = true;
            _audioSource.Play();
            _imageButtons[0].sprite = _sprites[0];
        }
        else if (_isPlayingMusic)
        {
            _isPlayingMusic = false;
            _audioSource.playOnAwake = false;
            _audioSource.Stop();
            _imageButtons[0].sprite = _sprites[1];
        }
    }
}
