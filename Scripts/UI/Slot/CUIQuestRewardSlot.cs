using UnityEngine;
using UnityEngine.UI;

public class CUIQuestRewardSlot : MonoBehaviour
{

    [SerializeField] private Image ins_ImgItem;
    [SerializeField] private Text ins_txtCoin;

    public void SetSlot(string strPath, int nCoin)
    {
        ins_ImgItem.sprite = CResourceLoader.Load<Sprite>(CDataManager.Inst.m_strItemProfilePath+strPath);

        ins_txtCoin.text = nCoin.ToString();
    }
}
