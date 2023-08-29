using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controls sound settings
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static float minVolume = -80;
    private static float maxVolume = 0;

    [System.Serializable]
    public class MixerState
    {
        public AudioMixerGroup mixer;
        public string SaveName => $"Volume{mixer.name}";

        public void Load()
        {
            var savedValue = PlayerPrefs.GetFloat(SaveName, 0.8f);
            var volume = Mathf.Lerp(minVolume, maxVolume, savedValue);
            mixer.audioMixer.SetFloat($"{mixer.name}Volume", volume);
        }

        public void Set(float value)
        {
            PlayerPrefs.SetFloat(SaveName, value);
            PlayerPrefs.Save();
            var volume = Mathf.Lerp(minVolume, maxVolume, value);
            mixer.audioMixer.SetFloat($"{mixer.name}Volume", volume);
        }
    }

    [SerializeField] private MixerState sound, music;

    public MixerState Sound => sound;
    public MixerState Music => music;

    private void Start()
    {
        sound.Load();
        music.Load();
    }

}
