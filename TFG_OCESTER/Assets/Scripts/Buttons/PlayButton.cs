using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Sprite btnImg;
    [SerializeField]private GameObject loaderScreen;
    
    private void Start()
    {
      gameObject.GetComponent<Button>().onClick.AddListener(PlayGame);
      //loaderController = GameObject.FindObjectOfType<LoaderController>();
    }

    private void PlayGame()
    {
        gameObject.GetComponent<Image>().sprite = btnImg;
        loaderScreen.GetComponent<LoaderController>().LoadNextLevel();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
