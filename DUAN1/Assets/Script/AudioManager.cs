using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource musicSource;

    private void Awake()
    {
        // Nếu đã có AudioManager thì không tạo thêm
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Giữ AudioManager khi chuyển Scene
        DontDestroyOnLoad(gameObject);

        // Lấy volume đã lưu
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        // Áp dụng ngay cho AudioSource
        if (musicSource != null)
        {
            musicSource.volume = savedVolume;
        }
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);

        if (musicSource != null)
        {
            musicSource.volume = volume;
        }

        // Lưu lại volume
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 1f);
    }
}