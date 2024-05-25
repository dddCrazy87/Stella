using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager2 : MonoBehaviour
{
    [SerializeField] private GameObject signupCanvas;
    [SerializeField] private GameObject loginCanvas;

    public void gotologin() {
        loginCanvas.SetActive(true);
        signupCanvas.SetActive(false);
    }

    public void gotosignup() {
        loginCanvas.SetActive(false);
        signupCanvas.SetActive(true);
    }

    private void Start() {
        signupCanvas.SetActive(true);
        loginCanvas.SetActive(false);
    }
}
