using System;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CQuestManager : CSingleton<CQuestManager>
{

    [SerializeField] private List<CQuestInfo> _listQuestInfo = new List<CQuestInfo>();
    public List<CQuestInfo> m_listQuestInfo { get { return _listQuestInfo; } }

    private Dictionary<int, CQuestInfo> _dicQuest = new Dictionary<int, CQuestInfo>();

    public void Install()
    {
        PhashingLevelData();
    }

    private void PhashingLevelData()
    {
        string strPath = "Texts/QuestData";
        var Json = CResourceLoader.Load<TextAsset>(strPath).text;
        var ArrQuest = JsonConvert.DeserializeObject<CQuestInfo[]>(Json);

        foreach (var data in ArrQuest)
        {
            _dicQuest.Add(data.m_nId, data);
            _listQuestInfo.Add(new CQuestInfo(data.m_nId, data.m_strTitle, data.m_strContent, data.m_strImgPath, data.m_nCoin));
        }
    }
}
