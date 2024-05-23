using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditBtns : MonoBehaviour
{
    [SerializeField] private MindMapProjs mindMapProjs;
    [SerializeField] private MindMapController mindMapController;

    // save btn
    public void OnClickSaveBtn() {
        mindMapProjs.saveProj();
        mindMapController.resetRootNode();
    }
}
