using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    static Data s_instance;

    public GameData VisibleGameData; // �����ֱ�� ������, �����ִ� �����ͷ� �� ������ �ʴ´� ( �����Ҷ��� ����!!! )
    public static GameData GameData; // �������� ������ ������ Inspectorâ���� ������ �ʴ´�
                                     // ���� pulbic���� ������ VisibleGameData ������ ���� �����ͻ󿡼� �ð������� ������ Ȯ���� �����ϰ� �����


    void Start()
    {
        s_instance = gameObject.GetComponent<Data>();

        Init();
    }

    static void Init()
    {
        s_instance.VisibleGameData = Data.GameData; // VisibleGameData�� GameData�� �����Ѵ�
    }
}