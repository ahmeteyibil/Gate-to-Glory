using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Font))]
public class FontPreviewInspector : Editor
{
    private string previewText = "AaBbCcDd 0123";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Font font = (Font)target;
        if (font.material == null || font.material.mainTexture == null) return;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Font Preview", EditorStyles.boldLabel);

        previewText = EditorGUILayout.TextField("Preview Text", previewText);

        GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            font = font,
            fontSize = 24,
            wordWrap = true,
            normal = { textColor = Color.white }
        };

        EditorGUILayout.LabelField(previewText, style, GUILayout.Height(60));
    }
}
