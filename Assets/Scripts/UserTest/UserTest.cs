using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTest : MonoBehaviour
{
    [SerializeField] private MindMapProjs mindMapProjs;
    [SerializeField] private ProjsUIManager projsUIManager;
    private void Start() {
        mindMapProjs.mindMapProjs = new List<MindMapNode> { 
            new("早餐", "寫下一個主題吧！", 0),
            new("午餐", "寫下一個主題吧！寫下一個主題吧！", 0),
            new("晚餐", "寫下一個主題吧！寫下一個主題吧！寫下一個主題吧！", 0)
        };
        projsUIManager.setProjsUI();
    }

    //(string nodeText, string nodeQusetion, int nodeLevel, List<MindMapNode> childrenNode)
    //(string nodeText, string nodeQusetion, int nodeLevel)
}
