using UnityEngine;
using System.Collections;

public class CBattleScene : CSeneObject
{
    private CMonGenerator[] _cMonGenerator = new CMonGenerator[2];

    private float _fCoolTime = 0.0f;
    private string _strSceneName = string.Empty;


    #region [code] Hero Values.

    [SerializeField] private CHeroFactoryUnit _cHeroFactroy = null;

    [SerializeField] private GameObject ins_objStrikerUnit;
    [SerializeField] private GameObject ins_objPenetrationUnit;
    [SerializeField] private GameObject ins_objRecoveryUnit;
    #endregion

    protected override void OnSceneAwake()
    {
        base.OnSceneAwake();
        CSoundManager.Inst.SetPlayMusic(2);

    }

    private GameObject ins_obj;
    protected override void OnSceneStart()
    {
        base.OnSceneStart();

       

        CObjectPoolingManager.Inst.Install();

        CCameraManager.Inst.SetCameraView(EmCameraType.Battle);
        StartCoroutine(CorBattleSceneInstall());

        _strSceneName = CSceneManager.Inst.GetCurSceneName();
        //ins_objStrikeUnit

        ins_objStrikerUnit = _cHeroFactroy.CreateUnit(EmHeroType.Striker, CSceneManager.Inst.m_nStriker, ins_objStrikerUnit.transform.position);
        ins_objPenetrationUnit = _cHeroFactroy.CreateUnit(EmHeroType.Penetration, CSceneManager.Inst.m_nPenetration, ins_objPenetrationUnit.transform.position);
        ins_objRecoveryUnit = _cHeroFactroy.CreateUnit(EmHeroType.Recovery, CSceneManager.Inst.m_nRecovery, ins_objRecoveryUnit.transform.position);

        switch (_strSceneName)
        {
            case CDataManager.m_strOne:
                _cMonGenerator[0] = new CMonsterFirstRound();
                StartCoroutine(_cMonGenerator[0].CorCreateMon());

                break;

            case CDataManager.m_strTwo:
                _cMonGenerator[1] = new CMonsterSecRound();
                StartCoroutine(_cMonGenerator[1].CorCreateMon());

                break;
        }
    }

    private IEnumerator CorBattleSceneInstall()
    {

        if (CUIManager.Inst.m_cUILobby != null && CUIManager.Inst.m_cUIMap !=null)
        {
            CUIManager.Inst.m_cUILobby.Close(false);
            CUIManager.Inst.m_cUIMap.Close(false);
        }
        
        if(CSceneManager.Inst.GetCurSceneName() == "1")
        {
            _fCoolTime = 90.0f;
        }
        else
        {
            _fCoolTime = 130.0f;
          
        }
        yield return StartCoroutine(CUIManager.Inst.CorBattle(_fCoolTime, 12));


    }
}
