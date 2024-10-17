using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class CustomHierarchyHighlighter
{
    static CustomHierarchyHighlighter()
    {
        // 하이어라키에서 GUI를 커스터마이징
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj != null)
        {
            // HighlightObject 컴포넌트가 추가된 오브젝트만 강조
            HighlightObject highlight = obj.GetComponent<HighlightObject>();
            if (highlight != null)
            {
                // 배경 색상 설정
                EditorGUI.DrawRect(selectionRect, highlight.highlightColor);

                // 텍스트 중앙 정렬 여부에 따라 설정
                GUIStyle style = new GUIStyle(GUI.skin.label)
                {
                    alignment = highlight.centerText ? TextAnchor.MiddleCenter : TextAnchor.MiddleLeft, // 중앙 정렬 여부
                    normal = new GUIStyleState() { textColor = highlight.textColor }
                };

                // 텍스트를 중앙 또는 왼쪽에 배치
                Rect labelRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.height);
                EditorGUI.LabelField(labelRect, obj.name, style);
            }
        }
    }
}
