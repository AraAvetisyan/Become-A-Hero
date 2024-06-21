using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class StopGameForAdd : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    int stopgame;
    float volume;

    [DllImport(dllName: "__Internal")]
    private static extern bool ShowAdd();


    private void Start()
    {
        ShowAdd();
    }
    private void Update()
    {
        if (stopgame == 1)
        {
            //game stoped
            if (_audioSource.volume != 0)
            {
                volume = _audioSource.volume;
                _audioSource.volume = 0;
            }
            Time.timeScale = 0f;

        }
        if (stopgame == 2)
        {
            //game continue
            if (volume != 0)
            {
                _audioSource.volume = volume;
                volume = 0;
            }
            Time.timeScale = 1;
            stopgame = 0;
        }
       
    }

    
    
    
    public void Stop(string message)
    {
        stopgame=int.Parse(message);
        
    }


  
}
