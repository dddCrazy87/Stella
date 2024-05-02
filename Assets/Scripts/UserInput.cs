using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private GameObject camera_EditingMindMap;
    [SerializeField] private ＭindMapController mindMapController;
    [SerializeField] private float cameraShift = 1.5f;
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Vector3 curPos = camera_EditingMindMap.transform.position;
            curPos.y -= cameraShift;
            camera_EditingMindMap.transform.position = curPos;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Vector3 curPos = camera_EditingMindMap.transform.position;
            curPos.y += cameraShift;
            camera_EditingMindMap.transform.position = curPos;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            mindMapController.revolveSelectedNode(1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            mindMapController.revolveSelectedNode(-1);
        }
    }
}
