using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

// building Point.
public class CTower : MonoBehaviour
{
    [SerializeField] private GameObject ins_objTower;
    public GameObject m_objTower { get { return ins_objTower; } }


    [SerializeField] private CTowerUnit cTowerUnit;
    [SerializeField] private GameObject _objTower;


    private bool _bTowerIsOn = false;

    private void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) =>
        { OnPointerDown((PointerEventData)data); });

        eventTrigger.triggers.Add(entry_PointerDown);

    }


    private void OnPointerDown(PointerEventData data)
    {
   
        // 타워가 있으니 타워 생성창은 생성하지 않도록 한다.
        if(cTowerUnit !=null)
        {
            return;
        }

        if (gameObject.transform.localPosition.y >= 20.0f)
        {
            _bTowerIsOn = false;
        }
        else
        {
            _bTowerIsOn = true;
        }
        StartCoroutine(CUIManager.Inst.CorTowerBattle(gameObject.transform, _bTowerIsOn));
        CUIManager.Inst.m_cUITowerBattle.transform.position = data.position;

        StartCoroutine(CorTowerCheck());
    }

    private IEnumerator CorTowerCheck()
    {
        if (cTowerUnit == null)
        {
            cTowerUnit = gameObject.GetComponentInChildren<CTowerUnit>();
            yield break;
        }

        yield return new WaitForSeconds(2.0f);
    }

}


