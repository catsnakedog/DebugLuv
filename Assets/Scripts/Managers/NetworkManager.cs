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
    /// ���� �������� ��Ʈ �� �޾ƿ� �����ϴ� �Լ�
    /// </summary>
    /// <param name="googleSheetsID"> ���� �������� ��Ʈ�� ID </param>
    /// <param name="ManufactureData"> �������� ��Ʈ ���� �Լ� input�� �� �� �� �޾ƾ���, Cell�� ��('\t')���� ����</param>
    /// <param name="workSheetsID"> ��Ʈ�� WorkSheet ���� default �� 0 ù ��</param>
    /// <param name="startCell"> ���� �� default A1</param>
    /// <param name="endCell"> �� �� default E </param>
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
                Instance.LineData = "��Ʈ��ũ ���� �߰� ���ͳ�ȯ�� �Ǵ� URL Ȯ�� �ٶ�";
                Util.DebugLog("��Ʈ��ũ ���� �߰� ���ͳ�ȯ�� �Ǵ� URL Ȯ�� �ٶ�");
                Instance.folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
            else
            {
                Instance.LineData = webData.downloadHandler.text;
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string appFolder = Path.Combine(folderPath, "DebugLuv"); // 'YourAppName'�� ���ø����̼��� �̸����� �����ϼ���.
                Instance.folderPath = Path.Combine(appFolder, "LineData.txt");

                // ���ø����̼� ������ ������ ����
                if (!Directory.Exists(appFolder))
                {
                    Directory.CreateDirectory(appFolder);
                }

                // �α� ���� ����
                File.WriteAllText(Instance.folderPath, webData.downloadHandler.text);

                DataManager.ParsingDebugLuvData(Instance.folderPath, typeof(LineData));
            }
        }
        yield return null;
    }

}
