using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneManagerEX : ManagerSingle<SceneManagerEX> // Scene과 관련된 내용을 관리하는 Manager이다
{
    public BaseScene CurrentScene { get{ return GameObject.FindObjectOfType<BaseScene>();}} // 현재 BaseScene을 반환한다

    string _nextScene; // Loading후 무슨 씬을 로드해야하는지 저장한다

    public void LoadScene(string name) // 초기화 후 씬 이동
    {
        Managers.ClearAll();
        SceneManager.LoadScene(name);
    }
}
