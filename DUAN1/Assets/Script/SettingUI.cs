using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public GameObject settingPanel;
    public Slider musicSlider;

    public static bool IsOpen { get; private set; }

    private float oldTimeScale = 1f;

    private Collider[] gameColliders;
    private bool[] colliderStates;

    private void Start()
    {
        IsOpen = false;
        Time.timeScale = 1f;

        if (AudioManager.Instance != null)
        {
            musicSlider.value = AudioManager.Instance.GetMusicVolume();
        }

        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
    }

    public void OpenSetting()
    {
        oldTimeScale = Time.timeScale;

        IsOpen = true;
        Time.timeScale = 0f;

        DisableGameColliders();

        settingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);

        RestoreGameColliders();

        Time.timeScale = oldTimeScale;
        IsOpen = false;
    }

    private void DisableGameColliders()
    {
        gameColliders = FindObjectsByType<Collider>();

        colliderStates = new bool[gameColliders.Length];

        for (int i = 0; i < gameColliders.Length; i++)
        {
            if (gameColliders[i] != null)
            {
                colliderStates[i] = gameColliders[i].enabled;
                gameColliders[i].enabled = false;
            }
        }
    }

    private void RestoreGameColliders()
    {
        if (gameColliders == null) return;

        for (int i = 0; i < gameColliders.Length; i++)
        {
            if (gameColliders[i] != null)
            {
                gameColliders[i].enabled = colliderStates[i];
            }
        }
    }

    private void ChangeMusicVolume(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }

    private void OnDisable()
    {
        if (IsOpen)
        {
            RestoreGameColliders();
        }

        Time.timeScale = 1f;
        IsOpen = false;
    }
}