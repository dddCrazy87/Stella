using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser user;
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged; 
        //訂閱函式: 當Auth切換狀態就會觸發AuthStateChanged
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogIn(string email, string password){
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if(task.IsFaulted){
                print("faulted!");
                print(task.Exception.InnerException.Message);
                return;
            }
            if(task.IsCompletedSuccessfully){
                print("Login!");
            }
        });
    }

    public void LogOut(){
        auth.SignOut();
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

    void AuthStateChanged(object sender, System.EventArgs eventArgs){
        //檢查現在的使用者是不是user
        //不等於 表示角色有切換AuthStateChanged（登入到登出、登出到登入）
        if(auth.CurrentUser != user){
            user = auth.CurrentUser; //更新使用者
            if(user != null){
                print($"Login! user email: {user.Email}");
            }
        } 
        
    }

    //  當物件被刪除時，解除訂閱
    void OnDestroy() {
        auth.StateChanged -= AuthStateChanged; 
    }
}
