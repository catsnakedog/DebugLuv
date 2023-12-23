using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneManagerEX // Scene과 관련된 내용을 관리하는 Manager이다
{
    public BaseScene CurrentScene { get{ return GameObject.FindObjectOfType<BaseScene>();}} // 현재 BaseScene을 반환한다

    string _nextScene; // Loading후 무슨 씬을 로드해야하는지 저장한다

    public void LoadScene(Define.Scene type) // Enum으로 씬 호출
    {
        Managers.Clear();
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    public void LoadScene(string name) // String으로 씬 호출
    {
        Managers.Clear();
        CurrentScene.Clear();
        SceneManager.LoadScene(name);
    }
    public void LoadingAndLoadScene(Define.Scene type) // Loading씬을 거쳐서 씬 호출
    {
        Managers.Clear();
        CurrentScene.Clear();
        _nextScene = GetSceneName(type);
        SceneManager.LoadScene("Loading");
    }
    public void FadeEffectAndLoadScene(Define.Scene type) // Fade In/Out 효과를 거쳐서 씬 호출
    {
        GameObject fade = Managers.Resource.Instantiate("UI/Obj/FadeCanvas");
        UnityEngine.GameObject.DontDestroyOnLoad(fade);
        CurrentScene.Clear();
        fade.GetComponent<UI_Fade>().FadeEffect(GetSceneName(type));
    }

    public string GetNextSceneName() // _nextScene을 반환해준다
    {
        if (_nextScene == null)
            return "Error";

        return _nextScene;
    }

    string GetSceneName(Define.Scene type) // 현재 씬의 이름을 반환해준다
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear() // 초기화
    {
        CurrentScene.Clear();
    }
}
