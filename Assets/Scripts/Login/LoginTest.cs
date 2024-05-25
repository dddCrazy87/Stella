using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginTest : MonoBehaviour
{
    public void OnClickLoginBtn() {
        SceneManager.LoadScene("Main");
    }
}
