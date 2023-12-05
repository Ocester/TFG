using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource dialogSound;
    [SerializeField] private AudioSource pointSound;
    [SerializeField] private AudioSource digSound;
    [SerializeField] private AudioSource cutSound;
    [SerializeField] private AudioSource grabSound;
    
    private static MusicController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        EventController.pointObjectSound += PlayMusic;
        EventController.dialogSound += PlayMusic;
        EventController.digSound += PlayMusic;
        EventController.grabSound += PlayMusic;
        EventController.cutSound += PlayMusic;
        PlayMusic(ActionSound.gameMusic);
    }

    private void OnDisable()
    {
        EventController.pointObjectSound -= PlayMusic;
        EventController.dialogSound -= PlayMusic;
        EventController.digSound -= PlayMusic;
        EventController.grabSound -= PlayMusic;
        EventController.cutSound -= PlayMusic;
    }

    private void PlayMusic(ActionSound actionSound)
    {
        switch (actionSound)
        {
            case ActionSound.gameMusic:
                gameMusic.Play();
                return;
            case ActionSound.pointSound:
                pointSound.Play();
                return;
            case ActionSound.grabSound:
                grabSound.Play();
                return;
            case ActionSound.dialogSound:
                dialogSound.Play();
                return;
            case ActionSound.digSound:
                digSound.Play();
                return;
            case ActionSound.cutSound:
                cutSound.Play();
                return;
        }
    }
    
    public enum ActionSound
    {
        gameMusic,
        pointSound,
        grabSound,
        dialogSound,
        digSound,
        cutSound
        
    }
}
