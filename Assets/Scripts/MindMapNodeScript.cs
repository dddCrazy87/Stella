using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MindMapNodeScript : MonoBehaviour
{
    private MindMapController mindMapController;
    [SerializeField] private float orbitRadius = 2f;
    public MindMapNode mindMapNode;

    private void Start() {
        mindMapController = findMindMapController(transform).GetComponent<MindMapController>();
    }

    private Transform findMindMapController (Transform currentTransform) {
        if (currentTransform.parent == null) {
            return currentTransform;
        }
        else {
            return findMindMapController(currentTransform.parent);
        }
    }

    public void rearrange() {
        Quaternion rotation = transform.rotation;
        int cnt = transform.childCount;

        if (cnt <= 2) {
            Vector3 pos = transform.position; pos.y -= 1.5f;
            transform.GetChild(1).transform.position = pos;
            transform.GetChild(1).transform.rotation = rotation;
            return;
        }

        float angleStep = 360f / (cnt-1);
        for(int i = 1; i < cnt; i ++) {
            float angle = i * angleStep + rotation.eulerAngles.y;
            Vector3 pos = calculatePositionOnCircle(angle);
            Transform go = transform.GetChild(i).transform;
            go.position = pos;
            go.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private Vector3 calculatePositionOnCircle(float angle)
    {
        float x = Mathf.Sin(angle * Mathf.Deg2Rad) * orbitRadius;
        float z = Mathf.Cos(angle * Mathf.Deg2Rad) * orbitRadius;
        return new Vector3(x + transform.position.x,  + transform.position.y - 1.5f, z + transform.position.z);
    }

    private Coroutine coroutine;
    public void revolve(int dir) {
        
        if (dir != 1 && dir != -1) {
            Debug.Log("parameter should be 1 or -1.");
            return;
        }
        if (transform.childCount <= 2) {
            return;
        }
        
        float angleStep = 360f / (transform.childCount - 1);

        if (coroutine != null) {
            StopCoroutine(coroutine);
        }
        
        coroutine = StartCoroutine(revolveCoroutine(dir, angleStep));
    }

    IEnumerator revolveCoroutine(int dir, float angleStep) {
        
        Quaternion rotation = Quaternion.Euler(0, dir, 0);
        float start = transform.rotation.eulerAngles.y;
        float end = start;
        
        while (Math.Abs(end - start) < angleStep) {
            transform.rotation *= rotation;
            end -= 1;
            yield return null;
        }
        rotation= Quaternion.Euler(0, start + angleStep * dir, 0);
        transform.rotation = rotation;
    }

    private void OnMouseDown() {
        Debug.Log(mindMapNode.text);
        Debug.Log(mindMapNode.children.Count);
        mindMapController.selectedNode = gameObject;
    }
}
