using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour // Scene�� ���� �⺻�� �Ǵ� class�̴�
{
    public Define.Scene SceneType{get; protected set;} = Define.Scene.Unknown; // � ���ΰ�
    [HideInInspector] public UI_Base SceneUI; // �ش� ���� SceneUI�̴�

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj =  GameObject.FindObjectOfType(typeof(EventSystem));
        if(obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem"; // EventSystem�� �߰��Ѵ�
        SetResolution();
    }

    public void SetResolution() // �ػ� ������ ���ش�
    {
        int setWidth = 2400; // ����� ���� �ʺ�
        int setHeight = 1080; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }

    public abstract void Clear();
}