using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MindMapNodeScript : MonoBehaviour
{
    [SerializeField] private float orbitRadius = 2f;
    [SerializeField] private int otherChildren = 0;
    [SerializeField] private float nodeBetween = 1.5f;
    [SerializeField] private float revolveSpeed = 1f;
    public MindMapNode node;
    public int prevSelectedChild;
    public string question;

    private void Start() {
        prevSelectedChild = otherChildren;
    }

    public void setData(MindMapNode nodeData) {
        node = nodeData;
        question = nodeData.question;
    }

    public void rearrange() {
        Quaternion rotation = transform.rotation;
        transform.rotation = Quaternion.identity;
        int cnt = transform.childCount - otherChildren;

        if (cnt <= 1) {
            Vector3 pos = transform.position; pos.y -= nodeBetween;
            transform.GetChild(otherChildren).transform.SetPositionAndRotation(pos, rotation);
            return;
        }

        float angleStep = 360f / cnt;
        for(int i = otherChildren; i < transform.childCount; i ++) {
            float angle = (i - otherChildren) * angleStep;
            Vector3 pos = calculatePositionOnCircle(-angle);
            Transform go = transform.GetChild(i).transform;
            go.SetPositionAndRotation(pos, Quaternion.identity);
        }
    }

    private Vector3 calculatePositionOnCircle(float angle) {
        float x = Mathf.Sin(angle * Mathf.Deg2Rad) * orbitRadius;
        float z = Mathf.Cos(angle * Mathf.Deg2Rad) * orbitRadius;
        return new Vector3(x + transform.position.x,  + transform.position.y - nodeBetween, z + transform.position.z);
    }

    private Coroutine coroutine;
    public bool isCoroutineRunning = false;
    public void revolve(int dir) {

        if (isCoroutineRunning) {
            return;
        }
        if (dir != 1 && dir != -1) {
            return;
        }
        if (transform.childCount <= otherChildren + 1) {
            return;
        }
        
        float angleStep = 360f / (transform.childCount - otherChildren);

        if (coroutine != null) {
            StopCoroutine(coroutine);
        }
        
        coroutine = StartCoroutine(revolveCoroutine(dir, angleStep));
    }

    IEnumerator revolveCoroutine(int dir, float angleStep) {
        isCoroutineRunning = true;
        Quaternion rotation = Quaternion.Euler(0, dir*revolveSpeed, 0);
        float start = transform.rotation.eulerAngles.y;
        float end = start;
        
        while (Math.Abs(end - start) < angleStep) {
            transform.rotation *= rotation;
            end -= revolveSpeed;
            yield return null;
        }
        rotation= Quaternion.Euler(0, start + angleStep * dir, 0);
        transform.rotation = rotation;
        isCoroutineRunning = false;
        Transform selectedNode = transform.GetChild(prevSelectedChild);
        if (selectedNode.childCount > otherChildren) {
            float angle = 360 / (selectedNode.childCount - otherChildren);
            angle *= selectedNode.GetComponent<MindMapNodeScript>().prevSelectedChild - otherChildren;
            selectedNode.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    public Transform getNextNode() {
        if (transform.parent.GetComponent<MindMapNodeScript>().isCoroutineRunning) {
            return null;
        }
        int result = transform.GetSiblingIndex() + 1;
        if (result >= transform.parent.childCount) {
            result = otherChildren;
        }
        transform.parent.GetComponent<MindMapNodeScript>().prevSelectedChild = result;
        return transform.parent.transform.GetChild(result);
    }

    public Transform getPrevNode() {
        if (transform.parent.GetComponent<MindMapNodeScript>().isCoroutineRunning) {
            return null;
        }
        int result = transform.GetSiblingIndex() - 1;
        if (result <= otherChildren - 1) {
            result = transform.parent.childCount - 1;
        }
        transform.parent.GetComponent<MindMapNodeScript>().prevSelectedChild = result;
        return transform.parent.transform.GetChild(result);
    }

    public Transform getChildNode() {
        if (transform.childCount == otherChildren) {
            return null;
        }
        return transform.GetChild(prevSelectedChild);
    }

    public Transform getFatherNode() {
        return transform.parent;
    }

    private void OnMouseDown() {
        //print("now: " + node.text + " level:" + node.level + " cnt: " + node.children.Count);
    }
}

