using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameInputs _inputs;

    [SerializeField] Canvas hudCanvas;
    [SerializeField] Canvas pauseCanvas;

    [SerializeField] bool isPaused = false;

    private void Awake()
    {
        _inputs = new GameInputs();

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

        Time.timeScale = 0;
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

        Time.timeScale = 1;
    }
}
