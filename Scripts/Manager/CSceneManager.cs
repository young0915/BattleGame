using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CSceneManager : CSingleton<CSceneManager>
{
    public CSeneObject m_CurScene { get; set; } = null;

    public string m_strCurSceneName { get; private set; } = string.Empty;
    public int m_nStriker = 0;
    public int m_nPenetration = 0;
    public int m_nRecovery = 0;

    public void OnSceneMovement(string strSceneName)
    {
        m_strCurSceneName = strSceneName;

        SceneManager.LoadScene(strSceneName);
    }


    public void OnSceneMovement(string strSceneName, int nStriker, int nPenetration, int nRecovery)
    {
        m_strCurSceneName = strSceneName;

        this.m_nStriker = nStriker;
        this.m_nPenetration = nPenetration;
        this.m_nRecovery = nRecovery;



       SceneManager.LoadScene(strSceneName);
    }



    // 로딩바.
    public IEnumerator CorSceneLoad(string strSceneName)
    {
        yield return null;
        m_strCurSceneName = strSceneName;

        AsyncOperation operation = SceneManager.LoadSceneAsync(strSceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if (CUIManager.Inst.m_cUILoding != null)
            {
                if(CUIManager.Inst.m_cUILoding.m_LodingSlider.value <1.0f)
                {
                    CUIManager.Inst.m_cUILoding.m_LodingSlider.value
                        = Mathf.MoveTowards(CUIManager.Inst.m_cUILoding.m_LodingSlider.value,
                        1.0f,
                        Time.deltaTime);
                }
              if(CUIManager.Inst.m_cUILoding.m_LodingSlider.value >=1.0f && operation.progress >=0.9f)
                {
                    operation.allowSceneActivation = true;
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }

    public string GetCurSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

}
