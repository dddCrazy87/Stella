using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindMapController : MonoBehaviour
{
    public MindMapProjs mindMapProjs;
    private MindMapNode rootNode;
    public GameObject selectedNode;
    [SerializeField] private Transform nodePrefab;
    [SerializeField] private Material[] nodeMaterials;
 
    void Start() {
        rootNode = mindMapProjs.getCurrentProj();
        generateMindMap(rootNode, transform);
        Time.timeScale = 0.3f;
    }
    
    private void generateMindMap(MindMapNode node, Transform father) {
        
        Transform go = Instantiate(nodePrefab, father);
        go.GetChild(0).GetComponent<MeshRenderer>().material = nodeMaterials[0];
        go.GetComponent<MindMapNodeScript>().node = rootNode;
        generateMindMapRec(node, go);
    }
    
    private void generateMindMapRec(MindMapNode node, Transform father) {

        if (node.children.Count <= 0) {
            return;
        }
        
        List<Transform> childrenGo = new();

        for (int i = 0; i < node.children.Count; i ++) {
            Transform childGo = Instantiate(nodePrefab, father);
            childGo.GetChild(0).GetComponent<MeshRenderer>().material = nodeMaterials[node.children[i].level % nodeMaterials.Length];
            childGo.GetComponent<MindMapNodeScript>().node = node.children[i];
            childrenGo.Add(childGo);
        }
        
        father.GetComponent<MindMapNodeScript>().rearrange();
        
        for (int i = 0; i < node.children.Count; i ++) {
            generateMindMapRec(node.children[i], childrenGo[i]);
        }
    }

    public void addNode() {

        if (selectedNode == null) {
            Debug.Log("null");
            return;
        }
        selectedNode.transform.rotation = Quaternion.identity;
        Transform go = Instantiate(nodePrefab, selectedNode.transform);
        int level = selectedNode.GetComponent<MindMapNodeScript>().node.level + 1;
        MindMapNode newNode = new("new node", level);
        selectedNode.GetComponent<MindMapNodeScript>().node.children.Add(newNode);
        go.GetComponent<MindMapNodeScript>().node = newNode;
        go.GetChild(0).GetComponent<MeshRenderer>().material = nodeMaterials[level % nodeMaterials.Length];
        selectedNode.GetComponent<MindMapNodeScript>().rearrange();

        debug(mindMapProjs.getCurrentProj());
    }

    private void debug(MindMapNode node) {
        print("text: " + node.text + " level: " + node.level + " children.Count: " + node.children.Count);
        if (node.children.Count <= 0) return;
        for (int i = 0; i < node.children.Count; i ++ ) {
            debug(node.children[i]);
        }
    }

    public void revolveSelectedNode(int dir) {
        if (selectedNode == null) {
            return;
        }
        selectedNode.GetComponent<MindMapNodeScript>().revolve(dir);
    }
}
