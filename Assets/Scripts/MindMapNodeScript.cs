using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindMapNodeScript : MonoBehaviour
{
    private Transform mindMapController;
    [SerializeField] private float orbitRadius = 2f;
    public MindMapNode mindMapNode;

    private void Start() {
        mindMapController = findMindMapController(transform);
    }

    public void rearrange() {
        int cnt = transform.childCount;

        if (cnt == 2) {
            Vector3 pos = transform.position; pos.y -= 1.5f;
            transform.GetChild(1).transform.position = pos;
            return;
        }

        float angleStep = 360f / (cnt-1);
        for(int i = 1; i < cnt; i ++) {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 pos = calculatePositionOnCircle(angle);
            Transform tf = transform.GetChild(i).transform;
            tf.SetPositionAndRotation(pos, rotation);
        }
    }

    private Vector3 calculatePositionOnCircle(float angle)
    {
        float x = Mathf.Sin(angle * Mathf.Deg2Rad) * orbitRadius;
        float z = Mathf.Cos(angle * Mathf.Deg2Rad) * orbitRadius;
        return new Vector3(x + transform.position.x,  + transform.position.y - 1.5f, z + transform.position.z);
    }

    private Transform findMindMapController (Transform currentTransform) {
        if (currentTransform.parent == null) {
            return currentTransform;
        }
        else {
            return findMindMapController(currentTransform.parent);
        }
    }
    private void OnMouseDown() {
        Debug.Log(mindMapNode.text);
        mindMapController.gameObject.GetComponent<ＭindMapController>().selectedNode = gameObject;
    }
}
