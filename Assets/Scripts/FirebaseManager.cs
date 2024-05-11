using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.FirebaseAuth user;
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Register(string email, string password){
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith( task => {
            //為異步處理，需ContinueWith來接收處理的task
            //判斷task的狀態
            if(task.IsCanceled){
                print("cancled!");
                return;
            }
            if(task.IsFaulted){
                print("faulted!");
                print(task.Exception.InnerException.Message);
                return;
            }
            if(task.IsCompletedSuccessfully){
                print("register!");
            }
        });
    }
}
