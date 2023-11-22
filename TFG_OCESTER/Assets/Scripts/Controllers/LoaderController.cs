using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderController : MonoBehaviour
{
    [SerializeField] private float loadTime;
    public static LoaderController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadNextLevel()
    {
        Debug.Log("Play next level");
        // verifico que no hemos sobrepasado el número de escenas.
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings-1)
        {
            Debug.Log("no hay más niveles");
            return;
        }
        
        gameObject.SetActive(true);
        StartCoroutine(FadeOutLoadLevel());
    }
    private IEnumerator FadeOutLoadLevel()
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        StartCoroutine(FadeInLoadLevel());
    }
    
    private IEnumerator FadeInLoadLevel()
    {
        gameObject.GetComponent<Animator>().SetTrigger("End");
        yield return new WaitForSeconds(loadTime);
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
