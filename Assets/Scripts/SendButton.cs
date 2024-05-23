using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SendButton : MonoBehaviour {
    [SerializeField] private Image buttonImg;
    [SerializeField] private float targetAlpha = 0.35f;
    [SerializeField] private float clickEffectSpeed = 0.004f;
    [SerializeField] private RectTransform sendButtonRope;
    [SerializeField] private float curPosY;
    [SerializeField] private float targetPosY = 420f;
    [SerializeField] private float pullEffectSpeed = 0.1f;
    private Color buttonColor;
    private bool isClickEffectPlaying, isPullEffectPlaying;

    private void Start() {
        buttonColor = buttonImg.color;
        isClickEffectPlaying = false;
        isPullEffectPlaying = false;
        curPosY = sendButtonRope.anchoredPosition.y;
    }

    private Coroutine sendClickEffect, ropePullEffect;
    public void OnSendButtonClick() {
        if (isClickEffectPlaying || isPullEffectPlaying) return;
        if (sendClickEffect != null) StopCoroutine(sendClickEffect);
        if (ropePullEffect != null)  StopCoroutine(sendClickEffect);
        sendClickEffect = StartCoroutine(SendClickEffect());
        ropePullEffect = StartCoroutine(RopePullEffect());
    }

    IEnumerator SendClickEffect () {
        
        isClickEffectPlaying = true;
        float curA = 0f;
        while (targetAlpha - curA >= 0f) {
            curA += clickEffectSpeed * Time.deltaTime;
            buttonColor.a = curA;
            buttonImg.color = buttonColor;
            yield return null;
        }
        buttonColor.a = 0f;
        buttonImg.color = buttonColor;
        isClickEffectPlaying = false;
    }

    IEnumerator RopePullEffect () {
        
        isPullEffectPlaying = true;
        float curY = curPosY;
        while (curY - targetPosY >= 0f) {
            curY -= pullEffectSpeed * Time.deltaTime;
            sendButtonRope.anchoredPosition =  new(sendButtonRope.anchoredPosition.x,curY);
            yield return null;
        }
        curY = targetPosY;
        while (curPosY - curY >= 0f) {
            curY += pullEffectSpeed * Time.deltaTime;
            sendButtonRope.anchoredPosition =  new(sendButtonRope.anchoredPosition.x,curY);
            yield return null;
        }
        sendButtonRope.anchoredPosition =  new(sendButtonRope.anchoredPosition.x,curPosY);
        isPullEffectPlaying = false;
    }
}
