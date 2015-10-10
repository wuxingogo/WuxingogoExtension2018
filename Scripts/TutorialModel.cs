using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
[System.Serializable]
public class TutorialModel {

    [HeaderAttribute("标题")]
    public string title = "";
    [HeaderAttribute("点亮的物体")]
    public List<GameObject> effectObjects = null;
    [HeaderAttribute( "调用函数" )]
    public List<XEvent> events;
    [HeaderAttribute( "对话内容" )]
    public List<string> talkContent = null;
    [HeaderAttribute( "执行顺序" )]
    public TutorialOrder[] orders = null;

}

public enum TutorialBehaviour
{
    Dialog,
    ButtonClickEvent,
    Action,
    None
}

[System.Serializable]
public class TutorialOrder
{
    public float waitTime;
    public TutorialBehaviour behaviour;

}


