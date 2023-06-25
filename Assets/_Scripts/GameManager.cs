using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameInputs _inputs;
    PlayerManager player;
    Camera cam;
    
    [SerializeField] Canvas hudCanvas;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas deathCanvas;
    [SerializeField] Canvas endGame;

    [SerializeField] Animator fadeDark;

    [SerializeField] bool isPaused = false;

    [SerializeField] GameObject directionArrow;

    [Header("Scenes")]
    [SerializeField] bool isHospitalScene;
    [SerializeField] bool isMainScene;
    [SerializeField] bool isHouseLower;
    [SerializeField] bool isHouseUpper;

    private void Awake()
    {
        cam = Camera.main;
        _inputs = new GameInputs();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

        if (!isPaused)
        {
            _inputs.Player.Enable();
            _inputs.Player.Pause.performed += x => PauseGame();
        }
        else
        {
            _inputs.Game.Enable();
            _inputs.Game.UnpauseAndBack.performed += x => UnpauseGame();

        }

        if(isMainScene)
        {
            StartCoroutine(gameManagerMain());
        }
        else if (isHouseLower)
        {
            gameManagerHouseLower();
        }
        else if (isHouseUpper)
        {
            gameMangerHouseUpper();
        }
        else if (isHospitalScene)
        {
            StartCoroutine(gameManagerHospital());
        }

    }

    void PauseGame()
    {
        isPaused = true;
        _inputs.Player.Pause.performed -= x => PauseGame();
        _inputs.Player.Disable();
        _inputs.Game.Enable();
        _inputs.Game.UnpauseAndBack.performed += x => UnpauseGame();

        hudCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);
    }

    void UnpauseGame()
    {
        isPaused = false;
        _inputs.Game.UnpauseAndBack.performed -= x => UnpauseGame();
        _inputs.Game.Disable();
        _inputs.Player.Enable();
        _inputs.Player.Pause.performed += x => PauseGame();

        hudCanvas.gameObject.SetActive(true);
        pauseCanvas.gameObject.SetActive(false);
    }

    public IEnumerator GameOver()
    {
        _inputs.Disable();
        _inputs.Game.AnyKey.Enable();
        deathCanvas.gameObject.SetActive(true);
        hudCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        if (_inputs.Game.AnyKey.IsPressed())
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator gameManagerMain()
    {
        yield return new WaitForSecondsRealtime(21f);
        cam.GetComponent<Animator>().enabled = false;
        player.enabled = true;
        cam.GetComponent<CameraFollow>().enabled = true;

        float localRotation = 0;
        while (localRotation != 180)
        {
            localRotation += 1.5f;
            cam.transform.localEulerAngles = new Vector3(90f, localRotation, cam.transform.localRotation.z);
            yield return new WaitForFixedUpdate();
        }

        hudCanvas.gameObject.SetActive(true);

    }

    void gameManagerHouseLower()
    {
        directionArrow.SetActive(false);
    }

    void gameMangerHouseUpper()
    {
        directionArrow.SetActive(false);
    }

    IEnumerator gameManagerHospital()
    {
        yield return new WaitForSecondsRealtime(21f);
        SceneManager.LoadScene("Main");
    }

    public void End()
    {
        StartCoroutine(EndTimer());
    }

    IEnumerator EndTimer()
    {
        player.enabled = false;
        fadeDark.enabled = true;
        hudCanvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        endGame.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("MainMenu");
    }
}
