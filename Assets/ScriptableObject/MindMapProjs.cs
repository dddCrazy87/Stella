using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class MindMapNode {
    public string text;
    public int level;

    public string question;
    public string[] childQuestions;
    public List<MindMapNode> children;

    public MindMapNode() {
        text = "";
        level = 0;
        question = "寫下一個主題吧！";
        children = new();
    }

    public MindMapNode(MindMapNode node) {
        text = node.text;
        level = node.level;
        question = node.question;
        children = node.children;
    }

    public MindMapNode(string nodeText, string nodeQusetion, int nodeLevel) {
        text = nodeText;
        level = nodeLevel;
        question = nodeQusetion;
        children = new();
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

    public MindMapNode changeAnswer(string answer) {
        text = answer;
        childQuestions = generateQuestion();
        children = new List<MindMapNode> { new("", childQuestions[0], level + 1)};
        return children[0];
    }

    public MindMapNode addEmptyChildren() {
        children.Add(new("", childQuestions[children.Count], level+1));
        return children.Last();
    }
}

[CreateAssetMenu(fileName = "MindMapProjs", menuName = "ScriptableObj/MindMapProjs",order = 1)]
public class MindMapProjs : ScriptableObject
{
    public List<MindMapNode> mindMapProjs = new List<MindMapNode>{};
    public MindMapNode editingProj = new();

    // public MindMapNode getProj(int index) {
    //     return mindMapProjs[index];
    // }

    public void saveProj() {
        mindMapProjs.Add(new(editingProj));
        editingProj = new();
    }
}
