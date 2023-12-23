using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneManagerEX // Scene�� ���õ� ������ �����ϴ� Manager�̴�
{
    public BaseScene CurrentScene { get{ return GameObject.FindObjectOfType<BaseScene>();}} // ���� BaseScene�� ��ȯ�Ѵ�

    string _nextScene; // Loading�� ���� ���� �ε��ؾ��ϴ��� �����Ѵ�

    public void LoadScene(Define.Scene type) // Enum���� �� ȣ��
    {
        Managers.Clear();
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    public void LoadScene(string name) // String���� �� ȣ��
    {
        Managers.Clear();
        CurrentScene.Clear();
        SceneManager.LoadScene(name);
    }
    public void LoadingAndLoadScene(Define.Scene type) // Loading���� ���ļ� �� ȣ��
    {
        Managers.Clear();
        CurrentScene.Clear();
        _nextScene = GetSceneName(type);
        SceneManager.LoadScene("Loading");
    }
    public void FadeEffectAndLoadScene(Define.Scene type) // Fade In/Out ȿ���� ���ļ� �� ȣ��
    {
        GameObject fade = Managers.Resource.Instantiate("UI/Obj/FadeCanvas");
        UnityEngine.GameObject.DontDestroyOnLoad(fade);
        CurrentScene.Clear();
        fade.GetComponent<UI_Fade>().FadeEffect(GetSceneName(type));
    }

    public string GetNextSceneName() // _nextScene�� ��ȯ���ش�
    {
        if (_nextScene == null)
            return "Error";

        return _nextScene;
    }

    string GetSceneName(Define.Scene type) // ���� ���� �̸��� ��ȯ���ش�
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear() // �ʱ�ȭ
    {
        CurrentScene.Clear();
    }
}
