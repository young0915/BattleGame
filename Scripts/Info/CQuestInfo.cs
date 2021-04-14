public class CQuestInfo 
{
    public int m_nId;
    public string m_strTitle;
    public string m_strContent;
    public string m_strImgPath;
    public int m_nCoin;

    public CQuestInfo(int nId, string strTitle, string strContent, string strImgPath, int nCoin)
    {
        this.m_nId = nId;
        this.m_strTitle = strTitle;
        this.m_strContent = strContent;
        this.m_strImgPath = strImgPath;
        this.m_nCoin = nCoin;
    }

}
