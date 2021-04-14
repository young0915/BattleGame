public class CItemInfo 
{
    public int m_nId = 0;
    public string m_strName = string.Empty;
    public string m_strContent = string.Empty;
    public int m_nMoney=0;
    public string m_strPath = string.Empty;

    public CItemInfo(int nId, string strName, string strContent, int nMoeny, string strPath)
    {
        this.m_nId = nId;
        this.m_strName = strName;
        this.m_strContent = strContent;
        this.m_nMoney = nMoeny;
        this.m_strPath = strPath;
    }
}
