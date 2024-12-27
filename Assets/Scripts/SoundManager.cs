using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioSource musicSource; // Component phát nhạc
    [SerializeField] AudioClip[] musicClips; // Mảng chứa các file nhạc
    
    private int currentMusicIndex = 0;

    void Start()
    {
        // Khởi tạo AudioSource nếu chưa có
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true; // Tự động lặp lại
            musicSource.playOnAwake = true; // Tự động phát khi game bắt đầu
        }

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        // Bắt đầu phát nhạc nếu có file nhạc
        if (musicClips != null && musicClips.Length > 0)
        {
            PlayMusic();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    // Phát nhạc
    public void PlayMusic()
    {
        if (musicClips != null && musicClips.Length > 0)
        {
            musicSource.clip = musicClips[currentMusicIndex];
            musicSource.Play();
        }
    }

    // Dừng nhạc
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Chuyển bài tiếp theo
    public void NextSong()
    {
        currentMusicIndex = (currentMusicIndex + 1) % musicClips.Length;
        PlayMusic();
    }

    // Chuyển về bài trước
    public void PreviousSong()
    {
        currentMusicIndex--;
        if (currentMusicIndex < 0)
            currentMusicIndex = musicClips.Length - 1;
        PlayMusic();
    }

    // Tạm dừng/tiếp tục phát nhạc
    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
        else
            musicSource.UnPause();
    }
}