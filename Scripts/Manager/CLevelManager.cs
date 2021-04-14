using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CLevelManager : CSingleton<CLevelManager>
{
    private Dictionary<int, CLevelInfo> _dicLevelInfo = new Dictionary<int, CLevelInfo>();
    private List<CLevelInfo> _listLevel = new List<CLevelInfo>();
    public List<CLevelInfo> m_listLevel { get { return _listLevel; } }
    public void Install()
    {
        PhashingLevelData();
    }

    private void PhashingLevelData()
    {
        string strPath = "Texts/LevelData";

        var Json = CResourceLoader.Load<TextAsset>(strPath).text;
        var ArrLevel = JsonConvert.DeserializeObject<CLevelInfo[]>(Json);

        foreach (var data in ArrLevel)
        {
            _dicLevelInfo.Add(data.m_nId, data);
            _listLevel.Add(new CLevelInfo(
                data.m_nId,
                data.m_nLv,
                data.m_nHp,
                data.m_nAttack,
                data.m_nCritical));
        }
    }


}
