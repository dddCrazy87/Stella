using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MindMapNode {
    public string text;
    public int level;

    public string question;
    public string[] childQuestions;
    public List<MindMapNode> children;

    public MindMapNode() {
        text = "";
        level = 0;
        children = new();
    }

    public MindMapNode(string nodeText, string nodeQusetion, int nodeLevel) {
        text = nodeText;
        level = nodeLevel;
        question = nodeQusetion;
        children = new List<MindMapNode>();
    }

    public MindMapNode(string nodeText, string nodeQusetion, int nodeLevel, List<MindMapNode> childrenNode) {
        text = nodeText;
        level = nodeLevel;
        question = nodeQusetion;
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
        new ("c", "寫下一個主題吧！", 0, new List<MindMapNode> {
            new ("c.1", "你對C的感想是什麼？1", 1, new List<MindMapNode> {
                new ("c.1.1", "你對C.1的感想是什麼？1", 2)
            }),
            new ("c.2", "你對C的感想是什麼？2", 1),
            new ("c.3", "你對C的感想是什麼？3", 1, new List<MindMapNode> {
                new ("c.3.1", "你對C.3的感想是什麼？1", 2),
                new ("c.3.2", "你對C.3的感想是什麼？2", 2),
                new ("c.3.3", "你對C.3的感想是什麼？3", 2)
            })
        }),
    };

    public int currentProjIndex = 0;

    public MindMapNode getCurrentProj() {
        return mindMapProjs[currentProjIndex];
    }
}
