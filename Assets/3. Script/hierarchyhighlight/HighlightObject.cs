using UnityEditor;
using UnityEngine;

[ExecuteInEditMode] // 에디터 모드에서도 스크립트가 실행되도록 설정
public class HighlightObject : MonoBehaviour
{
    public Color highlightColor = Color.yellow; // 기본 배경 색상
    public Color textColor = Color.red;         // 기본 텍스트 색상
    public bool centerText = true;               // 텍스트 중앙 정렬 여부

    // OnValidate는 값이 변경될 때마다 호출됨
    void OnValidate()
    {
        // 변경된 색상이 하이어라키에 즉시 반영되도록 함
        EditorApplication.RepaintHierarchyWindow(); // 하이어라키 창 새로고침
    }
}
