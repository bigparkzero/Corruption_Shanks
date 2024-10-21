using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class GimmickTrigger : MonoBehaviour
{
    [Header("üũ�ϸ� �Լ� ������ �ڱ��ڽ� �ı�")]
    public bool InvokeEventEndDestroy;
    [Header("üũ�ϸ� �Լ� ������ �ڱ��ڽ� ��Ȱ��ȭ")]
    public bool InvokeEventEndDeactivate;

    public void test(string text)
    {
        Debug.Log(text);
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public void DestroyObject(GameObject target)
    {
        Destroy(target);
    }

    [System.Serializable]
    public class SimpleEvent : UnityEvent
    {
    }

    public List<SimpleEvent> OutputEvent;

    public bool triggerMultipleTimes;

    public void InvokeEvent()
    {
        if (triggerMultipleTimes)
        {
            for (int i = 0; i < OutputEvent.Count; i++)
            {
                for (int j = 0; j < OutputEvent[i].GetPersistentEventCount(); j++)
                {
                    Object targetObj = OutputEvent[i].GetPersistentTarget(j);
                    GimmickOutput associatedOutput = targetObj.GetComponent<GimmickOutput>();
                    if (associatedOutput != null)
                    {
                        associatedOutput.isDone = false; // isDone�� false�� ����
                    }
                }
            }
        }

        if (OutputEvent != null && OutputEvent.Count > 0)
        {
            StartCoroutine(InvokeEventsCoroutine());
        }
    }

    private bool isOutputGimmick;
    private bool isDoneAll;

    private IEnumerator InvokeEventsCoroutine()
    {
        // OutputEvent ����Ʈ�� ��ȸ�ϸ鼭 �ϳ��� ����
        for (int i = 0; i < OutputEvent.Count; i++)
        {
            // �̺�Ʈ ȣ��
            OutputEvent[i].Invoke();

            // GimmickOutput�� isDone�� false�� ����
            for (int j = 0; j < OutputEvent[i].GetPersistentEventCount(); j++)
            {
                Object targetObj = OutputEvent[i].GetPersistentTarget(j);
                GimmickOutput associatedOutput = targetObj.GetComponent<GimmickOutput>();
                if (associatedOutput != null)
                {
                    associatedOutput.isDone = false; // �̺�Ʈ ȣ�� �� isDone�� false�� ����
                }
            }

            isOutputGimmick = false; // Gimmick_Output�� �����ϴ��� üũ
            isDoneAll = false; // �Ϸ� ���� üũ

            // Gimmick_Output üũ
            for (int j = 0; j < OutputEvent[i].GetPersistentEventCount(); j++)
            {
                Object targetObj = OutputEvent[i].GetPersistentTarget(j);
                GimmickOutput associatedOutput = targetObj.GetComponent<GimmickOutput>();
                if (associatedOutput != null)
                {
                    isOutputGimmick = true; // Gimmick_Output�� ������
                }
            }

            if (isOutputGimmick)
            {
                // �ش� ������Ʈ�� isDone�� true�� �� ������ ���
                while (!isDoneAll)
                {
                    yield return null; // �� ������ ���
                    isDoneAll = true; // �⺻������ �Ϸ�� ������ ����
                    for (int j = 0; j < OutputEvent[i].GetPersistentEventCount(); j++)
                    {
                        Object targetObj = OutputEvent[i].GetPersistentTarget(j);
                        GimmickOutput associatedOutput = targetObj.GetComponent<GimmickOutput>();
                        if (associatedOutput != null && !associatedOutput.isDone)
                        {
                            isDoneAll = false; // �Ϸ���� ���� ��� false�� ����
                            break; // �� �̻� Ȯ���� �ʿ� ����
                        }
                    }

                    Debug.Log("<color=yellow>" + gameObject.name + "</color>" + "�� " + "<color=blue>" + i + "</color>" + " ��° �̺�Ʈ ��ٸ��� ��");
                }
            }

            // i + 1�� OutputEvent.Count�� �ʰ����� �ʵ��� Ȯ��
            if (i + 1 < OutputEvent.Count)
            {
                OutputEvent[i + 1].Invoke();
                Debug.Log("<color=yellow>" + gameObject.name + "</color>" + " �� " + "<color=blue>" + i + "</color>" + " ��° �̺�Ʈ ����");
            }
        }

        Debug.Log("<color=yellow>" + gameObject.name + "</color>" + "�� ��� �̺�Ʈ ���� �Ϸ�");
        if (InvokeEventEndDeactivate)
        {
            gameObject.SetActive(false);
            Debug.Log("<color=yellow>" + gameObject.name + "</color>" + " ��Ȱ��ȭ��");
        }
        if (InvokeEventEndDestroy)
        {
            Destroy(gameObject);
            Debug.Log("<color=yellow>" + gameObject.name + "</color>" + " ������");
        }
    }

    public bool testbool;
    [HideInInspector] public bool isTriggered;

    [Header("GimmickInput�� ������ �� �ֽ��ϴ�.\n �������� ���� ��� �θ� ������Ʈ�� GimmickInput�� �����մϴ�")]
    public GimmickInput GimmickInput;

    private void Start()
    {
        isTriggered = false;
    }

    public void InvokeEventRunOnTrigger()
    {
        if (GimmickInput != null)
        {
            isTriggered = true;
            InvokeEvent();
            GimmickInput.TriggerCheck();
        }
        else
        {
            if (OutputEvent != null)
            {
                InvokeEvent();
            }
            Debug.LogWarning("Gimmick Input script not found in parent object! Please add the Gimmick Input script to the parent object or place it as a child object of the object with Gimmick Input.");
        }
    }

    private void OnValidate()
    {
        GimmickInput GmmiInput = GetComponentInParent<GimmickInput>();
        if (GmmiInput != null)
        {
            GimmickInput = GmmiInput;
        }
        testbool = false;
    }
}
