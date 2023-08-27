using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{

    [SerializeField] private Slider sound, music;

    [Zenject.Inject] private AudioManager audioManager;

    private void Awake()
    {
        sound.onValueChanged.AddListener(audioManager.Sound.Set);
        music.onValueChanged.AddListener(audioManager.Music.Set);
    }

    private void OnEnable()
    {
        sound.value = PlayerPrefs.GetFloat(audioManager.Sound.SaveName, 1);
        music.value = PlayerPrefs.GetFloat(audioManager.Music.SaveName, 1);
    }
}
