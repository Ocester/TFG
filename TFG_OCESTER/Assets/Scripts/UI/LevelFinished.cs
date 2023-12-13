using UnityEngine;

public class LevelFinished : MonoBehaviour
{
    private void Start()
    {
        EventController.OnFinishLevel += ShowInformation;
    }
    private void OnDisable()
    {
        EventController.OnFinishLevel -= ShowInformation;
    }

    private void ShowInformation()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
        }

        Time.timeScale = 0;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
