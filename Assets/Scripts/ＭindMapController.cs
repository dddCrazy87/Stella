using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MindMapController : MonoBehaviour
{
    public MindMapProjs mindMapProjs;
    private MindMapNode rootNodeData;
    private Transform rootNodeTransform;
    public Transform selectedNode;
    [SerializeField] private Transform nodePrefab;
    [SerializeField] private Material[] nodeMaterials;
    [SerializeField] private float nodeBetween = 1.5f;
    [SerializeField] private int nodeOtherChildren = 0;
    [SerializeField] private TextMeshProUGUI question, answer;
 
    void Start() {
        rootNodeData = mindMapProjs.getCurrentProj();
        generateMindMap(rootNodeData, transform);
    }
    
    private void generateMindMap(MindMapNode node, Transform father) {
        
        Transform go = Instantiate(nodePrefab, father);
        go.GetChild(0).GetComponent<MeshRenderer>().material = nodeMaterials[0];
        go.GetComponent<MindMapNodeScript>().setData(node, "寫下一個主題吧");
        selectTheNode(go);
        rootNodeTransform = go;
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
            childGo.GetComponent<MindMapNodeScript>().setData(node.children[i], node.questions[i]);
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

        printallnode(mindMapProjs.getCurrentProj());
    }

    public void revolveSelectedNode(int dir) {
        if (selectedNode.transform.parent == null) {
            return;
        }
        selectedNode.transform.parent.GetComponent<MindMapNodeScript>().revolve(dir);
    }

    public bool selectOther(string op) {
        Transform tr = null;
        switch (op) {
            case "previous":
                if (selectedNode == rootNodeTransform) return false;
                tr = selectedNode.GetComponent<MindMapNodeScript>().getPrevNode();
                break;
            case "next":
                if (selectedNode == rootNodeTransform) return false;
                tr = selectedNode.GetComponent<MindMapNodeScript>().getNextNode();
                break;
            case "child":
                tr = selectedNode.GetComponent<MindMapNodeScript>().getChildNode();
                break;
            case "father":
                tr = selectedNode.GetComponent<MindMapNodeScript>().getFatherNode();
                break;
            default:
                break;
        }
        if (tr != null && tr != transform) {
            selectTheNode(tr);
            return true;
        }
        else {
            return false;
        }
    }

    private void selectTheNode (Transform target) {
        selectedNode = target;
        question.text = selectedNode.GetComponent<MindMapNodeScript>().question;
        answer.text = selectedNode.GetComponent<MindMapNodeScript>().node.text;
    }

    public Vector3 fixCameraPos(int dir) {
        if (dir != -1 && dir != 1) return new(0,0,0);
        Vector3 result = new(0, dir * nodeBetween, 0);
        if (dir == 1) {
            if (selectedNode.childCount >= nodeOtherChildren + 2) {
                result.z -= 2;
            }
        }
        if (dir == -1) {
            if (selectedNode.parent.childCount >= nodeOtherChildren + 2) {
                result.z += 2;
            }
        }
        return result;
    }

    private void printallnode(MindMapNode node) {
        print("text: " + node.text + " level: " + node.level + " children.Count: " + node.children.Count);
        if (node.children.Count <= 0) return;
        for (int i = 0; i < node.children.Count; i ++ ) {
            printallnode(node.children[i]);
        }
    }
}
