using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MindMapController : MonoBehaviour
{
    public MindMapProjs mindMapProjs;
    private MindMapNode editingProj;
    private Transform rootNodeTransform;
    public Transform selectedNode;
    [SerializeField] private Transform nodePrefab;
    // [Serializable]
    // private struct nodeMaterialData {
    //     [SerializeField] private Material nodeMaterial;
    //     [SerializeField] private Color[] colors;
    //     [SerializeField] private float probability;
    // }
    
    //[SerializeField] private nodeMaterialData[] nodeMaterials;

    [SerializeField] private Material[] nodeMaterials;
    [SerializeField] private Color[] nodeMaterialColors;
    [SerializeField] private float nodeBetween = 1.5f;
    [SerializeField] public float nodeRadius = 1.5f;
    [SerializeField] private int nodeOtherChildren = 0;
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private TMP_InputField answer;
    [SerializeField] private Transform mmCamera;
    private Vector3 mmCameraPosDefault;
    [SerializeField] private ProjsUIManager projsUIManager;
    [SerializeField] private UserInput userInput;
    [SerializeField] private TextMeshProUGUI projName;
 
    void Start() {
        editingProj = new();
        mindMapProjs.initCurProjIndex();
        generateMindMap(editingProj, transform);
        answer.onSubmit.AddListener(answerSubmit);
        mmCameraPosDefault = mmCamera.position;
    }

    public void resetRootNode(MindMapNode node) {
        Destroy(rootNodeTransform.gameObject);
        editingProj = node;
        mindMapProjs.initCurProjIndex();
        generateMindMap(editingProj, transform);
        mmCamera.position = mmCameraPosDefault;
    }
    
    private void generateMindMap(MindMapNode node, Transform father) {
        Transform go = Instantiate(nodePrefab, father);
        Material material = new(nodeMaterials[Random.Range(0, nodeMaterials.Length)]) {
            color = nodeMaterialColors[0]
        };
        go.GetChild(0).GetComponent<MeshRenderer>().material = material;
        go.GetComponent<MindMapNodeScript>().setData(node);
        selectedNode = go;
        question.text = selectedNode.GetComponent<MindMapNodeScript>().question;
        answer.text = selectedNode.GetComponent<MindMapNodeScript>().node.text;
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
            Material material = new(nodeMaterials[Random.Range(0, nodeMaterials.Length)]) {
                color = nodeMaterialColors[node.children[i].level % nodeMaterialColors.Length]
            };
            childGo.GetChild(0).GetComponent<MeshRenderer>().material = material;
            childGo.GetComponent<MindMapNodeScript>().setData(node.children[i]);
            childrenGo.Add(childGo);
        }
        
        father.GetComponent<MindMapNodeScript>().rearrange();
        
        for (int i = 0; i < node.children.Count; i ++) {
            generateMindMapRec(node.children[i], childrenGo[i]);
        }
    }

    // send answer
    public void answerSubmitByButton() {
        answerSubmit(answer.text);
    }
    private void answerSubmit(string answerText) {
        if (selectedNode == null) return;
        if (answerText == null) return;
        if (answerText == "") return;

        if (selectedNode.childCount > nodeOtherChildren) {
            List<Transform> trash = new();
            for (int i = nodeOtherChildren; i < selectedNode.childCount; i ++) {
                trash.Add(selectedNode.GetChild(i));
            }
            foreach (var item in trash) {
                item.SetParent(selectedNode.GetChild(0));
                Destroy(item.gameObject);
            }
            selectedNode.GetComponent<MindMapNodeScript>().prevSelectedChild = nodeOtherChildren;
        }
        if (selectedNode != rootNodeTransform) {
            string textTmp = selectedNode.GetComponent<MindMapNodeScript>().node.text;
            if (selectedNode.parent.childCount == nodeOtherChildren + 1 && textTmp == "") {
                mmCamera.position += new Vector3(0,0,nodeRadius);
            }
        }
        else {
            projName.text = answerText;
        }

        Transform go = Instantiate(nodePrefab, selectedNode);
        MindMapNode newNode = selectedNode.GetComponent<MindMapNodeScript>().node.changeAnswer(answerText);
        go.GetComponent<MindMapNodeScript>().setData(newNode);
        Material material = new(nodeMaterials[Random.Range(0, nodeMaterials.Length)]) {
            color = nodeMaterialColors[newNode.level % nodeMaterialColors.Length]
        };
        go.GetChild(0).GetComponent<MeshRenderer>().material = material;
        selectedNode.GetComponent<MindMapNodeScript>().rearrange();
        go.rotation = Quaternion.identity;

        if (selectedNode != rootNodeTransform && selectedNode.GetSiblingIndex() == selectedNode.parent.childCount - 1) {
            Transform go2 = Instantiate(nodePrefab, selectedNode.parent);
            MindMapNode newNode2 = selectedNode.parent.GetComponent<MindMapNodeScript>().node.addEmptyChildren();
            go2.GetComponent<MindMapNodeScript>().setData(newNode2);
            Material material2 = new(nodeMaterials[Random.Range(0, nodeMaterials.Length)]) {
                color = nodeMaterialColors[newNode2.level % nodeMaterialColors.Length]
            };
            go2.GetChild(0).GetComponent<MeshRenderer>().material = material2;
            selectedNode.parent.GetComponent<MindMapNodeScript>().rearrange();
            float newAngle = 360/(selectedNode.parent.childCount-nodeOtherChildren)* -2;
            selectedNode.parent.rotation = Quaternion.Euler(0, newAngle, 0);
        }
    }

    // save proj
    public void OnClickSaveBtn() {
        if (mindMapProjs.curProjIndex == -1) {
            mindMapProjs.saveProj(editingProj);
            projsUIManager.addNewProj();
        }
        else {
            mindMapProjs.saveProj(editingProj);
            projsUIManager.updateProjUI();
        }
    }

    // new proj
    public void OnClickNewBtn() {
        resetRootNode(new());
    }

    // delete a node
    private Coroutine resetNodeChild;
    public void DeleteSelectedNode() {
        Transform toDestroy = selectedNode;
        selectOther("father");
        selectedNode.GetComponent<MindMapNodeScript>().node.children.Remove(toDestroy.GetComponent<MindMapNodeScript>().node);
        Destroy(toDestroy.gameObject);
        userInput.cameraTargetPos = selectedNode.position + mmCameraPosDefault;
        userInput.isMoving_camera = true;
        if (resetNodeChild != null) StopCoroutine(ResetNodeChild());
        resetNodeChild = StartCoroutine(ResetNodeChild());
    }

    private IEnumerator ResetNodeChild() {
        yield return null;
        yield return null;
        yield return null;
        selectedNode.GetComponent<MindMapNodeScript>().rearrange();
        if (selectedNode.GetComponent<MindMapNodeScript>().prevSelectedChild == nodeOtherChildren) {
            selectedNode.GetComponent<MindMapNodeScript>().prevSelectedChild = selectedNode.childCount - 1;
        }
        else {
            selectedNode.GetComponent<MindMapNodeScript>().prevSelectedChild -= 1;
        }
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
                result.z -= nodeRadius;
            }
        }
        if (dir == -1) {
            if (selectedNode.parent.childCount >= nodeOtherChildren + 2) {
                result.z += nodeRadius;
            }
        }
        return result;
    }

    public void printallnode() {
        // foreach (var item in mindMapProjs.mindMapProjs) {
        //     printallnoderec(item);
        // }
        printallnoderec(editingProj);
        //print(selectedNode.GetComponent<MindMapNodeScript>().node.text);
    }

    private void printallnoderec(MindMapNode node) {
        print("text: " + node.text + " level: " + node.level + " children.Count: " + node.children.Count);
        if (node.children.Count <= 0) return;
        for (int i = 0; i < node.children.Count; i ++ ) {
            printallnoderec(node.children[i]);
        }
    }
}
