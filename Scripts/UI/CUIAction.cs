using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CUIAction : CBaseUI
{
    [SerializeField] private Image ins_ImgPanel;
    [SerializeField] private Text ins_txtContentAction;

    private EmUIActionType _eUIActionType;
    private bool _bIsTitle = false;
    private bool _bIsContent = false;
    private string _strTitle = string.Empty;
    private string _strContent = string.Empty;
    private string _strName = string.Empty;                                                     // 만든 이유: 닉네임을 알기 위해서.

    public void SetTextInfoType(EmUIActionType eAction,string strName)
    {
        this._eUIActionType = eAction;
        this._strName = strName;
        _strTitle = string.Empty;
        _strContent = string.Empty;

        switch (eAction)
        {
            case EmUIActionType.GameOver:
                _strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 14);

                break;

            case EmUIActionType.Victory:

                _strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 15);
                break;

            case EmUIActionType.NicknameNotallowed:
                _strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 4);
                
                break;

            case EmUIActionType.lackInventory:
                _strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 9);

                break;

            case EmUIActionType.lackMoney:
                _strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 8);

                break;

            case EmUIActionType.HeroLack:
                _strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 30);

                break;

            case EmUIActionType.PossessionProduct:
                _strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 35);
                break;

            case EmUIActionType.NoSeatsAvailable:
                _strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 36);

                break;
            case EmUIActionType.Unreinforceable:
                _strContent = string.Format("{0}   {1}", strName,CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 38));
                break;


        }

        ins_txtContentAction.text = _strContent.ToString();

        // iTween alpha.
        StartCoroutine(CorAlpha());
    }

    #region [code] ITween
    private IEnumerator CorAlpha()
    {
        SetITweenValueTo(1.0f, 0.0f, 1f);
        SetITweenValueTo(0.0f, 1.0f, 1f);
        SetITweenValueTo(1.0f, 0.0f, 1f);
        yield return new WaitForSeconds(0.8f);
  
       Close();
    }

    public void SetITweenValueTo(float fFrom, float fTo, float fTime)
    {
        Hashtable hash = new Hashtable(){
             {"from", fFrom},
             {"to", fTo},
             {"time", fTime},
             {"speed", 1f},
             {"delay", 0f},
             {"easeType",iTween.EaseType.linear},
             {"loopType",iTween.LoopType.none},
             {"onupdate", "SetImageColor"},
             {"onupdatetarget", gameObject},
         };
        iTween.ValueTo(gameObject, hash);
    }

    void SetImageColor(float alpha)
    {
        ins_ImgPanel.color = new Color(ins_ImgPanel.color.r, ins_ImgPanel.color.g, ins_ImgPanel.color.b, alpha);
        ins_txtContentAction.color = new Color(ins_txtContentAction.color.r, ins_txtContentAction.color.g, ins_txtContentAction.color.b, alpha);
    }

    #endregion

    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestory = true)
    {
        base.Close(bDestory);
        Destroy(gameObject);
    }
}
