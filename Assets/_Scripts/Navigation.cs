using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    GameObject player;

    [SerializeField] List<GameObject> Landmarks = new List<GameObject>();
    [SerializeField] GameObject currentLandmark;

    [SerializeField] GameObject pointer;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        currentLandmark = Landmarks[0];
    }

    private void Update()
    {
        if (Vector3.Distance(currentLandmark.transform.position, player.transform.position) < 4f)
        {
            if(currentLandmark != Landmarks[Landmarks.Count - 1])
            {
                for (int k = 0; k < Landmarks.Count - 1; k++)
                {
                    if (currentLandmark == Landmarks[k])
                    {
                        currentLandmark = Landmarks[k + 1];
                        break;
                    }
                }
            }
        }

        pointer.transform.LookAt(currentLandmark.transform);
        pointer.transform.localEulerAngles = new Vector3(90, pointer.transform.localEulerAngles.y, 0);
    }
}
