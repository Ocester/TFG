using UnityEngine;
using UnityEngine.UI;


public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource dialogSound;
    [SerializeField] private AudioSource pointSound;
    [SerializeField] private AudioSource digSound;
    [SerializeField] private AudioSource cutSound;
    [SerializeField] private AudioSource grabSound;
    [SerializeField] private AudioSource endLevelSound;
    [Header("Music Volume")]
    [SerializeField] private Slider soundLevelSlider;
    public static MusicController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        EventController.PointObjectSound += PlaySound;
        EventController.DialogSound += PlaySound;
        EventController.DigSound += PlaySound;
        EventController.GrabSound += PlaySound;
        EventController.CutSound += PlaySound;
        EventController.FinishLevelSound += PlaySound;
        gameMusic.volume = 0.5f;
        PlaySound(ActionSound.GameMusic);
        soundLevelSlider.value = gameMusic.volume;
        soundLevelSlider.onValueChanged.AddListener(ChangeMusicVolume);
    }

    private void OnDisable()
    {
        EventController.PointObjectSound -= PlaySound;
        EventController.DialogSound -= PlaySound;
        EventController.DigSound -= PlaySound;
        EventController.GrabSound -= PlaySound;
        EventController.CutSound -= PlaySound;
        EventController.FinishLevelSound -= PlaySound;
    }

    private void ChangeMusicVolume(float volume)
    {
        gameMusic.volume = volume;
    } 

    private void PlaySound(ActionSound actionSound)
    {
        switch (actionSound)
        {
            case ActionSound.GameMusic:
                gameMusic.Play();
                return;
            case ActionSound.PointSound:
                pointSound.Play();
                return;
            case ActionSound.GrabSound:
                grabSound.Play();
                return;
            case ActionSound.DialogSound:
                dialogSound.Play();
                return;
            case ActionSound.DigSound:
                digSound.Play();
                return;
            case ActionSound.CutSound:
                cutSound.Play();
                return;
            case ActionSound.EndLevelSound:
                gameMusic.Stop();
                endLevelSound.Play();
                return;
        }
    }
    
    public enum ActionSound
    {
        GameMusic,
        PointSound,
        GrabSound,
        DialogSound,
        DigSound,
        CutSound,
        EndLevelSound
    }
}
