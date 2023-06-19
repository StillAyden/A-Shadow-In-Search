using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject target;

    Vector3 camFollowVelocity = Vector3.zero;
    [SerializeField] float camFollowSpeed = 0.5f;
    [SerializeField] float camOffset;
    [SerializeField] float camHeight;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(this.transform.position,
                                                        new Vector3(target.transform.position.x + camOffset, target.transform.position.y + camHeight, target.transform.position.z),
                                                            ref camFollowVelocity, camFollowSpeed);
        transform.position = targetPosition;
    }
}
