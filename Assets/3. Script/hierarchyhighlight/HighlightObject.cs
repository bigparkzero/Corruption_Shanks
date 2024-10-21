using UnityEditor;
using UnityEngine;

[ExecuteInEditMode] // ������ ��忡���� ��ũ��Ʈ�� ����ǵ��� ����
public class HighlightObject : MonoBehaviour
{
    public Color highlightColor = Color.yellow; // �⺻ ��� ����
    public Color textColor = Color.red;         // �⺻ �ؽ�Ʈ ����
    public bool centerText = true;               // �ؽ�Ʈ �߾� ���� ����
    public int fontSize = 12;                    // �ؽ�Ʈ ũ��
    public bool bold = false;                    // ���� ���� ����

    // OnValidate�� ���� ����� ������ ȣ���
    void OnValidate()
    {
        // ����� ������ ���̾��Ű�� ��� �ݿ��ǵ��� ��
        EditorApplication.RepaintHierarchyWindow(); // ���̾��Ű â ���ΰ�ħ
    }
}
