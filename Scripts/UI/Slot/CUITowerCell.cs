using UnityEngine;
using UnityEngine.UI;

public class CUITowerCell : MonoBehaviour
{
    [SerializeField] private Image ins_imgTower;
    [SerializeField] private Text ins_txtTowerName;

    private string _strTowerName = string.Empty;
    private string _strSpritePath = string.Empty;
    private int _nId = 0;

    public void SetData(CTowerModel model)
    {
        _nId = model.m_nId;

        _strTowerName = CDBPlayerInfo.Inst.m_listTowerInfo[_nId].m_strName.ToString();
        _strSpritePath = CDataManager.Inst.m_strTowerProfilePath + CDBPlayerInfo.Inst.m_listTowerInfo[_nId].m_strProfile.ToString();

      
        ins_txtTowerName.text = _strTowerName;
        ins_imgTower.sprite = CResourceLoader.Load<Sprite>(_strSpritePath);
    }
}
