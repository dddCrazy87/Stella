using UnityEngine;
using UnityEngine.EventSystems;

public class DragCnavasObj2 : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform objRect;
    [SerializeField] private RectTransform minXobj;
    [SerializeField] private RectTransform maxXobj;
    public float minX = -300f;
    public float maxX = 300f;
    [SerializeField] private RectTransform minYobj;
    [SerializeField] private RectTransform maxYobj;
    public float minY = -300f;
    public float maxY = 300f;

    private Vector2 startPos;
    private Vector2 startObjPos;

    void Start()
    {
        startPos = Vector2.zero;
        startObjPos = objRect.anchoredPosition;
        if (minXobj != null) minX = minXobj.anchoredPosition.x;
        if (maxXobj != null) maxX = maxXobj.anchoredPosition.x;
        if (minYobj != null) minY = minYobj.anchoredPosition.x;
        if (maxYobj != null) maxY = maxYobj.anchoredPosition.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            float newX = Mathf.Clamp(localPointerPosition.x - startPos.x + startObjPos.x, minX, maxX);
            float newY = Mathf.Clamp(localPointerPosition.y - startPos.y + startObjPos.y, minY, maxY);
            objRect.anchoredPosition = new Vector2(newX, newY);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        startObjPos = objRect.anchoredPosition;
    }

}
