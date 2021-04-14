using UnityEngine;
using System.Collections;

public class CCharacterAni : MonoBehaviour
{
    [SerializeField] private Animator ins_Ani;
    private EmCharacterAction _eCharacterAction;

    #region [code] Animation Variable
    private const string strAttack = "bIsAttack";
    private const string strGetHit = "bIsGetHit";
    private const string strDie = "bIsDie";
    private const string strVictory = "bIsVictory";
    private const string strSkill = "bIsSkill";
    
    #endregion


    public void SetAction(EmCharacterAction eHeroAction)
    {
        this._eCharacterAction = eHeroAction;

        string strAni = string.Empty;

        switch (_eCharacterAction)
        {
            case EmCharacterAction.Attack:
                strAni = strAttack;

                break;
            case EmCharacterAction.Skill:
                strAni = strSkill;

                break;
            case EmCharacterAction.GetHit:
                strAni = strGetHit;

                break;
            case EmCharacterAction.Victory:
                strAni = strVictory;

                break;
            case EmCharacterAction.Die:
                strAni = strDie;

                break;

        }

        StartCoroutine(CorAni(strAni));
    }


    private IEnumerator CorAni(string strAni)
    {
       
        ins_Ani.SetBool(strAni, true);
        yield return new WaitForSeconds(0.3f);
        ins_Ani.SetBool(strAni, false);

    }
}
