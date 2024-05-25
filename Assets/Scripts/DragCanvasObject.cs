using UnityEngine;
using UnityEngine.EventSystems;

public class DragCanvasObject : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform objRect;
    [SerializeField] private RectTransform minXobj;
    [SerializeField] private RectTransform maxXobj;
    public float minX = -300f;
    public float maxX = 300f;

    private Vector2 startPos;
    private Vector2 startObjPos;

    void Start()
    {
        startPos = Vector2.zero;
        startObjPos = objRect.anchoredPosition;
        if (minXobj != null) minX = minXobj.anchoredPosition.x;
        if (maxXobj != null) maxX = maxXobj.anchoredPosition.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            float newX = Mathf.Clamp(localPointerPosition.x - startPos.x + startObjPos.x, minX, maxX);
            objRect.anchoredPosition = new Vector2(newX, startObjPos.y);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        startObjPos = objRect.anchoredPosition;
    }

    public void AddSpace(float space) {
        minX -= space;
    }
}
