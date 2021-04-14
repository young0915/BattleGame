public class CHeroInfo 
{
    public int m_nId;
    public int m_nLv;
    public EmHeroType m_eHeroType;
    public string m_strName = string.Empty;
    public string m_strContent = string.Empty;
    public int m_nHp;
    public int m_nAttack;
    public int m_nCritical;
    public string m_strProfile;
    public int m_nMoney;
    public string m_strPrefabPath;

    // CBattleScene에서 히어로 정보를 사용할 때.
    public CHeroInfo(int nLv, EmHeroType eHeroType, int nHp, int nAttack, int nCritical)
    {
        this.m_nLv = nLv;
        this.m_eHeroType = eHeroType;
        this.m_nHp = nHp;
        this.m_nAttack = nAttack;
        this.m_nCritical = nCritical;
    }

    // 데이터 파싱할 때 사용하는 것.
    public CHeroInfo(int nId, int nLv, EmHeroType eHeroType,string strName, string strContent, int nHp,int nAttack, int nCritical, string strProfile, int nMoney, string strPrefabPath)
    {
        this.m_nId = nId;
        this.m_nLv = nLv;
        this.m_eHeroType = eHeroType;
        this.m_strName = strName;
        this.m_strContent = strContent;
        this.m_nHp = nHp;
        this.m_nAttack = nAttack;
        this.m_nCritical = nCritical;
        this.m_strProfile = strProfile;
        this.m_nMoney = nMoney;
        this.m_strPrefabPath = strPrefabPath;
    }

}
