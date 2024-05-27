using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchCharector : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform headIcon;
    [SerializeField] private float startPosX = 25f;
    [SerializeField] private float endPosX = 1000f;
    [SerializeField] private float iconSpaceBetween = 975f;
    [SerializeField] private float animSpeed = 100f;

    private void Start() {
        endPosX = (iconSpaceBetween * (transform.childCount - 1) + startPosX) * -1;
    }

    private bool isMoved = false, isMoving = false, isEdgeMoving = false, isEdgeMoving2 = false;
    private Vector2 targetPos = new();
    private Vector2 edgeAnimStartPos = new();

    public void OnDrag(PointerEventData eventData) {
        Vector2 localPointerPosition;
        if (!isMoved && !isMoving && !isEdgeMoving && !isEdgeMoving2 && RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out localPointerPosition)) {
            if (eventData.delta.x > 0f) {
                if (headIcon.anchoredPosition.x >= startPosX) {
                    animSpeed = Math.Abs(animSpeed);
                    targetPos = new Vector2(startPosX + iconSpaceBetween/2, 0);
                    edgeAnimStartPos = headIcon.anchoredPosition;
                    isEdgeMoving = true;
                }
                else {
                    targetPos = headIcon.anchoredPosition + new Vector2(iconSpaceBetween, 0);
                    animSpeed = Math.Abs(animSpeed);
                    isMoved = true;
                    isMoving = true;
                }
            }
            if (eventData.delta.x < 0f) {
                if (headIcon.anchoredPosition.x <= endPosX) {
                    animSpeed = Math.Abs(animSpeed) * -1;
                    targetPos = new Vector2(endPosX - iconSpaceBetween/2, 0);
                    edgeAnimStartPos = headIcon.anchoredPosition;
                    isEdgeMoving = true;
                }
                else {
                    targetPos = headIcon.anchoredPosition - new Vector2(iconSpaceBetween, 0);
                    animSpeed = Math.Abs(animSpeed) * -1;
                    isMoved = true;
                    isMoving = true;
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData){
        isMoved =false;
    }

    private void Update() {
        if (isMoving) {
            if (Vector2.Distance(targetPos, headIcon.anchoredPosition) > Math.Abs(animSpeed * Time.deltaTime)) {
                headIcon.anchoredPosition += new Vector2(animSpeed * Time.deltaTime, 0);
            }
            else {
                headIcon.anchoredPosition = targetPos;
                isMoving = false;
            }
        }

        if (isEdgeMoving) {
            if (Vector2.Distance(targetPos, headIcon.anchoredPosition) > Math.Abs(animSpeed * Time.deltaTime)) {
                headIcon.anchoredPosition += new Vector2(animSpeed * Time.deltaTime, 0);
            }
            else {
                headIcon.anchoredPosition = targetPos;
                isEdgeMoving = false;
                isEdgeMoving2 = true;
            }
        }

        if (isEdgeMoving2) {
            if (Vector2.Distance(edgeAnimStartPos, headIcon.anchoredPosition) > Math.Abs(animSpeed * Time.deltaTime)) {
                headIcon.anchoredPosition -= new Vector2(animSpeed * Time.deltaTime, 0);
            }
            else {
                headIcon.anchoredPosition = edgeAnimStartPos;
                isEdgeMoving2 = false;
            }
        }
    }
}
