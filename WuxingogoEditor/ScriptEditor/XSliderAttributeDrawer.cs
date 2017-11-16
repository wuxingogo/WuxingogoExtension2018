namespace wuxingogo.Attribute
{
    using UnityEngine;
    using UnityEditor;
    using wuxingogo.Runtime;
    /// <summary>
    /// https://forum.unity.com/threads/how-to-write-a-custom-rangeattribute.437561/
    /// </summary>
    [CustomPropertyDrawer(typeof(XSliderAttribute))]
    public class XSliderAttributeDrawer : PropertyDrawer
    {
 
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
 
            // First get the attribute since it contains the range for the slider
            XSliderAttribute range = attribute as XSliderAttribute;
 
            // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
            if (property.propertyType == SerializedPropertyType.Float)
                EditorGUI.Slider(position, property, range.MinValue, range.MaxValue, label);
            else if (property.propertyType == SerializedPropertyType.Integer)
         
                EditorGUI.IntSlider(position, property, (int)range.MinValue, (int)range.MaxValue, label);
            else
                EditorGUI.LabelField(position, label.text, "Use Range with float or int.");
        }
    }
}