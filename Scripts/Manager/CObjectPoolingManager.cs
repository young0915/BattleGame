using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CObjectInfo
{
    public EmObjectPoolType m_eObjectPoolType;                                  // Object 타입.
    public int m_nAmount = 0;                                                           // 오브젝트 타입 수.
    public GameObject m_objectPrefab;                                               // 프리팹.
    public GameObject m_objPos;                                                      // 생성된 Obj소속 우치.                                                     
    [SerializeField] private Vector3 _VecPos;                                          // ObjectPool Manager를 사용할 위치(영역 범위).
    [HideInInspector] public Vector3 m_VecPos { get { return _VecPos; } }

    [HideInInspector] public List<GameObject> m_Poollist = new List<GameObject>();
}


public class CObjectPoolingManager : CSingleton<CObjectPoolingManager>
{
    [SerializeField] private List<CObjectInfo> ins_ObjectPoolist = new List<CObjectInfo>();


   public void Install()
    {
        for(int i =0; i<ins_ObjectPoolist.Count; i++)
        {
            CreateObject(ins_ObjectPoolist[i]);
        }
    }

    private void CreateObject(CObjectInfo cInfo)
    {
        GameObject obj = null;

        for(int i =0; i<cInfo.m_nAmount; i++)
        {
            obj = Instantiate(cInfo.m_objectPrefab, cInfo.m_objPos.transform);
            obj.name = cInfo.m_objectPrefab.name + i.ToString("00");
            obj.SetActive(false);

            cInfo.m_Poollist.Add(obj);
        }

    }

    public GameObject GetObject(int num)
    {
        for (int i =0; i<ins_ObjectPoolist[num].m_Poollist.Count; i++)
        {
            if(ins_ObjectPoolist[num].m_Poollist[i].activeSelf == false)
            {
                return ins_ObjectPoolist[num].m_Poollist[i];
            }
        }
        return null; 
    }



    public void GetObjectActive(bool bIsOn)
    {
        CObjectPoolingManager.Inst.gameObject.SetActive(bIsOn);
    }
}
