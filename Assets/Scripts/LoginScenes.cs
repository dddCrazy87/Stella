using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScenes : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    FirebaseManager firebaseManager;
    [SerializeField]
    InputField inputEmail;
    [SerializeField]
    InputField inputPassword;
    [SerializeField]
    GameObject panelLogin;
    [SerializeField]
    GameObject panelInfo;

    [SerializeField]
    Text textEmail;

    void Start()
    {
        firebaseManager.auth.StateChanged += AuthStateChanged; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogIn(){
        firebaseManager.LogIn(inputEmail.text, inputPassword.text);
    }

    public void LogOut(){
        firebaseManager.LogOut();
    }

    public void Register(){
        firebaseManager.Register(inputEmail.text, inputPassword.text);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs){
        if(firebaseManager.user == null){
            textEmail.text = "";
            panelLogin .SetActive(true);
            panelInfo .SetActive(false);

        } else{
            textEmail.text = firebaseManager.user.Email;
            panelLogin .SetActive(false);
            panelInfo .SetActive(true);
        }
        
    }

    void OnDestroy() {
        firebaseManager.auth.StateChanged -= AuthStateChanged; 
    }

}
