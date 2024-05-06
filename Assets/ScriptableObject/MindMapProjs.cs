using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MindMapNode {
    public string text;
    public int level;
    public string[] questions;
    public List<MindMapNode> children;

    public MindMapNode(string nodeText, int nodeLevel) {
        text = nodeText;
        level = nodeLevel;
        children = new List<MindMapNode>();
    }

    public MindMapNode(string nodeText, int nodeLevel, List<MindMapNode> childrenNode) {
        text = nodeText;
        level = nodeLevel;
        children = childrenNode;
    }
}

[CreateAssetMenu(fileName = "MindMapProjs", menuName = "ScriptableObj/MindMapProjs",order = 1)]
public class MindMapProjs : ScriptableObject
{
    private MindMapNode[] mindMapProjs = new MindMapNode[] {
        new ("a", 0),
        new ("b", 0, new List<MindMapNode> {
            new ("b.1", 1),
            new ("b.2", 1)
        }),
        new ("c", 0, new List<MindMapNode> {
            new ("c.1", 1, new List<MindMapNode> {
                new ("c.1.1", 2)
            }),
            new ("c.2", 1),
            new ("c.3", 1, new List<MindMapNode> {
                new ("c.3.1", 2),
                new ("c.3.2", 2),
                new ("c.3.3", 2)
            })
        }),
    };

    public int currentProjIndex = 2;

    public MindMapNode getCurrentProj() {
        return mindMapProjs[currentProjIndex];
    }
}
