using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUp : GimmickOutput
{
    [Header("Ʃ�丮�� �˾��� �� �̹��� \ninputgimmick�� event ���� SetImage()�� �����Ͻ� �� �ֽ��ϴ�")]
    public Sprite image;
    [Header("Ʃ�丮�� �˾��� �� �ؽ�Ʈ \ninputgimmick�� event ���� SetString()�� �����Ͻ� �� �ֽ��ϴ�")]
    [Multiline(4)]
    public string info;
    private void Start()
    {
        isDone = false;
    }
    public void open()
    {
        TutorialUi.Instance.OpenPopUp(info, image);
        isDone = true;
    }
    public void close()
    {
        TutorialUi.Instance.CloesPopUp();
        isDone = true;
    }
    public void SetImage(Sprite sprite)
    {
        image = sprite;
    }
    public void SetString(string egsw)
    {
        info = egsw;
    }
}