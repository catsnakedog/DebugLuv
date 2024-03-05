using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeFix : MonoBehaviour
{
    private int _beforeWidth;
    private int _beforeHeight;

    void Start()
    {
        _beforeWidth = Screen.width;
        _beforeHeight = Screen.height;
        SetResolution();
    }

    void SetResolution()
    {
        int setWidth = 1920; // ����� ���� �ʺ�
        int setHeight = 1080; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����
        _beforeWidth = deviceWidth;
        _beforeHeight = deviceHeight;

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

    void Update()
    {
        if (_beforeWidth != Screen.width || _beforeHeight != Screen.height)
            SetResolution();
        _beforeWidth = Screen.width;
        _beforeHeight = Screen.height;
    }
}
