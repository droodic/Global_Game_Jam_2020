using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager _instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();
            }
            return _instance;
        }
    }
    #endregion
    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioClip playerMoveSound;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    public void PlayVectorSound(Vector2 vector2)
    {
        if (vector2.x != 0 || vector2.y != 0)
        {
            efxSource.clip = playerMoveSound;
            efxSource.Play();
            efxSource.loop = true;
        }
        else
        {
            efxSource.loop = false;
            efxSource.Stop();
        }
    }
}
