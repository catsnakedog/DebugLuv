using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using System.Text;
using System.IO;
using UnityEngine.Networking;


public class NetworkManager : ManagerSingle<NetworkManager>
{
    public string LineData { get; set; }
    public string folderPath { get; set; }


    /// <summary>
    /// 구글 스프레드 시트 를 받아와 가공하는 함수
    /// </summary>
    /// <param name="googleSheetsID"> 구글 스프레드 시트의 ID </param>
    /// <param name="ManufactureData"> 스프레드 시트 가공 함수 input은 한 줄 을 받아야함, Cell은 탭('\t')으로 구분</param>
    /// <param name="workSheetsID"> 시트의 WorkSheet 구분 default 는 0 첫 장</param>
    /// <param name="startCell"> 시작 셀 default A1</param>
    /// <param name="endCell"> 끝 셀 default E </param>
    /// <returns></returns>
    public static IEnumerator GoogleSheetsDataParsing(string googleSheetsID = "1EjOBLPnuZwbcps5llCsQgZnj0Oel77A8enGVn7JTqjM" , string workSheetsID = "2121148207", string endCell = "CF469", string startCell = "B6", string textName = "LineData")
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("https://docs.google.com/spreadsheets/d/");
        sb.Append(googleSheetsID);
        sb.Append("/export?format=tsv");
        sb.Append("&gid=" + workSheetsID);
        sb.Append("&range=" + startCell + ":" + endCell);

        using (UnityWebRequest webData = UnityWebRequest.Get(sb.ToString()))
        {
            yield return webData.SendWebRequest();

            if (webData.isNetworkError || webData.isHttpError)
            {
                Instance.LineData = "네트워크 문제 발견 인터넷환경 또는 URL 확인 바람";
                Util.DebugLog("네트워크 문제 발견 인터넷환경 또는 URL 확인 바람");
                Instance.folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
            else
            {
                Instance.LineData = webData.downloadHandler.text;
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string appFolder = Path.Combine(folderPath, "DebugLuv"); // 'YourAppName'을 애플리케이션의 이름으로 변경하세요.
                Instance.folderPath = Path.Combine(appFolder, "LineData.txt");

                // 애플리케이션 폴더가 없으면 생성
                if (!Directory.Exists(appFolder))
                {
                    Directory.CreateDirectory(appFolder);
                }

                // 로그 파일 쓰기
                File.WriteAllText(Instance.folderPath, webData.downloadHandler.text);

                DataManager.ParsingDebugLuvData(Instance.folderPath, typeof(LineData));
            }
        }
        yield return null;
    }

}
