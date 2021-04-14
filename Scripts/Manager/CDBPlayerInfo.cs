using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class CDBPlayerInfo : CSingleton<CDBPlayerInfo>
{
    [HideInInspector] public string m_strName;
    [HideInInspector] public int m_nMoney;

    //CPlayerData

    private List<CTowerInfo> _listTowerInfo = new List<CTowerInfo>();
    public List<CTowerInfo> m_listTowerInfo { get { return _listTowerInfo; } }

    private List<CHeroInfo> _listHeroInfo = new List<CHeroInfo>();
    public List<CHeroInfo> m_listHeroInfo { get { return _listHeroInfo; } }

    private List<CInvenInfo> _listInvenInfo = new List<CInvenInfo>();
    public List<CInvenInfo> m_listInvenInfo { get { return _listInvenInfo; } }

    private string _strDBPath = "URI=file:CDBPlayer.db";

    private string _strDBTowerCmdTxt = "SELECT * From CPlayerTowerData;";
    private string _strDBHeroCmdtxt = "SELECT * From CPlayerHeroData;";
    private string _strDBInvenCmdtxt = "SELECT * From CPlayerInventroy;";
    private EmDBType _eDBType;


    public void Init()
    {
        OnPlayerInfoDelete();
        CreateDB();
        OnCreateDB(EmDBType.Tower);
        OnCreateDB(EmDBType.Hero);
        OnCreateDB(EmDBType.Inven);
    }


    #region [DB Code ] CPlayerData.
    public void CreateDB()
    {
        using (var connection = new SqliteConnection(_strDBPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF Not EXISTS CPlayerData (m_strName VARCHAR(30), m_nMoney VARCHAR(20));";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void AddPlayerInfo(string strName, int nMoney = 0)
    {
        using (var connection = new SqliteConnection(_strDBPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {

                command.CommandText = "INSERT INTO CPlayerData VALUES(" + '"' + strName + '"' + ',' + nMoney + ");";

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        DisPlayPlayerInfo();
    }


    private void DisPlayPlayerInfo()
    {
        using (var connection = new SqliteConnection(_strDBPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * From CPlayerData;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        m_strName = reader["m_strName"].ToString();
                        m_nMoney = reader.GetInt32(1);
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    //public void OnPlayerInfoUpdate(int nMoney, string strName)
    public void OnPlayerInfoUpdate(int nMoney)
    {
        using (var connection = new SqliteConnection(_strDBPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE CPlayerData Set m_nMoney  =" + nMoney + ";";
                command.ExecuteNonQuery();
                CUIManager.Inst.m_cUILobby.SetMoney(nMoney);
            }
            connection.Close();
        }
    }


    private void OnPlayerInfoDelete()
    {
        // m_strName이 값이 있다면 체크하기 위해.
        if (m_strName != string.Empty)
        {
            using (var connection = new SqliteConnection(_strDBPath))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE From CPlayerData;";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

    }

    #endregion

    public void OnCreateDB(EmDBType eDBType)
    {
        this._eDBType = eDBType;


        string strDBPath = string.Empty;
        string strCmdText = string.Empty;

        switch (_eDBType)
        {
            case EmDBType.Tower:
                strDBPath = _strDBPath;
                strCmdText = _strDBTowerCmdTxt;

                break;

            case EmDBType.Hero:
                strDBPath = _strDBPath;
                strCmdText = _strDBHeroCmdtxt;

                break;
            case EmDBType.Inven:
                strDBPath = _strDBPath;
                strCmdText = _strDBInvenCmdtxt;
                break;

        }


        using (var Connection = new SqliteConnection(strDBPath))
        {
            Connection.Open();

            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = strCmdText;

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        switch (_eDBType)
                        {
                            case EmDBType.Tower:
                                _listTowerInfo.Add(new CTowerInfo(
                                    reader.GetInt32(0),
                                    (EmTowerType)reader.GetInt32(1),
                                    reader.GetInt32(2),
                                    reader.GetString(3),
                                    reader.GetInt32(4),
                                    reader.GetInt32(5),
                                      reader.GetString(6)
                                    ));

                                break;

                            case EmDBType.Hero:
                                _listHeroInfo.Add(new CHeroInfo(
                             reader.GetInt32(0),
                             reader.GetInt32(1),
                             (EmHeroType)reader.GetInt32(2),
                             reader.GetString(3),
                             reader.GetString(4),
                             reader.GetInt32(5),
                             reader.GetInt32(6),
                             reader.GetInt32(7),
                             reader.GetString(8),
                             reader.GetInt32(9),
                            reader.GetString(10)
                             ));

                                break;
                            case EmDBType.Inven:
                                _listInvenInfo.Add(new CInvenInfo(
                                    reader.GetInt32(0),
                                    (EmInven)reader.GetInt32(1),
                                    reader.GetInt32(2),
                                    reader.GetInt32(3),
                                    reader.GetString(4)
                                    ));
                                break;
                        }
                    }

                    //break;
                    /*}*/ // switch of End.
                    reader.Close();
                } // using of End.
                Connection.Close();
            }
        }
    }


    public void SetTowerPurchase(int nId, int nTowerType, int nLv, string strName,
        int nMoney, int nAttack,  string strProfile)
    {
        using (var connection = new SqliteConnection(_strDBPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Update CPlayerTowerData Set m_eTowerType =" + nTowerType +
                    ", m_nLv = " + nLv +
                    ", m_strName = " + '"' + strName + '"' +
                    ", m_nMoney =" + nMoney +
                    ", m_nAttack =" + nAttack +
                    ", m_strProfile =" + '"' + strProfile + '"' + "Where m_nId  =" + nId + "; ";

                command.ExecuteNonQuery();

                _listTowerInfo.Clear();
                OnCreateDB(EmDBType.Tower);
            }
            connection.Close();
        }
    }


    public void SetHeroPurchase(int nHeroType, int nId)
    {
        using (var connection = new SqliteConnection(_strDBPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE CPlayerHeroData set m_eHeroType ="
                    + nHeroType + " WHERE m_nId =" + nId + ';';

                command.ExecuteNonQuery();

                _listHeroInfo.Clear();
                OnCreateDB(EmDBType.Hero);
            }
            connection.Close();
        }
    }




    public void SetHeroEnhance(int nLv, int nHp, int nAttack, int nCritical, int nId)
    {

        using (var connection = new SqliteConnection(_strDBPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE CPlayerHeroData Set m_nLv = " + nLv + ", m_nHp =" + nHp +
                    ", m_nAttack = " + nAttack + " , m_nCritical = " + nCritical + " Where m_nId = " + nId + ";";

                command.ExecuteNonQuery();

                _listHeroInfo.Clear();
                OnCreateDB(EmDBType.Hero);
            }
            connection.Close();
        }
    }

    // 아이템 구매 또는 삭제.
    public void SetItemUpdate(int eInven, int nItemId, int nCnt, string strItemPath, int nId)
    {
        using (var connection = new SqliteConnection(_strDBPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Update CPlayerInventroy Set m_eInven = " + eInven + ", m_nItemId =" + nItemId +
            ", m_nCnt=" + nCnt + ", m_strItemPath =" + '"' + strItemPath + '"' + " where m_nId =" + nId + ";";
                command.ExecuteNonQuery();

                _listInvenInfo.Clear();
                OnCreateDB(EmDBType.Inven);
            
            }
            connection.Close();
        }
    }


    // 같은 이름만 있을 경우는 수량만 변경.
    public void SetSameItemPurchase(int nCnt, int nItemId)
    {
        using (var connection = new SqliteConnection(_strDBPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE CPlayerInventroy SET m_nCnt =" + nCnt + " WHERE m_nItemId =" + nItemId + ";";

                command.ExecuteNonQuery();
                _listInvenInfo.Clear();
                OnCreateDB(EmDBType.Inven);
            }
            connection.Close();
        }
    }

    public void Inven()
    {
        OnCreateDB(EmDBType.Inven);
    }



}
