using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SettingPopUp : MonoBehaviour
{
    [SerializeField] private GameObject _musicOnBtn;
    [SerializeField] private GameObject _musicOffBtn;

    [SerializeField] private GameObject _sfxOnBtn;
    [SerializeField] private GameObject _sfxOffBtn;

    [SerializeField] private GameObject board;
    private bool isConfirming = false; // Flag to track if the button was already clicked
    private void Awake()
    {
        LoadButtonStates();
    }
    public void Confirm()
    {
        // Check if the button has already been clicked
        if (isConfirming) return;

        // Set the flag to true to prevent further clicks
        isConfirming = true;

        // Play the sound
        AudioController.Instance.PlaySound(AudioController.Instance.closePopup);

        // Resume time in the game
        Time.timeScale = 1f;

        // Animate the panel scaling down
        board.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            // Deactivate the board and the game object after the animation completes
            board.SetActive(false);
            gameObject.SetActive(false);

            // Reset the flag once the operation is complete
            isConfirming = false;
        });
    }

    public void Exit()
    {
        Application.Quit(); 
    }

    public void ToggleMusicOn()
    {
        _musicOnBtn.SetActive(false);
        _musicOffBtn.SetActive(true);
        AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);

        AudioController.Instance.ToggleMusic();
        SaveButtonStates();
    }

    public void ToggleMusicOff()
    {
        _musicOnBtn.SetActive(true);
        _musicOffBtn.SetActive(false);
        AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);

        AudioController.Instance.ToggleMusic();
        SaveButtonStates();
    }

    public void ToggleSfxOn()
    {
        _sfxOnBtn.SetActive(false);
        _sfxOffBtn.SetActive(true);
        AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);

        AudioController.Instance.ToggleSFX();
        SaveButtonStates();
    }

    public void ToggleSfxOff()
    {
        _sfxOnBtn.SetActive(true);
        _sfxOffBtn.SetActive(false);
        AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);

        AudioController.Instance.ToggleSFX();
        SaveButtonStates();
    }

    

    private void SaveButtonStates()
    {
        PlayerPrefs.SetInt("MusicOnBtnActive", _musicOnBtn.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("MusicOffBtnActive", _musicOffBtn.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("SfxOnBtnActive", _sfxOnBtn.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("SfxOffBtnActive", _sfxOffBtn.activeSelf ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadButtonStates()
    {
        _musicOnBtn.SetActive(PlayerPrefs.GetInt("MusicOnBtnActive", 1) == 1);
        _musicOffBtn.SetActive(PlayerPrefs.GetInt("MusicOffBtnActive", 0) == 1);
        _sfxOnBtn.SetActive(PlayerPrefs.GetInt("SfxOnBtnActive", 1) == 1);
        _sfxOffBtn.SetActive(PlayerPrefs.GetInt("SfxOffBtnActive", 0) == 1);
    }
}
