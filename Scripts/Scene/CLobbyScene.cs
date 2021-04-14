using UnityEngine;
using System.Collections;

// 스타트 씬.
public class CLobbyScene : CSeneObject
{
    protected override void OnSceneAwake()
    {
        base.OnSceneAwake();
        CSoundManager.Inst.SetPlayMusic(1);
    }


    protected override void OnSceneStart()
    {
        base.OnSceneStart();

        CUIManager.Inst.SceneChange();
        CCameraManager.Inst.SetCameraView(EmCameraType.Lobby);

        StartCoroutine(CorLobbySetting());

        if (CUIManager.Inst.m_cUILobby != null && CUIManager.Inst.m_cUIMap != null
            )
        {

            CUIManager.Inst.m_cUILobby.gameObject.SetActive(true);
            CUIManager.Inst.m_cUIMap.gameObject.SetActive(true);

            // 코인획득
            CDBPlayerInfo.Inst.m_nMoney += CQuestManager.Inst.m_listQuestInfo[0].m_nCoin;
            CDBPlayerInfo.Inst.OnPlayerInfoUpdate(CDBPlayerInfo.Inst.m_nMoney);

            CUIManager.Inst.m_cUILobby.m_cQuestScrollView.RemoveQuestFrom(0, 1);

        }

        if(CUIManager.Inst.m_cUIBattle != null)
        {
            CUIManager.Inst.m_cUIBattle.Close();
        }
    }

    private IEnumerator CorLobbySetting()
    {
        UILobbyCheck();
        // 한번 UI 정리.
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(CUIManager.Inst.CorITweenFade());
        yield return new WaitForSeconds(0.3f);



        if (CUIManager.Inst.m_cUILobby == null && CUIManager.Inst.m_cUIMap == null)
        {
            yield return StartCoroutine(CUIManager.Inst.CorLobby());
            yield return StartCoroutine(CUIManager.Inst.CorMap());
        }

    }

    /// <summary>
    ///  UI 정리 씬을 이동시 UI를 체크하면서 업애야한다.
    /// </summary>
    private void UILobbyCheck()
    {

        if (CUIManager.Inst.m_cUIBattle != null)
        {
            CUIManager.Inst.m_cUIBattle.Close();
        }
        if (CUIManager.Inst.m_cUILogin != null)
        {
            CUIManager.Inst.m_cUILogin.Close();
        }
    }


}
