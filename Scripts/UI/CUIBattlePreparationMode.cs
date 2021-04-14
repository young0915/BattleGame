using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CUIBattlePreparationMode : CBaseUI
{
    [SerializeField] private Text ins_txtStart;
    [SerializeField] private Text ins_txtStriker;
    [SerializeField] private Text ins_txtPenetration;
    [SerializeField] private Text ins_txtRecovery;
    [SerializeField] private List<Image> _listImgHeroProfile;
    [SerializeField] private List<Image> _listImgMonProfile;

    [Space]
    [SerializeField] private CUIBattleHeroScrollView ins_cUIBattleHeroScrollView;

    private string _strBattleMonsterPath = string.Empty;
    private string _strProfilePath = string.Empty;
    private string _strBattleScene = string.Empty;
    private bool _bClickLock = false;

    [SerializeField] private int _nStriker = 0;
    [SerializeField] private int _nPenetration = 0;
    [SerializeField] private int _nRecovery = 0;

    public void Initialization(string strBattleScene)
    {
        SetTextInfo();

        this._strBattleScene = strBattleScene;

        for (int i = 0; i < _listImgHeroProfile.Count; i++)
        {

            _listImgHeroProfile[i].gameObject.SetActive(false);

        }
       
        StartCoroutine(ins_cUIBattleHeroScrollView.CorMakeDataModel());
        SetMonPreParation(_strBattleScene);
    }


    protected override void SetTextInfo()
    {
        base.SetTextInfo();
        ins_txtStart.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 26).ToString();
        ins_txtStriker.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 27).ToString();
        ins_txtPenetration.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 28).ToString();
        ins_txtRecovery.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 29).ToString();
    }


    private void SetMonPreParation(string strBattleScene)
    {

        _strBattleMonsterPath = CDataManager.Inst.m_strMonProfilePath;
        switch (strBattleScene)
        {
            case "1":
                _listImgMonProfile[0].sprite = CResourceLoader.Load<Sprite>(_strBattleMonsterPath + "Beholder");
                _listImgMonProfile[1].sprite = CResourceLoader.Load<Sprite>(_strBattleMonsterPath + "CrabMonster");
                _listImgMonProfile[2].sprite = CResourceLoader.Load<Sprite>(_strBattleMonsterPath + "RatAssassin");
                _listImgMonProfile[3].sprite = CResourceLoader.Load<Sprite>(_strBattleMonsterPath + "Specter");

                break;

            case "2":

                _listImgMonProfile[0].sprite = CResourceLoader.Load<Sprite>(_strBattleMonsterPath + "BlackKnight");
                _listImgMonProfile[1].sprite = CResourceLoader.Load<Sprite>(_strBattleMonsterPath + "WormMonster");
                _listImgMonProfile[2].sprite = CResourceLoader.Load<Sprite>(_strBattleMonsterPath + "FlyingDemon");
                _listImgMonProfile[3].sprite = CResourceLoader.Load<Sprite>(_strBattleMonsterPath + "Werewolf");

                break;
        }

    }



    public void OnClickStart()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        for(int i =0; i<_listImgHeroProfile.Count; i++)
        {
            if(_listImgHeroProfile[i].gameObject.activeSelf ==false)
            {
                StartCoroutine(CUIManager.Inst.CorAction(EmUIActionType.HeroLack));
                _bClickLock = false;
            }
            else if(_listImgHeroProfile[0].gameObject.activeSelf && _listImgHeroProfile[1].gameObject.activeSelf && _listImgHeroProfile[2].gameObject.activeSelf)
            {
                CSceneManager.Inst.OnSceneMovement(_strBattleScene, _nStriker, _nPenetration, _nRecovery);
                _bClickLock = false;

                Close();
            }
           
        }
        // 밑에꺼 지워주기.
    

    }

 



    public void SetHero(EmHeroType eHeroType, int nId)
    {
        _strProfilePath = CDataManager.Inst.m_strProfilePath + CDBPlayerInfo.Inst.m_listHeroInfo[nId].m_strProfile;


        switch (eHeroType)
        {
            case EmHeroType.Striker:

                this._nStriker = nId;

                _listImgHeroProfile[0].gameObject.SetActive(true);
                _listImgHeroProfile[0].sprite = CResourceLoader.Load<Sprite>(_strProfilePath);


                break;

            case EmHeroType.Penetration:
                this._nPenetration = nId;

                _listImgHeroProfile[1].gameObject.SetActive(true);
                _listImgHeroProfile[1].sprite = CResourceLoader.Load<Sprite>(_strProfilePath);

                break;

            case EmHeroType.Recovery:
                this._nRecovery = nId;

                _listImgHeroProfile[2].gameObject.SetActive(true);
                _listImgHeroProfile[2].sprite = CResourceLoader.Load<Sprite>(_strProfilePath);

                break;
       }
    }


    public void OnClickHeroCancel(int nProfile)
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        _listImgHeroProfile[nProfile].gameObject.SetActive(false);


        _bClickLock = false;
    }


    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestory = true)
    {
        base.Close(bDestory);
    }
}
