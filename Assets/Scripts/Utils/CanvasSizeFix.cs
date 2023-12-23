using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSizeFix : MonoBehaviour // Canvas�� �ػ󵵸� �������ش�
{
    void Start()
    {
        float setWidth = 2400; // ����� ���� �ʺ�
        float setHeight = 1080; // ����� ���� ����

        float deviceWidth = Screen.width; // ��� �ʺ� ����
        float deviceHeight = Screen.height; // ��� ���� ����

        float rate = (setHeight / setWidth);

        if (rate * deviceWidth > deviceHeight)// ���ΰ� �� �� ��Ȳ (���ο� �������)
        {
            this.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
        else // ���ΰ� �� �� ��Ȳ (���ο� �������)
        {
            this.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }
    }
}
