using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SendButton : MonoBehaviour {
    [SerializeField] private Image buttonImg;
    [SerializeField] private float targetAlpha = 0.5f;
    [SerializeField] private float effectSpeed = 0.1f;
    private Color buttonColor;
    private bool isClickEffectPlaying;

    private void Start() {
        buttonColor = buttonImg.color;
        isClickEffectPlaying = false;
    }

    private Coroutine sendClickEffect;
    public void OnSendButtonClick() {
        if (isClickEffectPlaying) return;
        if (sendClickEffect != null) StopCoroutine(sendClickEffect);
        sendClickEffect = StartCoroutine(SendClickEffect());
    }

    IEnumerator SendClickEffect () {
        
        isClickEffectPlaying = true;
        float curA = 0f;
        while (targetAlpha - curA >= 0f) {
            curA += effectSpeed;
            buttonColor.a = curA;
            buttonImg.color = buttonColor;
            yield return null;
        }
        buttonColor.a = 0f;
        buttonImg.color = buttonColor;
        isClickEffectPlaying = false;
    }
}
