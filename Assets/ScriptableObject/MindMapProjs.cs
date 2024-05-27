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
        if (text == "適合小孩吃的點心食譜") {
            string[] result = new string[5] {
                "點心會在什麼時候享用？",
                "點心的味道如何",
                "你對" + text + "的感想33333",
                "你對" + text + "的感想44444",
                "你對" + text + "的感想55555"
            };
            return result;
        }
        else if (text == "下午放學後") {
            string[] result = new string[5] {
                "你放學後的心情通常是怎樣的？",
                "你對" + text + "的感想22222",
                "你對" + text + "的感想33333",
                "你對" + text + "的感想44444",
                "你對" + text + "的感想55555"
            };
            return result;
        }
        else if (text == "甜甜的") {
            string[] result = new string[5] {
                "你認為有什麼食物是甜的？",
                "你對" + text + "的感想22222",
                "你對" + text + "的感想33333",
                "你對" + text + "的感想44444",
                "你對" + text + "的感想55555"
            };
            return result;
        }
        else if (text == "巧克力") {
            string[] result = new string[5] {
                "你最喜歡的巧克力是什麼品牌或款式？",
                "你有吃過什麼巧克力製品？",
                "你對" + text + "的感想33333",
                "你對" + text + "的感想44444",
                "你對" + text + "的感想55555"
            };
            return result;
        }
        else if (text == "蛋糕") {
            string[] result = new string[5] {
                "你會在什麼時候選擇吃蛋糕？",
                "你對" + text + "的感想22222",
                "你對" + text + "的感想33333",
                "你對" + text + "的感想44444",
                "你對" + text + "的感想55555"
            };
            return result;
        }
        else{
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
    public int curProjIndex = -1;

    public void initCurProjIndex() {
        curProjIndex = -1;
    }

    public void saveProj(MindMapNode node) {
        if (curProjIndex == -1) {
            mindMapProjs.Add(new(node));
            curProjIndex = mindMapProjs.Count - 1;
        }
        else {
            mindMapProjs[curProjIndex] = new(node);
        }
    }
}
