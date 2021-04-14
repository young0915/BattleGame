using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CHeroSlot : MonoBehaviour
{

    [SerializeField] private Image ins_ImgHeroProfile;
    [SerializeField] private Image ins_ImgCoolTime;
    public Image m_ImgCoolTime { get { return ins_ImgCoolTime; } }
    [SerializeField] private Slider ins_HpSlider;
    public Slider m_HpSlider { get { return ins_HpSlider; } }

    [SerializeField] private Button ins_BtnHeroSkill;
    public Button m_BtnHeroSkill { get { return ins_BtnHeroSkill; } }

    private EmHeroType _eHeroType;

    private float _fCoolTime = 7.0f;                        // 테스트.
    private bool _bClickLock = false;



    public void OnClickHeroSkill(int nIdx)
    {
        if (_bClickLock)
            return;

        EmHeroType eHero = (EmHeroType)nIdx;
        if (_eHeroType == eHero)
            return;

        StartCoroutine(CorSkillCoolTimeSkill());
    }



    private IEnumerator CorSkillCoolTimeSkill()
    {
        ins_ImgCoolTime.fillAmount = 1.0f;

        while (ins_ImgCoolTime.fillAmount > 0)
        {
            ins_ImgCoolTime.fillAmount -= 1 * Time.deltaTime / _fCoolTime;
            yield return null;
        }
        yield break;
    }


    public Image SetProfile()
    {
        return ins_ImgHeroProfile;
    }

}
