using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjScript : MonoBehaviour
{
    public int projIndex;
    public string projName;
    private Vector2 nameStartPos;
    [SerializeField] private TextMeshProUGUI projNameUI;
    [SerializeField] private int maxWordNum = 6;
    [SerializeField] private float wordSpace = 36f;

    private void Start() {
        nameStartPos = projNameUI.rectTransform.anchoredPosition;
    }

    public void setProjsNameUI(string name) {
        projName = name;
        projNameUI.text = projName;
        if(projName.Length > maxWordNum) {
            projNameUI.rectTransform.anchoredPosition = new Vector2((projName.Length - maxWordNum)/2*wordSpace, nameStartPos.y);
        }
        else {
            projNameUI.rectTransform.anchoredPosition = nameStartPos;
        }
    }

    public void selectProj() {
        transform.parent.GetComponent<ProjsUIManager>().selectProj(projIndex);
    }
}
