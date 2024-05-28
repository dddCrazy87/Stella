using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjsUIManager : MonoBehaviour
{
    [SerializeField] private MindMapProjs mindMapProjs;
    [SerializeField] private MindMapController mindMapController;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private RectTransform projUIPrefab;
    
    [SerializeField] private float projUIBetweenSpace = 180f;
    [SerializeField] private Vector2 curPos = new(-163f, 47f);
    [SerializeField] private DragCanvasObject dragCanvasObject;

    public void setProjsUI() {
        for (int i = 0; i < mindMapProjs.mindMapProjs.Count; i ++) {
            addProjUI(i);
        }
    }

    public void addNewProj() {
        addProjUI(mindMapProjs.curProjIndex);
    }

    public void updateProjUI() {
        transform.GetChild(mindMapProjs.curProjIndex).GetComponent<ProjScript>().setProjsNameUI(mindMapProjs.mindMapProjs[mindMapProjs.curProjIndex].text);
    }

    private void addProjUI(int id) {
        RectTransform tr = Instantiate(projUIPrefab, transform);
        tr.anchoredPosition += curPos;
        curPos.x += projUIBetweenSpace;
        tr.GetComponent<ProjScript>().projIndex = id;
        tr.GetComponent<ProjScript>().setProjsNameUI(mindMapProjs.mindMapProjs[id].text);

        if (transform.childCount > 3) {
            dragCanvasObject.AddSpace(projUIBetweenSpace);
        }
    }

    public void selectProj(int id) {
        mindMapController.resetRootNode(mindMapProjs.mindMapProjs[id]);
        mindMapProjs.curProjIndex = id;
        uIManager.userInfoToggle();
    }
}
