using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSoundManager : CSingleton<CSoundManager>
{
    [SerializeField] private List<AudioSource> _listAudioSource = new List<AudioSource>();

    public void SetPlayMusic(int nId)
    {
        _listAudioSource[nId].Play();
        StartCoroutine(CorSetMusic(nId));
    }

    private IEnumerator CorSetMusic(int nId)
    {
        if(nId ==0)
        {
            _listAudioSource[0].Stop();
        }
        else
        {
            _listAudioSource[nId - 1].Stop();
        }

        yield return new WaitForSeconds(0.5f);
        _listAudioSource[nId].Play();
    }

    public AudioSource GetMusic(int nMusic)
    {
        return _listAudioSource[nMusic];
    }
 

}
