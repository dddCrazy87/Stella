using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // user info canvas toggle
    [SerializeField] private GameObject main, userInfoUI;
    private bool isUserInfoOpen = false;
    public void userInfoToggle() {
        if (isUserInfoOpen == false) {
            userInfoUI.SetActive(true);
            main.SetActive(false);
            isUserInfoOpen = true;
        }
        else if (isUserInfoOpen == true) {
            userInfoUI.SetActive(false);
            main.SetActive(true);
            isUserInfoOpen = false;
        }
    }

    // edit btn toggle
    [SerializeField] private Transform saveBtnMask, newBtnMask, deleteBtnMask;
    [SerializeField] private float editBtnsAnimSpeed = 0.1f;
    private bool isEditBtnOpen = false;
    private int isEditBtnAnimPlaying = 3;
    private RectTransform saveBtn, newBtn, deleteBtn;
    private Vector2 saveBtnPos, newBtnPos, deleteBtnPos;
    private Coroutine saveBtnEffect, newBtnEffect, deleteBtnEffect;
    
    public void editBtnToggle() {
        if(isEditBtnAnimPlaying < 3) return;

        if (saveBtnEffect != null) StopCoroutine(saveBtnEffect);
        if (newBtnEffect != null) StopCoroutine(newBtnEffect);
        if (deleteBtnEffect != null) StopCoroutine(deleteBtnEffect);

        if (isEditBtnOpen == false) {
            saveBtnEffect = StartCoroutine(SaveBtnEffect(1));
            newBtnEffect = StartCoroutine(NewBtnEffect(1));
            deleteBtnEffect = StartCoroutine(DeleteBtnEffect(1));
            isEditBtnOpen = true;
        }
        else if (isEditBtnOpen == true) {
            saveBtnEffect = StartCoroutine(SaveBtnEffect(-1));
            newBtnEffect = StartCoroutine(NewBtnEffect(-1));
            deleteBtnEffect = StartCoroutine(DeleteBtnEffect(-1));
            isEditBtnOpen = false;
        }
    }

    IEnumerator SaveBtnEffect(int dir) {
        isEditBtnAnimPlaying = 0;
        Vector2 start = new(), end = new(), cur = saveBtn.anchoredPosition;
        if(dir == 1) {
            start = saveBtn.anchoredPosition; end = saveBtnPos;
        }
        if(dir == -1) {
            start = saveBtn.anchoredPosition;
            end = saveBtnMask.GetChild(1).GetComponent<RectTransform>().anchoredPosition;
        }
        Vector2 norV = (end - start).normalized * editBtnsAnimSpeed * Time.deltaTime;
        while(Vector2.Distance(cur, end) > editBtnsAnimSpeed * Time.deltaTime) {
            Vector2 savePos = cur + norV;
            cur += norV;
            saveBtn.anchoredPosition = savePos;
            yield return null;
        }
        saveBtn.anchoredPosition = end;
        isEditBtnAnimPlaying += 1;
    }

    IEnumerator NewBtnEffect(int dir) {
        isEditBtnAnimPlaying = 0;
        Vector2 start = new(), end = new(), cur = newBtn.anchoredPosition;
        if(dir == 1) {
            start = newBtn.anchoredPosition; end = newBtnPos;
        }
        if(dir == -1) {
            start = newBtn.anchoredPosition;
            end = newBtnMask.GetChild(1).GetComponent<RectTransform>().anchoredPosition;
        }
        Vector2 norV = (end - start).normalized * editBtnsAnimSpeed * Time.deltaTime;
        while(Vector2.Distance(cur, end) > editBtnsAnimSpeed * Time.deltaTime) {
            Vector2 newPos = cur + norV;
            cur += norV;
            newBtn.anchoredPosition = newPos;
            yield return null;
        }
        newBtn.anchoredPosition = end;
        isEditBtnAnimPlaying += 1;
    }

    IEnumerator DeleteBtnEffect(int dir) {
        isEditBtnAnimPlaying = 0;
        Vector2 start = new(), end = new(), cur = deleteBtn.anchoredPosition;
        if(dir == 1) {
            start = deleteBtn.anchoredPosition; end = deleteBtnPos;
        }
        if(dir == -1) {
            start = deleteBtn.anchoredPosition;
            end = deleteBtnMask.GetChild(1).GetComponent<RectTransform>().anchoredPosition;
        }
        Vector2 norV = (end - start).normalized * editBtnsAnimSpeed * Time.deltaTime;
        while(Vector2.Distance(cur, end) > editBtnsAnimSpeed * Time.deltaTime) {
            Vector2 newPos = cur + norV;
            cur += norV;
            deleteBtn.anchoredPosition = newPos;
            yield return null;
        }
        deleteBtn.anchoredPosition = end;
        isEditBtnAnimPlaying += 1;
    }

    // sky
    [SerializeField] private GameObject skyUI;
    [SerializeField] private GameObject qaUI;
    [SerializeField] private GameObject editBtnsUI;
    [SerializeField] private GameObject mmCamera;
    public void OnClickSkyBtn() {
        qaUI.SetActive(false);
        editBtnsUI.SetActive(false);
        mmCamera.SetActive(false);
        skyUI.SetActive(true);
    }

    public void OnClickSkyUiBack() {
        skyUI.SetActive(false);
        qaUI.SetActive(true);
        mmCamera.SetActive(true);
        editBtnsUI.SetActive(true);
    }

    private void Start() {
        // start
        main.SetActive(true);
        userInfoUI.SetActive(false);
        skyUI.SetActive(false);

        // to edit btn
        saveBtn = saveBtnMask.GetChild(0).GetComponent<RectTransform>();
        saveBtnPos = saveBtn.anchoredPosition;
        saveBtn.anchoredPosition = saveBtnMask.GetChild(1).GetComponent<RectTransform>().anchoredPosition;

        newBtn = newBtnMask.GetChild(0).GetComponent<RectTransform>();
        newBtnPos = newBtn.anchoredPosition;
        newBtn.anchoredPosition = newBtnMask.GetChild(1).GetComponent<RectTransform>().anchoredPosition;

        deleteBtn = deleteBtnMask.GetChild(0).GetComponent<RectTransform>();
        deleteBtnPos = deleteBtn.anchoredPosition;
        deleteBtn.anchoredPosition = deleteBtnMask.GetChild(1).GetComponent<RectTransform>().anchoredPosition;
    }

}
