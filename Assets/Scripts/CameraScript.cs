using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float cameraShift = 1.5f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Vector3 curPos = transform.position;
            curPos.y -= cameraShift;
            transform.position = curPos;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Vector3 curPos = transform.position;
            curPos.y += cameraShift;
            transform.position = curPos;
        }
    }
}
