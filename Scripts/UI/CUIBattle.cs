using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CUIBattle : CBaseUI
{

    [SerializeField] private List<CHeroSlot> ins_listHeroSlot = new List<CHeroSlot>();
    public List<CHeroSlot> m_listHeroSlot { get { return ins_listHeroSlot; } }

    [SerializeField] private Text ins_txtMin;
    [SerializeField] private Text ins_txtQuotes;
    [SerializeField] private Text ins_txtSec;
    [SerializeField] private Text ins_txtMoney;
    [SerializeField] private Text ins_txtMonsterCnt;

    [Space]
    [SerializeField] private List<Image> ins_ImgHeart = new List<Image>();

    private string _strStriker = string.Empty;
    private string _strPenetration = string.Empty;
    private string _strRecovery = string.Empty;

    private float _fTimerStart = 0.0f;
    private int _nMin = 0;
    private int _nSec = 0;
    private int _nMonCnt = 0;
    private int _nHeartCnt = 0;
    private bool _bClickLock = false;


    public void Initialization(float fCoolTime, int nMonCnt)
    {
        SetTextInfo();
        this._fTimerStart = fCoolTime;
        StartCoroutine(CorTimer(this._fTimerStart));
        this._nMonCnt = nMonCnt;

        SetTextInfo();
        SetHero();

    }


    private IEnumerator CorTimer(float fCoolTime)
    {
        while (_fTimerStart > 0.0f)
        {
            _fTimerStart -= Time.deltaTime;
            _nMin = Mathf.RoundToInt(_fTimerStart) / 60;
            _nSec = Mathf.RoundToInt(_fTimerStart % 60);

            if (_nSec == 60)                                                                          //60초이면 0으로 수정할 것. 
            {
                _nSec = 0;
            }

            ins_txtMin.text = _nMin.ToString();
            ins_txtSec.text = _nSec.ToString();
            yield return null;

        }

        yield return StartCoroutine(CUIManager.Inst.CorAction(EmUIActionType.GameOver));
        yield return StartCoroutine(CUIManager.Inst.CorITweenFade());

        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(false);

        SetTimer();
    }

    private void SetTimer()
    {
        if (_fTimerStart <= 0)
        {
            CSceneManager.Inst.OnSceneMovement("CLobbyScene");
        }
    }

    protected override void SetTextInfo()
    {
        base.SetTextInfo();
        ins_txtMoney.text = CDBPlayerInfo.Inst.m_nMoney.ToString();
        ins_txtMonsterCnt.text = _nMonCnt.ToString();
    }


    // 히어로 셋팅.
    private void SetHero()
    {
        _strStriker = CDataManager.Inst.m_strProfilePath + CDBPlayerInfo.Inst.m_listHeroInfo[CSceneManager.Inst.m_nStriker].m_strProfile;
        _strPenetration = CDataManager.Inst.m_strProfilePath + CDBPlayerInfo.Inst.m_listHeroInfo[CSceneManager.Inst.m_nPenetration].m_strProfile;
        _strRecovery = CDataManager.Inst.m_strProfilePath + CDBPlayerInfo.Inst.m_listHeroInfo[CSceneManager.Inst.m_nRecovery].m_strProfile;

        ins_listHeroSlot[0].SetProfile().sprite = CResourceLoader.Load<Sprite>(_strStriker);
        ins_listHeroSlot[1].SetProfile().sprite = CResourceLoader.Load<Sprite>(_strPenetration);
        ins_listHeroSlot[2].SetProfile().sprite = CResourceLoader.Load<Sprite>(_strRecovery);


        ins_listHeroSlot[0].m_HpSlider.maxValue = CDBPlayerInfo.Inst.m_listHeroInfo[CSceneManager.Inst.m_nStriker].m_nHp;
        ins_listHeroSlot[1].m_HpSlider.maxValue = CDBPlayerInfo.Inst.m_listHeroInfo[CSceneManager.Inst.m_nPenetration].m_nHp;
        ins_listHeroSlot[2].m_HpSlider.maxValue = CDBPlayerInfo.Inst.m_listHeroInfo[CSceneManager.Inst.m_nRecovery].m_nHp;


        ins_listHeroSlot[0].m_HpSlider.value = ins_listHeroSlot[0].m_HpSlider.maxValue;
        ins_listHeroSlot[1].m_HpSlider.value = ins_listHeroSlot[1].m_HpSlider.maxValue;
        ins_listHeroSlot[2].m_HpSlider.value = ins_listHeroSlot[2].m_HpSlider.maxValue;
    }


    public void OnClickPreferences()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        Time.timeScale = 0;

        StartCoroutine(CUIManager.Inst.CorBattlePreferences());

        _bClickLock = false;
    }

    // 몬스터 수 제거.
    public void SetMonCnt(int nMonCnt)
    {
        this._nMonCnt -= nMonCnt;

        this.ins_txtMonsterCnt.text = _nMonCnt.ToString();

        if (_nMonCnt == 1)
        {
            StartCoroutine(CorGameWin());
        }
    }

    // 플레이어 승.
    private IEnumerator CorGameWin()
    {
        yield return StartCoroutine(CUIManager.Inst.CorAction(EmUIActionType.Victory));
        yield return StartCoroutine(CUIManager.Inst.CorITweenFade());

        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(false);

        CSceneManager.Inst.OnSceneMovement("CLobbyScene");
        Close();
    }

    // 타이머 체크.
    public float GetTimerCheck()
    {
        return _fTimerStart;
    }



    public void SetHeart()
    {

        _nHeartCnt += 1;
        ins_ImgHeart[_nHeartCnt - 1].gameObject.SetActive(false);

        for (int i = 0; i < ins_ImgHeart.Count; i++)
        {
            if (!ins_ImgHeart[i].gameObject.activeSelf)
            {
                // 타임 종료 게임오버로 처리한다.
                StartCoroutine(CorTimer(0));
            }
        }

        return;
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
