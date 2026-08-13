using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [Header("Setting UI")]
    public GameObject settingPanel;
    public Slider musicSlider;

    private void Start()
    {
        // Đảm bảo Setting đóng khi bắt đầu
        settingPanel.SetActive(false);

        // Lấy volume đã lưu
        if (AudioManager.Instance != null)
        {
            float savedVolume = AudioManager.Instance.GetMusicVolume();

            // Gán giá trị cho Slider
            musicSlider.SetValueWithoutNotify(savedVolume);

            // Đảm bảo AudioSource cũng đúng volume
            AudioManager.Instance.SetMusicVolume(savedVolume);
        }

        // Đăng ký sự kiện Slider
        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
    }

    public void OpenSetting()
    {
        settingPanel.SetActive(true);

        // Cập nhật Slider đúng với volume hiện tại
        if (AudioManager.Instance != null)
        {
            float currentVolume = AudioManager.Instance.GetMusicVolume();

            musicSlider.SetValueWithoutNotify(currentVolume);
        }

        // Dừng game
        Time.timeScale = 0f;
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);

        // Chạy lại game
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
        // Đảm bảo game không bị kẹt khi SettingUI bị destroy
        Time.timeScale = 1f;

        // Hủy listener
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
        }
    }
}