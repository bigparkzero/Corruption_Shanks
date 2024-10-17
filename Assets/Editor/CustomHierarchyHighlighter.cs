using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class CustomHierarchyHighlighter
{
    static CustomHierarchyHighlighter()
    {
        // ���̾��Ű���� GUI�� Ŀ���͸���¡
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj != null)
        {
            // HighlightObject ������Ʈ�� �߰��� ������Ʈ�� ����
            HighlightObject highlight = obj.GetComponent<HighlightObject>();
            if (highlight != null)
            {
                // ��� ���� ����
                EditorGUI.DrawRect(selectionRect, highlight.highlightColor);

                // �ؽ�Ʈ �߾� ���� ���ο� ���� ����
                GUIStyle style = new GUIStyle(GUI.skin.label)
                {
                    alignment = highlight.centerText ? TextAnchor.MiddleCenter : TextAnchor.MiddleLeft, // �߾� ���� ����
                    normal = new GUIStyleState() { textColor = highlight.textColor }
                };

                // �ؽ�Ʈ�� �߾� �Ǵ� ���ʿ� ��ġ
                Rect labelRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.height);
                EditorGUI.LabelField(labelRect, obj.name, style);
            }
        }
    }
}
