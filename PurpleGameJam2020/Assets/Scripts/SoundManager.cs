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
    public AudioSource playerMoveSource;
    public AudioSource catMoveSource;
    public AudioSource musicSource;
    public AudioClip playerMoveSound;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    public AudioClip[] catClips;
    public AudioClip repairSound;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        playerMoveSource.clip = clip;
        playerMoveSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        playerMoveSource.pitch = randomPitch;
        playerMoveSource.clip = clips[randomIndex];
        playerMoveSource.Play();
    }

    public void PlayVectorSound(Vector2 vector2)
    {
        if (vector2.x != 0 || vector2.y != 0)
        {
            playerMoveSource.clip = playerMoveSound;
            playerMoveSource.Play();
            playerMoveSource.loop = true;
        }
        else
        {
            playerMoveSource.loop = false;
            playerMoveSource.Stop();
        }
    }

    public void PlayRandomCatNoise()
    {
        int randomIndex = Random.Range(0, catClips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        catMoveSource.pitch = randomPitch;
        catMoveSource.clip = catClips[randomIndex];
        catMoveSource.Play();
    }

    public void Repair(float value)
    {
        if (value == 1)
        {
            playerMoveSource.clip = repairSound;
            playerMoveSource.Play();
            playerMoveSource.loop = true;
        }
        else
        {
            playerMoveSource.loop = false;
            playerMoveSource.Stop();
        }
    }
}
