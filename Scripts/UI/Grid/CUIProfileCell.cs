using UnityEngine;
using UnityEngine.UI;

public class CUIProfileCell : MonoBehaviour
{
    [SerializeField] private Image ins_ImgProfile;

    private string _strProfilePath = string.Empty;
    private int _nId = 0;
    private bool _bClickLock = false;

    public void SetData(CProfileGridModel cModel)
    {
        this._nId = cModel.m_nId;

        this._strProfilePath = CDataManager.Inst.m_strProfilePath + CDBManager.Inst.m_listHeroInfo[_nId].m_strProfile.ToString();

        ins_ImgProfile.sprite = CResourceLoader.Load<Sprite>(_strProfilePath);


    }


    public void OnClickProfile()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        CUIManager.Inst.m_cUILobby.SetProfile().sprite = CResourceLoader.Load<Sprite>(_strProfilePath);

        _bClickLock = false;
    }
}
