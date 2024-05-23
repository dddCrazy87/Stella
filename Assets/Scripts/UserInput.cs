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

    [Header("touch")]
    [SerializeField] float swipeThreshold = 50f;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;


    void Update() {

        if (isMoving_camera) {
            camera_EditingMindMap.position = Vector3.MoveTowards(camera_EditingMindMap.position, cameraTargetPos, cameraMoveSpeed * Time.deltaTime);
            if (camera_EditingMindMap.position == cameraTargetPos) {
                camera_EditingMindMap.position = cameraTargetPos;
                isMoving_camera = false;
            }
        }


        // keyboard
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isMoving_camera) {
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving_camera) {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving_camera) {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving_camera) {
            MoveLeft();
        }

        //touch
        if (Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);

            switch (touch.phase) {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        endTouchPosition = touch.position;
                        Vector2 swipeDirection = endTouchPosition - startTouchPosition;

                        if (Mathf.Abs(swipeDirection.x) > swipeThreshold)
                        {
                            if (swipeDirection.x > 0) MoveRight();
                            else MoveLeft(); 
                            isSwiping = false;
                        }
                        else if (Mathf.Abs(swipeDirection.y) > swipeThreshold)
                        {
                            if (swipeDirection.y > 0) MoveUp();
                            else MoveDown();
                            isSwiping = false;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    isSwiping = false;
                    break;
            }
        }
    }

    void MoveRight() {
        Debug.Log("Moved Right");

        if (mindMapController.selectOther("previous")) {
            mindMapController.revolveSelectedNode(-1);
        }
    }

    void MoveLeft() {
        Debug.Log("Moved Left");

        if (mindMapController.selectOther("next")) {
            mindMapController.revolveSelectedNode(1);
        }
    }

    void MoveUp() {
        Debug.Log("Moved Up");

        if (mindMapController.selectOther("child")) {
            cameraTargetPos = camera_EditingMindMap.position + mindMapController.fixCameraPos(-1);
            cameraMoveSpeed = cameraMoveYSpeed;
            isMoving_camera = true;
        }
        
    }

    void MoveDown() {
        Debug.Log("Moved Down");

        if (mindMapController.selectOther("father")) {
            cameraTargetPos = camera_EditingMindMap.position + mindMapController.fixCameraPos(1);
            cameraMoveSpeed = cameraMoveYSpeed;
            isMoving_camera = true;
        }
    }

    public void fixCameraPosition() {
        camera_EditingMindMap.position = camera_EditingMindMap.position + new Vector3(0,0,1.5f);
        cameraMoveSpeed = cameraMoveZSpeed;
    }

    void Start() {
        Application.targetFrameRate = 30;
    }
}
