using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;
 
 
//[CustomPropertyDrawer(typeof(XAttribute))]
//public class XAttributeDrawer : PropertyDrawer 
//{
//	#region Members
// 
//    private static Type _editorType = null;
//    private static MethodInfo _layerMaskFieldMethod = null;
//    private Type _fieldType = null;
//    private GUIContent _label = null;
//    private GUIContent _oldlabel = null;
//    //private SerializedProperty currentProperty = null;
// 
// 
//    #endregion
// 
//    #region Exposed
// 
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent oldLabel)
//    {
//        _oldlabel = oldLabel;
// 
//        EditorGUI.BeginProperty (position, label, property);
//        EditorGUI.BeginChangeCheck ();
// 
//        switch(property.propertyType)
//        {
//        case SerializedPropertyType.AnimationCurve:
//            AnimationCurve newAnimationCurveValue = EditorGUI.CurveField(position, label, property.animationCurveValue);
//            if(EditorGUI.EndChangeCheck()) property.animationCurveValue = newAnimationCurveValue;
//            break;
//        case SerializedPropertyType.Boolean:
//            bool newBoolValue = EditorGUI.Toggle(position, label, property.boolValue);
//            if(EditorGUI.EndChangeCheck()) property.boolValue = newBoolValue;
//            break;
//        case SerializedPropertyType.Bounds:
//            Bounds newBoundsValue = EditorGUI.BoundsField(position, label, property.boundsValue);
//            if(EditorGUI.EndChangeCheck()) property.boundsValue = newBoundsValue;
//            break;
//        case SerializedPropertyType.Color:
//            Color newColorValue = EditorGUI.ColorField(position, label, property.colorValue);
//            if(EditorGUI.EndChangeCheck()) property.colorValue = newColorValue;
//            break;
//        case SerializedPropertyType.Enum:
//            int newEnumValueIndex = (int)(object)EditorGUI.EnumPopup(position, label, Enum.Parse(GetFieldType(property), property.enumNames[property.enumValueIndex]) as Enum);
//            if(EditorGUI.EndChangeCheck()) property.enumValueIndex = newEnumValueIndex;
//            break;
//        case SerializedPropertyType.Float:
//            float newFloatValue = EditorGUI.FloatField(position, label, property.floatValue);
//            if(EditorGUI.EndChangeCheck()) property.floatValue = newFloatValue;
//            break;
//        case SerializedPropertyType.Integer:
//            int newIntValue = EditorGUI.IntField(position, label, property.intValue);
//            if(EditorGUI.EndChangeCheck()) property.intValue = newIntValue;
//            break;
//        case SerializedPropertyType.LayerMask:
//            layerMaskFieldMethod.Invoke(property.intValue, new object[] { position, property, label });
//            break;
//        case SerializedPropertyType.ObjectReference:
//            UnityEngine.Object newObjectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, GetFieldType(property), true);
//            if(EditorGUI.EndChangeCheck()) property.objectReferenceValue = newObjectReferenceValue;
//            break;
//        case SerializedPropertyType.Rect:
//            Rect newRectValue = EditorGUI.RectField(position, label, property.rectValue);
//            if(EditorGUI.EndChangeCheck()) property.rectValue = newRectValue;
//            break;
//        case SerializedPropertyType.String:
//            string newStringValue = EditorGUI.TextField(position, label, property.stringValue);
//            if(EditorGUI.EndChangeCheck()) property.stringValue = newStringValue;
//            break;
//        default:
//            Logger.LogWarning("ToolTipDrawer: found an un-handled type: " + property.propertyType);
//            break;
//        }
// 
//        EditorGUI.EndProperty ();
//    }
// 
//    #endregion
// 
//    #region Internal
//    private static Type editorType
//    {
//        get
//        {
//            if(_editorType == null)
//            {
//                Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.EditorGUI));
//                _editorType = assembly.GetType("UnityEditor.EditorGUI");
//                if(_editorType == null)
//                {
//                    Logger.LogWarning("ToolTipDrawer: Failed to open source file of EditorGUI");
//                }
//            }
//            return _editorType;
//        }
//    }
// 
//    private static MethodInfo layerMaskFieldMethod
//    {
//        get
//        {
//            if(_layerMaskFieldMethod == null)
//            {
//                Type[] typeDecleration = new Type[] {typeof(Rect), typeof(SerializedProperty), typeof(GUIContent)};
//                _layerMaskFieldMethod = editorType.GetMethod("LayerMaskField", BindingFlags.NonPublic | BindingFlags.Static,
//                                                             Type.DefaultBinder, typeDecleration, null);
//                if(_layerMaskFieldMethod == null)
//                {
//                    Logger.LogError("ToolTipDrawer: Failed to locate the internal LayerMaskField method.");
//                }
//            }
//            return _layerMaskFieldMethod;
//        }
//    }
// 
//    private GUIContent label
//    {
//        get
//        {
//            if(_label == null)
//            {
//                XAttribute labelAttribute = attribute as XAttribute;
//                _label = new GUIContent(_oldlabel.text, labelAttribute.tooltip);
//            }
// 
// 
//            return _label;
//        }
//    }
// 
//    private Type GetFieldType(SerializedProperty property)
//    {
//        if(_fieldType == null)
//        {
//            Type parentClassType = property.serializedObject.targetObject.GetType();
//            FieldInfo fieldInfo = parentClassType.GetField(property.name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
// 
//            if(fieldInfo == null)
//            {
//                Logger.LogError("ToolTipDrawer: Could not locate the object in the parent class");
//                return null;
//            }
//            _fieldType = fieldInfo.FieldType;
//        }
//        return _fieldType;
//    }
// 
//    #endregion
   
//}