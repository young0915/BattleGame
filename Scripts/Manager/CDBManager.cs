using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine;


public class CDBManager : CSingleton<CDBManager>
{
    private List<CMonInfo> _listMonInfo = new List<CMonInfo>();
    public List<CMonInfo> m_listMonInfo { get { return _listMonInfo; } }


    private List<CHeroInfo> _listHeroInfo = new List<CHeroInfo>();
    public List<CHeroInfo> m_listHeroInfo { get { return _listHeroInfo; } }

    private List<CTowerInfo> _listTowerIfno = new List<CTowerInfo>();
    public List<CTowerInfo> m_listTowerInfo { get { return _listTowerIfno; } }

    private List<CItemInfo> _listItemInfo = new List<CItemInfo>();
    public List<CItemInfo> m_listItemInfo { get { return _listItemInfo; } }


    private readonly string _strDBPath = "URI=file:CTowerDefense.db";
    private readonly string _strDBCmdText = "SELECT * From CHero;";
    private readonly string _strDBCmdTowerText = "SELECT * From CTower;";
    private readonly string _strDBCmdItemText = "SELECT * From CItem;";
    private readonly string _strDBMonCmdText = "SELECT * From CMonsterData;";


    private EmDBType _eDBType;


    public void Install()
    {
        CDBManager.Inst.OnCreateDB(EmDBType.Monster);
        CDBManager.Inst.OnCreateDB(EmDBType.Hero);
        CDBManager.Inst.OnCreateDB(EmDBType.Tower);
        CDBManager.Inst.OnCreateDB(EmDBType.Item);

    }

    private void OnCreateDB(EmDBType eDBType)
    {
        this._eDBType = eDBType;

        string strDBPath = string.Empty;
        string strCmdText = string.Empty;

        strDBPath = _strDBPath;

        switch (_eDBType)
        {
            case EmDBType.Monster:
                strCmdText = _strDBMonCmdText;

                break;

            case EmDBType.Hero:
                strCmdText = _strDBCmdText;
                break;

            case EmDBType.Tower:
                strCmdText = _strDBCmdTowerText;
                break;

            case EmDBType.Item:
                strCmdText = _strDBCmdItemText;

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
                            case EmDBType.Monster:
                                _listMonInfo.Add(new CMonInfo(
                                    reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetInt32(2),
                                reader.GetInt32(3),
                                reader.GetInt32(4)));

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

                            case EmDBType.Tower:
                                _listTowerIfno.Add(new CTowerInfo(
                                    reader.GetInt32(0),
                                    (EmTowerType)reader.GetInt32(1),
                                    reader.GetInt32(2),
                                    reader.GetString(3),
                                    reader.GetInt32(4),
                                    reader.GetInt32(5),
                                      reader.GetString(6)
                                    ));


                                break;

                            case EmDBType.Item:
                                _listItemInfo.Add(new CItemInfo(
                                    reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetString(2),
                                    reader.GetInt32(3),
                                    reader.GetString(4)
                                    ));


                                break;

                        } // switch of End.
                    }

                    reader.Close();
                } // using of End.

                Connection.Close();
            }
        }

    }

}
