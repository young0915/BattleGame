using System.Collections;
public class CMainScene : CSeneObject
{

    protected override void OnSceneAwake()
    {
        base.OnSceneAwake();
        CSoundManager.Inst.SetPlayMusic(0);
    }

    protected override void OnSceneStart()
    {
        base.OnSceneStart();

        CCameraManager.Inst.SetCameraView(EmCameraType.Start);
        StartCoroutine(CorGameStart());
        CDBManager.Inst.Install();
        CDBPlayerInfo.Inst.Init();
        CLevelManager.Inst.Install();
        CQuestManager.Inst.Install();


    }


    private IEnumerator CorGameStart()
    {
        yield return StartCoroutine(CUIManager.Inst.CorStart());
    }

}
