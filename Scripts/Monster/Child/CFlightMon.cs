using UnityEngine;


public class CFlightMon : CMonster
{
 
    public CFlightMon(string strPath, Vector3 Transform)
    {
        this.strMonPath = strPath;

        var prefab = CResourceLoader.Load<CFlightMon>(strMonPath);
        CMonster obj = Instantiate(prefab) as CFlightMon;

        obj.transform.position = Transform;

        // 몬스터는 배틀씬 자식으로 들어가게 한다.
        obj.transform.parent = CSceneManager.Inst.m_CurScene.transform;

    }

    protected override void Awake()
    {
        cMonInfo = new CMonInfo(
                 CDBManager.Inst.m_listMonInfo[2].m_nId,
                 CDBManager.Inst.m_listMonInfo[2].m_nHp,
                 CDBManager.Inst.m_listMonInfo[2].m_nSpeed,
                 CDBManager.Inst.m_listMonInfo[2].m_nAttack,
                 CDBManager.Inst.m_listMonInfo[2].m_nCritical);

        ins_HpSlider.maxValue = cMonInfo.m_nHp;
        ins_HpSlider.value = ins_HpSlider.maxValue;
    }

    protected override void Start()
    {
        base.Start();
    }


}
