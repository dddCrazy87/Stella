using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    [SerializeField] private GameObject nodeUI;
    [SerializeField] private TextMeshProUGUI answer;
    [SerializeField] private Button deleteBtn;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private int nodeOtherChildren = 1;
    private bool updateingAnserUI = false, updateingDelBtnState = false;
    private Transform selectedNode = null;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<MindMapNodeScript>().node.level == 0) {
            deleteBtn.interactable = false;
            updateingDelBtnState = false;
        }
        else {
            selectedNode = other.transform;
            updateingDelBtnState = true;
        }
        if(other.CompareTag("Node")) {
            nodeUI.SetActive(true);
            updateingAnserUI = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Node")) {
            nodeUI.SetActive(false);
            updateingAnserUI = false;
            updateingDelBtnState = false;
        }
    }

    private void Update() {
        if(updateingAnserUI) {
            if(inputField.text == ""){
                answer.text = " ";
            }
            else {
                answer.text = inputField.text;
            }
        }

        if (updateingDelBtnState) {
            if(selectedNode == null) return;
            if(selectedNode.childCount > nodeOtherChildren) {
                deleteBtn.interactable = true;
            }
            else {
                deleteBtn.interactable = false;
            }
        }
    }
}
