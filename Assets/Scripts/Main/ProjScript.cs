using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjScript : MonoBehaviour
{
    public int projIndex;
    public string projName;
    [SerializeField] private TextMeshProUGUI projNameUI;

    public void setProjsNameUI(string name) {
        projName = name;
        projNameUI.text = projName;
    }
}
