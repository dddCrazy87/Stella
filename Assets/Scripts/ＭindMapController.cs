using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ＭindMapController : MonoBehaviour
{
    public MindMapProjs mindMapProjs;
    private MindMapNode rootNode;
    public GameObject selectedNode;
    [SerializeField] private Transform nodePrefab;
 
    void Start() {
        rootNode = mindMapProjs.getCurrentProj();
        generateMindMap(rootNode, transform);
    }
    
    private void generateMindMap(MindMapNode node, Transform father) {
        
        Transform go = Instantiate(nodePrefab, father);

        go.GetComponent<MindMapNodeScript>().mindMapNode = node;
        generateMindMapRec(node, go.transform);
    }
    
    private void generateMindMapRec(MindMapNode node, Transform father) {

        if (node.children.Length <= 0) {
            return;
        }
        
        List<Transform> childrenGo = new();

        for (int i = 0; i < node.children.Length; i ++) {
            Transform childGo = Instantiate(nodePrefab, father);
            childGo.GetComponent<MindMapNodeScript>().mindMapNode = node.children[i];
            childrenGo.Add(childGo);
        }
        
        father.GetComponent<MindMapNodeScript>().rearrange();
        
        for (int i = 0; i < node.children.Length; i ++) {
            generateMindMapRec(node.children[i], childrenGo[i]);
        }
    }

    public void addNode() {

        if (selectedNode == null) {
            Debug.Log("null");
            return;
        }
        Debug.Log(selectedNode.name);
        Instantiate(nodePrefab, selectedNode.transform);
        selectedNode.GetComponent<MindMapNodeScript>().rearrange();
    }
}
