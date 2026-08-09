using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public GameObject settingPanel;
    public Slider musicSlider;

    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            musicSlider.value = AudioManager.Instance.GetMusicVolume();
        }

        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);

        // Mặc định đóng Setting
        settingPanel.SetActive(false);
    }

    public void OpenSetting()
    {
        settingPanel.SetActive(true);

        // Dừng thời gian game
        Time.timeScale = 0f;
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);

        // Chạy lại thời gian game
        Time.timeScale = 1f;
    }

    private void ChangeMusicVolume(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }

    private void OnDestroy()
    {
        // Tránh game bị kẹt timeScale = 0
        Time.timeScale = 1f;
    }
}