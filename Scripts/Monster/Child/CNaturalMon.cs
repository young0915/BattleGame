using UnityEngine;

public class CNaturalMon : CMonster
{
    public CNaturalMon(string strPath, Vector3 Transform)
    {
        this.strMonPath = strPath;

        var prefab = CResourceLoader.Load<CNaturalMon>(strMonPath);
        CMonster obj = Instantiate(prefab) as CNaturalMon;

        obj.transform.position = Transform;

        // 몬스터는 배틀씬 자식으로 들어가게 한다.
        obj.transform.parent = CSceneManager.Inst.m_CurScene.transform;
    }
    #region

    protected override void Awake()
    {
        cMonInfo = new CMonInfo(
          CDBManager.Inst.m_listMonInfo[0].m_nId,
          CDBManager.Inst.m_listMonInfo[0].m_nHp,
          CDBManager.Inst.m_listMonInfo[0].m_nSpeed,
           CDBManager.Inst.m_listMonInfo[0].m_nAttack,
          CDBManager.Inst.m_listMonInfo[0].m_nCritical);

        ins_HpSlider.maxValue = cMonInfo.m_nHp;
        ins_HpSlider.value = ins_HpSlider.maxValue;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void GetNextWaypoint()
    {
        base.GetNextWaypoint();
    }


  


    #endregion





}
