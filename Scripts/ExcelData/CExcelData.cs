[System.Serializable]
public class CExcelData 
{
    public CExcelInfo[] m_items;
}

[System.Serializable]
public class CExcelInfo
{
    public CExcelInfo(string key, string value)
    {
        m_strKey = key;
        m_strValue = value;
    }

    public string m_strKey;
    public string m_strValue;


}

