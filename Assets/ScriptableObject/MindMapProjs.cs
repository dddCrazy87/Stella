using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MindMapNode {
    public string text;
    public string[] questions;
    public MindMapNode[] children;

    public MindMapNode(string nodeText) {
        text = nodeText;
        children = new MindMapNode[] {};
    }

    public MindMapNode(string nodeText, MindMapNode[] childrenNode) {
        text = nodeText;
        children = childrenNode;
    }
}

[CreateAssetMenu(fileName = "MindMapProjs", menuName = "ScriptableObj/MindMapProjs",order = 1)]
public class MindMapProjs : ScriptableObject
{
    private MindMapNode[] mindMapProjs = new MindMapNode[] {
        new("a"),
        new("b", new MindMapNode[] {
            new("b.1"),
            new("b.2")
        }),
        new("c", new MindMapNode[] {
            new("c.1", new MindMapNode[] {
                new("c.1.1")
            }),
            new("c.2"),
            new("c.3", new MindMapNode[] {
                new("c.3.1"),
                new("c.3.2"),
                new("c.3.3"),
            }),
        }),
    };

    public int currentProjIndex = 2;

    public MindMapNode getCurrentProj() {
        return mindMapProjs[currentProjIndex];
    }
}
