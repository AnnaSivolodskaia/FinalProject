using UnityEngine.Audio;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        }
    }

    public void TriggerSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    
    public void Play(string name, float fadeDuration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            s.source.Play();
            StartCoroutine(FadeIn(s, fadeDuration));
        }
    }

    private IEnumerator FadeIn(Sound sound, float fadeDuration)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            sound.source.volume = Mathf.Lerp(0f, sound.volume, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        sound.source.volume = sound.volume;
    }

    public void StopMusic(string name, float fadeDuration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            StartCoroutine(FadeOut(s, fadeDuration));
        }
    }

    private IEnumerator FadeOut(Sound sound, float fadeDuration)
    {
        float startVolume = sound.source.volume;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            sound.source.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        sound.source.Stop();
        sound.source.volume = startVolume;
    }

}
