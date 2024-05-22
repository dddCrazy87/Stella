using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserInfo", menuName = "ScriptableObj/UserInfo",order = 2)]
public class UserInfo : ScriptableObject
{
    public int character;
    public string nickname;
    public int level;
    public int exp;
    public  int[] achievements;
    public  int[] collections;
    public MindMapNode[] projs;
}
