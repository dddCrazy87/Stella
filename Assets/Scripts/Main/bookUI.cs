using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookUI : MonoBehaviour
{
    [SerializeField] private GameObject bookCanvas;
    private void Start() {
        bookCanvas.SetActive(false);
    }
    public void openBook() {
        bookCanvas.SetActive(true);
    }
    public void closeBook() {
        bookCanvas.SetActive(false);
    }
}
