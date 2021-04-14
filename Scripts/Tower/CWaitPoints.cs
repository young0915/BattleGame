using UnityEngine;

public class CWaitPoints : MonoBehaviour
{
    public static Transform[] m_traPoints;

    private void Awake()
    {
        m_traPoints = new Transform[transform.childCount];

        for(int i =0; i < m_traPoints.Length; i++)
        {
            m_traPoints[i] = transform.GetChild(i);
        }
    }

}
