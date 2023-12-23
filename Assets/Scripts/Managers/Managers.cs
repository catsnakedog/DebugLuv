using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour // 필요한 각종 기능들을 모아두는 클래스이다
{
    static Managers s_instance; // 정적 Managers
    static Managers instance {get{Init(); return s_instance;}} // 각각의 Manager가 private한 s_instance에 접근하기 위한 정적 Managers의 프로퍼티

    ResourceManager _resource = new ResourceManager(); // Resource와 관련된 부분을 관리한다
    UI_Manager _ui_manager = new UI_Manager(); // UI와 관련된 부분을 관리한다
    SceneManagerEX _scene = new SceneManagerEX(); // Scene과 관련된 부분을 관리한다
    SoundManager _sound = new SoundManager(); // Sound와 관련된 부분을 관리한다
    DataManager _data = new DataManager(); // Data와 관련된 부분을 관리한다
    public static ResourceManager Resource{get{return instance._resource;}} // _resource의 프로퍼티 ( 정적 접근을 위해 static으로 선언해준다 )
    public static UI_Manager UI_Manager{get{return instance._ui_manager;}} // _ui_manager의 프로퍼티
    public static SceneManagerEX Scene{get{return instance._scene;}} // _scene의 프로퍼티
    public static SoundManager Sound{get{return instance._sound;}} // _sound이 프로퍼티
    public static DataManager DataManager { get {  return instance._data;}} // _data의 프로퍼티

    // 전부 프로퍼티로 접근하게 한 이유는 직접적인 접근을 막아 안전성을 높히기 위해서다
    
    void Start()
    {
        Init();
    }

    static void Init(){
        if(s_instance == null) // 싱글톤 패턴을 통해 s_instance의 유일성을 보장한다
        {
            GameObject go = GameObject.Find("@Managers");
            if(go==null)
            {
                go = new GameObject{name = "@Managers"};
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init(); // 사운드 최초 세팅
            s_instance._data.Init(); // 데이터 최초 세팅

            // 나머지 Manager들은 Init을 하지 않는 이유는 최초 세팅을 해줄 필요가 없기 때문이다
        }
    }

    public static void Clear() // Manager들을 초기화 해준다
    {
        Sound.Clear();
        Scene.Clear();
        UI_Manager.Clear();
    }
}
