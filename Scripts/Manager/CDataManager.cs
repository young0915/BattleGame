using UnityEngine;
using System.Collections.Generic;

public class CDataManager : CSingleton<CDataManager>
{

    private Dictionary<int, string> _dicDataText;
    public const string m_strGameDataInfo = "GameDataInfo";
    private const string _strMissing = "Excel text not found";

    public readonly string m_strProfilePath = "Texture/Hero/";
    public readonly string m_strTowerProfilePath = "Texture/Tower/";
    public readonly string m_strItemProfilePath = "Texture/Item/";
    public readonly string m_strMonProfilePath = "Texture/Mon/";
    public const string m_strOne = "1";
    public const string m_strTwo = "2";

    // 데이터 입력, 파일 이름과, 키값(id) 출력.
    public string GetDataValue(string strfileName, int nKey)
    {
        _dicDataText = new Dictionary<int, string>();
        TextAsset mytxtData = CResourceLoader.Load<TextAsset>("Texts/" + strfileName);

        string strTxt = mytxtData.text;
        if (strTxt != "" && strTxt != null)
        {
            string strDataAsJson = strTxt;
            CExcelData LoadedData = JsonUtility.FromJson<CExcelData>(strDataAsJson);

            for (int i = 0; i < LoadedData.m_items.Length; i++)
            {
                if (!_dicDataText.ContainsKey(i))
                {
                    _dicDataText.Add(i, LoadedData.m_items[i].m_strValue);
                }
            }
        }
#if LogError
        else
        {
            Debug.LogError("Cannot find file!");
        }
#endif

        string strResult = _strMissing;
        if (_dicDataText.ContainsKey(nKey - 1))
        {
            strResult = _dicDataText[nKey - 1].Replace("\\n", "\n");
        }
        return strResult;
    }

}
