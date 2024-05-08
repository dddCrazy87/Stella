using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private GameObject camera_EditingMindMap;
    [SerializeField] private MindMapController mindMapController;
    [SerializeField] private float cameraMoveSpeed = 0.2f;
    private bool isMoving_camera = false;
    private Vector3 cameraTargetPos = new();

    void Update() {

        if (isMoving_camera) {
            camera_EditingMindMap.transform.position = Vector3.MoveTowards(camera_EditingMindMap.transform.position, cameraTargetPos, cameraMoveSpeed * Time.deltaTime);
            if (camera_EditingMindMap.transform.position == cameraTargetPos) {
                camera_EditingMindMap.transform.position = cameraTargetPos;
                isMoving_camera = false;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isMoving_camera) {
            if (mindMapController.selectOther("father")) {
                cameraTargetPos = camera_EditingMindMap.transform.position + mindMapController.fixCameraPos(1);
                isMoving_camera = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving_camera) {
            if (mindMapController.selectOther("child")) {
                cameraTargetPos = camera_EditingMindMap.transform.position + mindMapController.fixCameraPos(-1);
                isMoving_camera = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving_camera) {
            if (mindMapController.selectOther("previous")) {
                mindMapController.revolveSelectedNode(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving_camera) {
            if (mindMapController.selectOther("next")) {
                mindMapController.revolveSelectedNode(-1);
            }
        }
    }
}
