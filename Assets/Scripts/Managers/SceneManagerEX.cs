using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneManagerEX : ManagerSingle<SceneManagerEX> // Scene�� ���õ� ������ �����ϴ� Manager�̴�
{
    public BaseScene CurrentScene { get{ return GameObject.FindObjectOfType<BaseScene>();}} // ���� BaseScene�� ��ȯ�Ѵ�

    string _nextScene; // Loading�� ���� ���� �ε��ؾ��ϴ��� �����Ѵ�

    public void LoadScene(string name) // �ʱ�ȭ �� �� �̵�
    {
        Managers.ClearAll();
        SceneManager.LoadScene(name);
    }
}
