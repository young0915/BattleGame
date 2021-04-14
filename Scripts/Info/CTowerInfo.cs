public class CTowerInfo
{
    public int m_nId;
    public EmTowerType m_eTowerType;
    public int m_nLv = 0;
    public string m_strName = string.Empty;
    public int m_nMoney =0;
    public int m_nAttack = 0;
    public string m_strProfile = string.Empty;

    public CTowerInfo(int nId, EmTowerType eTowerType, int nLv,string strName, int nMoney, int nAttack, string strProfile)
    {
        this.m_nId = nId;
        this.m_eTowerType = eTowerType;
        this.m_nLv = nLv;
        this.m_strName = strName;
        this.m_nMoney = nMoney;
        this.m_nAttack = nAttack;
        this.m_strProfile = strProfile;
    }

}
