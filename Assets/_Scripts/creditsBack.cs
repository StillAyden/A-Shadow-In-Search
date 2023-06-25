using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsBack : MonoBehaviour
{
    [SerializeField] Animator backAnim;
    [SerializeField] Canvas menuCanvas;
    MainMenu mainMenu;

    public void BackToMenu()//InputAction.CallbackContext context
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        backAnim.SetTrigger("BackToMenu");
        StartCoroutine(menuTimer());
    }

    IEnumerator menuTimer()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        menuCanvas.enabled = true;
        menuCanvas.gameObject.SetActive(true);
        this.gameObject.GetComponent<Canvas>().enabled = false;

    }
}
