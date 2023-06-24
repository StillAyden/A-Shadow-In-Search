using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Image cursor;
    [SerializeField] Canvas pauseCanvas;

    int selectedOption = 1;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    public void highlightResume()
    {
        selectedOption = 1;
        cursor.transform.localPosition = new Vector3(0,35,0);
    }
    public void clickResume()
    {
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void highlightSettings()
    {
        selectedOption = 2;
        cursor.transform.localPosition = new Vector3(0, -25, 0);
    }
    public void clickSettings()
    {

    }

    public void highlightExit()
    {
        selectedOption = 3;
        cursor.transform.localPosition = new Vector3(0, -85, 0);
    }

    public void clickExit()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
