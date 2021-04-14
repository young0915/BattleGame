using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CUIManager : CSingleton<CUIManager>
{
    [SerializeField] private RectTransform[] ins_rtraRoots = null;

    private readonly string _strDefaultPath = "Prefab/UI/";
    private List<CBaseUI> _listUI = new List<CBaseUI>();


    public CUIStart m_cUIStart;
    public IEnumerator CorStart(EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if(m_cUIStart == null)
        {
            m_cUIStart = MakeUI<CUIStart>("CUIStart", eCanvas);
        }

        m_cUIStart.Initialization();
        m_cUIStart.Open(callBack);
        yield return null;
    }

    public CUILogin m_cUILogin;
    public IEnumerator CorLogin(EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if(m_cUILogin == null)
        {
            m_cUILogin = MakeUI<CUILogin>("CUILogin", eCanvas);
        }

        m_cUILogin.Initialization();
        m_cUILogin.Open(callBack);
        yield return null;
    }

    public CUILoading m_cUILoding;
    public IEnumerator CorLoding(string strSceneName,EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if(m_cUILoding == null)
        {
            m_cUILoding = MakeUI<CUILoading>("CUILoading", eCanvas);
        }

        m_cUILoding.Initialization(strSceneName);
        m_cUILoding.Open(callBack);
        yield return null;
    }

    public CUIAction m_cUIAction;
    public IEnumerator CorAction(EmUIActionType eAction, string strName = "", EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if(m_cUIAction == null)
        {
            m_cUIAction = MakeUI<CUIAction>("CUIAction", eCanvas);
        }

        m_cUIAction.SetTextInfoType(eAction, strName);
        yield return null;
    }

    public CUILobby m_cUILobby;
    public IEnumerator CorLobby(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if(m_cUILobby == null)
        {
            m_cUILobby = MakeUI<CUILobby>("CUILobby", eCanvas);
        }
        else
        {
            m_cUILobby.gameObject.SetActive(true);
        }

        m_cUILobby.Initialization();
        m_cUILobby.Open(callBack);
        yield return null;
    }

    public CUILobbyPreferences m_cUILobbyPreferences;
    public IEnumerator CorLobbyPreferences(EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if(m_cUILobbyPreferences == null)
        {
            m_cUILobbyPreferences = MakeUI<CUILobbyPreferences>("CUILobbyPreferences", eCanvas);
        }

        CUIManager.Inst.m_cUIMap.Close(false);
        m_cUILobbyPreferences.Open(callBack);
        m_cUILobbyPreferences.Initialization();
        yield return null;
    }

    public CUIMap m_cUIMap;
    public IEnumerator CorMap(EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack =null)
    {
        if(m_cUIMap == null)
        {
            m_cUIMap = MakeUI<CUIMap>("CUIMap", eCanvas);
        }

        m_cUIMap.Open(callBack);
        yield return null;
    }


    public CUIBattle m_cUIBattle;
    public IEnumerator CorBattle(float fCoolTime =0.0f ,int nMonCnt =0,EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if(m_cUIBattle == null)
        {
            m_cUIBattle = MakeUI<CUIBattle>("CUIBattle", eCanvas);
        }


        m_cUIBattle.Initialization(fCoolTime, nMonCnt);
        yield return null;
    }

    public CUIBattlePreferences m_cUIBattlePreferences;
    public IEnumerator CorBattlePreferences(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if(m_cUIBattlePreferences == null)
        {
            m_cUIBattlePreferences = MakeUI<CUIBattlePreferences>("CUIBattlePreferences", eCanvas);
        }

        m_cUIBattlePreferences.Open(callBack);
        m_cUIBattlePreferences.Initialization();
        yield return null;
    }


    public CUIStore m_cUIStore;
    public IEnumerator CorStore(EmCanvasLayer eCanvas = EmCanvasLayer.Layer2, Action<EmClickState> callback = null)
    {
        if(m_cUIStore == null)
        {
            m_cUIStore = MakeUI<CUIStore>("CUIStore", eCanvas);
        }

        CUIManager.Inst.m_cUIMap.Close(false);
        m_cUIStore.Open(callback);
        m_cUIStore.Initialization();
        yield return null;
    }

    public CUIInventory m_cUIInventory;
    public IEnumerator CorInventory(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack  = null)
    {
        if(m_cUIInventory ==null)
        {
            m_cUIInventory = MakeUI<CUIInventory>("CUIInventory", eCanvas);
        }

        CUIManager.Inst.m_cUIMap.Close(false);
        m_cUIInventory.Open(callBack);
        m_cUIInventory.Initialization();
        yield return null;
    }

    public CUIItemInfo m_cUIItemInfo;
    public IEnumerator CorItemInfo(Transform traPos,int nId ,EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callback = null)
    {
        if(m_cUIItemInfo == null)
        {
            m_cUIItemInfo = MakeUI<CUIItemInfo>("CUIItemInfo", eCanvas);
        }

        m_cUIItemInfo.Open(callback);
        m_cUIItemInfo.Initialization(traPos, nId);
        yield return null;
    }

    public CUITower m_cUITower; 
    public IEnumerator CorTower(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if(m_cUITower== null)
        {
            m_cUITower = MakeUI<CUITower>("CUITower", eCanvas);
        }

        CUIManager.Inst.m_cUIMap.Close(false);
        m_cUITower.Open(callBack);
        m_cUITower.Initialization();
        yield return null;
    }

    public CUITowerBattle m_cUITowerBattle;
    public IEnumerator CorTowerBattle(Transform traPos, bool bTowerPosY, EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if(m_cUITowerBattle == null)
        {
            m_cUITowerBattle = MakeUI<CUITowerBattle>("CUITowerBattle",eCanvas);
        }

        m_cUITowerBattle.Initialization(traPos, bTowerPosY);
        m_cUITowerBattle.Open(callBack);
        yield return null;
    }

    public CUIHero m_cUIHero;
    public IEnumerator CorHero(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if(m_cUIHero == null)
        {
            m_cUIHero = MakeUI<CUIHero>("CUIHero", eCanvas);
        }

        CUIManager.Inst.m_cUIMap.Close(false);

        m_cUIHero.Initialization();
        m_cUIHero.Open(callBack);
        yield return null;
    }


    public CUIBattlePreparationMode m_cUIBattlePreparationMode;
    public IEnumerator CorBattlePreparation(string strBattleScene, EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if(m_cUIBattlePreparationMode == null)
        {
            m_cUIBattlePreparationMode = MakeUI<CUIBattlePreparationMode>("CUIBattlePreparationMode", eCanvas);
        }

        m_cUIBattlePreparationMode.Initialization(strBattleScene);
        m_cUIBattlePreparationMode.Open(callBack);
        yield return null;
    }


    public CUIQuestInfo m_cUIQuestInfo;
    public IEnumerator CorQuestInfo(int nId,EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if(m_cUIQuestInfo == null)
        {
            m_cUIQuestInfo = MakeUI<CUIQuestInfo>("CUIQuestInfo", eCanvas);
        }
        m_cUIQuestInfo.Initialization(nId);
        m_cUIQuestInfo.Open(callBack);
        yield return null;
    }

    public CUIProfileWin m_cUIProfileWin;
    public IEnumerator CorProfileWin(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if(m_cUIProfileWin == null)
        {
            m_cUIProfileWin = MakeUI<CUIProfileWin>("CUIProfileWin", eCanvas);
        }

        CUIManager.Inst.m_cUIMap.Close(false);
        m_cUIProfileWin.Initialization();
        m_cUIProfileWin.Open(callBack);
        yield return null;
    }


    #region [code] ITween 함수들.

    public CUIITweenFade m_cUITweenFade;
    public IEnumerator CorITweenFade(EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if (m_cUITweenFade == null)
        {
            m_cUITweenFade = MakeUI<CUIITweenFade>("CUIITweenFade", eCanvas);
        }

        m_cUITweenFade.Initialization();
        m_cUITweenFade.Open(callBack);
        yield return null;
    }

    #endregion


    #region [code] DefaultFunc

    private T MakeUI<T>(string strPrefabsName, EmCanvasLayer eCanvas, bool bAsync = false) where T : CBaseUI
    {
        string strPath = _strDefaultPath + strPrefabsName;
        T cPrefab = null;

        if (bAsync)
            CResourceLoader.LoadAsync<T>(strPath, (res) => cPrefab = res);
        else
            cPrefab = CResourceLoader.Load<T>(strPath);


        T instance = Instantiate<T>(cPrefab);
        instance.GetComponent<RectTransform>().SetParent(ins_rtraRoots[(int)eCanvas]);
        instance.transform.localScale = Vector3.one;
        InitRectTransform(instance.gameObject);

        return instance;
    }

    private void InitRectTransform(GameObject gameObject, bool useCanvasSize = true)
    {
        RectTransform ot = gameObject.GetComponent<RectTransform>();
        RectTransform rt = gameObject.transform.parent.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        ot.anchorMin = new Vector2(0.5f, 0.5f);
        ot.anchorMax = new Vector2(0.5f, 0.5f);
        ot.pivot = new Vector2(0.5f, 0.5f);
        ot.localPosition = Vector3.zero;

        if (true == useCanvasSize)
            ot.sizeDelta = rt.sizeDelta;
    }

    public void AddUI(CBaseUI cBaseUI)
    {
        _listUI.Add(cBaseUI);
    }

    public void RemoveUI(CBaseUI cBaseUI)
    {
        _listUI.Remove(cBaseUI);
    }

    // 쓸까말까 고민중.
    public void SceneChange()
    {
        for(int i =0; i< _listUI.Count; i++)
        {
            _listUI[i].Close(true);
        }
        _listUI.Clear();
    }


    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && _listUI.Count >0)
        {
            CBaseUI cCloseUI = _listUI[_listUI.Count-1];
           // cCloseUI.
        }
    }

    #endregion
}
