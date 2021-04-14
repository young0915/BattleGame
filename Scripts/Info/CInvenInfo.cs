public class CInvenInfo
{
    public int m_nId = 0;
    public EmInven m_eInven = EmInven.None;
    public int m_nItemId = 0;               // CItemInfo m_nId를 말하는 것.
    public int m_nCnt = 0;
    public string m_strItemPath = string.Empty;

    public CInvenInfo(int nId, EmInven eInven, int nItemId, int nCnt, string strItemPath)
    {
        this.m_nId = nId;
        this.m_eInven = eInven;
        this.m_nItemId = nItemId;
        this.m_nCnt = nCnt;
        this.m_strItemPath = strItemPath;
        
    }
}
