using UnityEngine;
using System.Collections;
using UnityEditor;
using wuxingogo.Runtime;
using System.Reflection;
using System.Collections.Generic;
using System;
using Object = UnityEngine.Object;

namespace wuxingogoEditor
{
    [CustomEditor(typeof(XScriptableObject), true)]
    public class XScriptObjectEditor : XMonoBehaviourEditor
    {
    }

}