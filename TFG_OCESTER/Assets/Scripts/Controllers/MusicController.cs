using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource dialogSound;
    [SerializeField] private AudioSource pointSound;
    [SerializeField] private AudioSource digSound;
    [SerializeField] private AudioSource cutSound;
    [SerializeField] private AudioSource grabSound;
    [SerializeField] private AudioSource endLevelSound;
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
        EventController.PointObjectSound += PlayMusic;
        EventController.DialogSound += PlayMusic;
        EventController.DigSound += PlayMusic;
        EventController.GrabSound += PlayMusic;
        EventController.CutSound += PlayMusic;
        EventController.FinishLevelSound += PlayMusic;
        PlayMusic(ActionSound.GameMusic);
    }

    private void OnDisable()
    {
        EventController.PointObjectSound -= PlayMusic;
        EventController.DialogSound -= PlayMusic;
        EventController.DigSound -= PlayMusic;
        EventController.GrabSound -= PlayMusic;
        EventController.CutSound -= PlayMusic;
        EventController.FinishLevelSound -= PlayMusic;
    }

    private void PlayMusic(ActionSound actionSound)
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
