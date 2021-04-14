using UnityEngine;
using UnityEngine.UI;

public class CUIBattleHeroCell : MonoBehaviour
{

    [SerializeField] private Image ins_ImgDisable;
    [SerializeField] private Image ins_ImgHeroProfile;
    [SerializeField] private Text ins_txtHeroName;

    private int _nId = 0;
    private string _strHeroName = string.Empty;
    private string _strHeroProfilePath = string.Empty;

    private bool _bClickLock = false;

    public void SetData(CBattleModel cModel)
    {
        this._nId = cModel.m_nId;

        this._strHeroName = CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_strName.ToString();
        this._strHeroProfilePath = CDataManager.Inst.m_strProfilePath + CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_strProfile.ToString();

        // 중복 방지.


        if (CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_eHeroType != EmHeroType.None && cModel.m_eCellState == EmCellState.NotComplete)
        {
            ins_ImgDisable.gameObject.SetActive(false);
        }
        ins_txtHeroName.text = _strHeroName;
        ins_ImgHeroProfile.sprite = CResourceLoader.Load<Sprite>(_strHeroProfilePath);

    }

    public void OnClickGoBattle()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        CUIManager.Inst.m_cUIBattlePreparationMode.SetHero(CDBPlayerInfo.Inst.m_listHeroInfo[_nId].m_eHeroType, _nId);

        _bClickLock = false;
    }

}
