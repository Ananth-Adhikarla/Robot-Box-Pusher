using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] myClips = null;
    int trackNumber = 0;

    AudioSource myAudioSource;
    float volume = 0.4f;

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.volume = volume;
        MusicChanger();
    }

    void Update()
    {
        myAudioSource.volume = PlayerPrefs.GetFloat("Volume");
    }

  
    public void MusicChanger()
    {
        ConditionCheck();

        AudioClip myClip = SelectMusic();
        //trackNumber++;
        myAudioSource.clip = myClip;
        myAudioSource.Play();
        //myAudioSource.PlayOneShot(myClip,volume);

    }

    private void ConditionCheck()
    {
        if (myAudioSource.isPlaying == true)
        {
            myAudioSource.Stop();
        }
        if (trackNumber + 1 > myClips.Length)
        {
            trackNumber = 0;
        }
    }
    // randomize music
    private AudioClip SelectMusic()
    {
        //return myClips[trackNumber];
        return myClips[UnityEngine.Random.Range(0,myClips.Length)];
    }



}
