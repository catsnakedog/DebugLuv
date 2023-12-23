using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour // �ʿ��� ���� ��ɵ��� ��Ƶδ� Ŭ�����̴�
{
    static Managers s_instance; // ���� Managers
    static Managers instance {get{Init(); return s_instance;}} // ������ Manager�� private�� s_instance�� �����ϱ� ���� ���� Managers�� ������Ƽ

    ResourceManager _resource = new ResourceManager(); // Resource�� ���õ� �κ��� �����Ѵ�
    UI_Manager _ui_manager = new UI_Manager(); // UI�� ���õ� �κ��� �����Ѵ�
    SceneManagerEX _scene = new SceneManagerEX(); // Scene�� ���õ� �κ��� �����Ѵ�
    SoundManager _sound = new SoundManager(); // Sound�� ���õ� �κ��� �����Ѵ�
    DataManager _data = new DataManager(); // Data�� ���õ� �κ��� �����Ѵ�
    public static ResourceManager Resource{get{return instance._resource;}} // _resource�� ������Ƽ ( ���� ������ ���� static���� �������ش� )
    public static UI_Manager UI_Manager{get{return instance._ui_manager;}} // _ui_manager�� ������Ƽ
    public static SceneManagerEX Scene{get{return instance._scene;}} // _scene�� ������Ƽ
    public static SoundManager Sound{get{return instance._sound;}} // _sound�� ������Ƽ
    public static DataManager DataManager { get {  return instance._data;}} // _data�� ������Ƽ

    // ���� ������Ƽ�� �����ϰ� �� ������ �������� ������ ���� �������� ������ ���ؼ���
    
    void Start()
    {
        Init();
    }

    static void Init(){
        if(s_instance == null) // �̱��� ������ ���� s_instance�� ���ϼ��� �����Ѵ�
        {
            GameObject go = GameObject.Find("@Managers");
            if(go==null)
            {
                go = new GameObject{name = "@Managers"};
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init(); // ���� ���� ����
            s_instance._data.Init(); // ������ ���� ����

            // ������ Manager���� Init�� ���� �ʴ� ������ ���� ������ ���� �ʿ䰡 ���� �����̴�
        }
    }

    public static void Clear() // Manager���� �ʱ�ȭ ���ش�
    {
        Sound.Clear();
        Scene.Clear();
        UI_Manager.Clear();
    }
}
