using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    private void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
