using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIQuestCell : MonoBehaviour
{

    private int _nId;
    private string _strQuestContent = string.Empty;
    private bool _bClickLock = false;

    public void SetData(CQuestModel cModel)
    {
       _nId = cModel.m_nId;

    }


    public void OnClickQuestInfo()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;
        StartCoroutine(CUIManager.Inst.CorQuestInfo(_nId));

        _bClickLock = false;
    }
}
