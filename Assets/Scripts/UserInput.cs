using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private Transform camera_EditingMindMap;
    [SerializeField] private MindMapController mindMapController;
    [SerializeField] private float cameraMoveYSpeed = 0.2f;
    [SerializeField] private float cameraMoveZSpeed = 0.2f;
    private float cameraMoveSpeed = 0f;
    private bool isMoving_camera = false;
    private Vector3 cameraTargetPos = new();

    void Update() {

        if (isMoving_camera) {
            camera_EditingMindMap.position = Vector3.MoveTowards(camera_EditingMindMap.position, cameraTargetPos, cameraMoveSpeed * Time.deltaTime);
            if (camera_EditingMindMap.position == cameraTargetPos) {
                camera_EditingMindMap.position = cameraTargetPos;
                isMoving_camera = false;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isMoving_camera) {
            if (mindMapController.selectOther("father")) {
                cameraTargetPos = camera_EditingMindMap.position + mindMapController.fixCameraPos(1);
                cameraMoveSpeed = cameraMoveYSpeed;
                isMoving_camera = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving_camera) {
            if (mindMapController.selectOther("child")) {
                cameraTargetPos = camera_EditingMindMap.position + mindMapController.fixCameraPos(-1);
                cameraMoveSpeed = cameraMoveYSpeed;
                isMoving_camera = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving_camera) {
            if (mindMapController.selectOther("previous")) {
                mindMapController.revolveSelectedNode(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving_camera) {
            if (mindMapController.selectOther("next")) {
                mindMapController.revolveSelectedNode(1);
            }
        }
    }

    public void fixCameraPosition() {
        camera_EditingMindMap.position = camera_EditingMindMap.position + new Vector3(0,0,3);
        //cameraTargetPos = camera_EditingMindMap.position + new Vector3(0,0,3);
        cameraMoveSpeed = cameraMoveZSpeed;
        //isMoving_camera = true;
    }
}
