using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField] private GameObject MapCanvas;
    private void Start() {
        MapCanvas.SetActive(false);
    }
    public void openMap() {
        MapCanvas.SetActive(true);
    }
    public void closeMap() {
        MapCanvas.SetActive(false);
    }
}
