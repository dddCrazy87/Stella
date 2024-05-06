using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MindMapNode {
    public string text;
    public int level;
    public string[] questions;
    public List<MindMapNode> children;

    public MindMapNode() {
        text = "root";
        level = 0;
        questions = new string[] { "寫下你的主題" };
        children = new();
    }

    public MindMapNode(string nodeText, int nodeLevel) {
        text = nodeText;
        level = nodeLevel;
        questions = generateQuestion();
        children = new List<MindMapNode>();
    }

    public MindMapNode(string nodeText, int nodeLevel, List<MindMapNode> childrenNode) {
        text = nodeText;
        level = nodeLevel;
        questions = generateQuestion();
        children = childrenNode;
    }

    private string[] generateQuestion() {
        string[] result = new string[5] {
            "你對" + text + "的感想11111",
            "你對" + text + "的感想22222",
            "你對" + text + "的感想33333",
            "你對" + text + "的感想44444",
            "你對" + text + "的感想55555"
        };
        return result;
    }
}

[CreateAssetMenu(fileName = "MindMapProjs", menuName = "ScriptableObj/MindMapProjs",order = 1)]
public class MindMapProjs : ScriptableObject
{
    private MindMapNode[] mindMapProjs = new MindMapNode[] {
        new (),
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
