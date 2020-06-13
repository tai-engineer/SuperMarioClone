using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedFloat), true)]
public class RangedFloatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        // Find min max properties
        SerializedProperty minProp = property.FindPropertyRelative("minValue");
        SerializedProperty maxProp = property.FindPropertyRelative("maxValue");

        float minValue = minProp.floatValue;
        float maxValue = maxProp.floatValue;

        float rangeMin = 0f;
        float rangeMax = 1f;

        // Get min max range from custom attribute
        var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof(MinMaxRangeAttribute), true);
        if(ranges.Length > 0)
        {
            rangeMin = ranges[0].Min;
            rangeMax = ranges[0].Max;
        }

        // Define label width
        const float rangeBoundsLabelWidth = 40f;

        // Identify label positions and assign GUI content
        var label1 = new Rect(position);
        label1.width = rangeBoundsLabelWidth;
        GUI.Label(label1, new GUIContent(minValue.ToString("F2")));
        position.xMin += rangeBoundsLabelWidth;

        var label2 = new Rect(position);
        label2.xMin = position.xMax - rangeBoundsLabelWidth;
        GUI.Label(label2, new GUIContent(maxValue.ToString("F2")));
        position.xMax -= rangeBoundsLabelWidth;

        // Get value from MinMaxSlider and apply changes
        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
        if(GUI.changed)
        {
            minProp.floatValue = minValue;
            maxProp.floatValue = maxValue;
        }
        EditorGUI.EndChangeCheck();
    }
}