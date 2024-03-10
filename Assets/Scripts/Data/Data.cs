using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    static Data s_instance;

    public GameData VisibleGameData; // 보여주기용 데이터, 여기있는 데이터로 몬가 하지는 않는다 ( 빌드할때는 제거!!! )
    public static GameData GameData; // 정적으로 선언한 변수는 Inspector창에서 보이지 않는다
                                     // 따라서 pulbic으로 선언한 VisibleGameData 변수를 만들어서 에디터상에서 시각적으로 데이터 확인을 가능하게 만든다


    void Start()
    {
        s_instance = gameObject.GetComponent<Data>();

        Init();
    }

    static void Init()
    {
        s_instance.VisibleGameData = Data.GameData; // VisibleGameData와 GameData를 연결한다
    }
}