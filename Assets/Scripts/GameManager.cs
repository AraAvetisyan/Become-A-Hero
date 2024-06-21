using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mobileUI;
    [SerializeField] private GameObject tutorialForMobile, tutorialForPC;
    public static Action<bool> IsMobileON;
    public static Action<bool> StopGameForAdd;
    

    private void Start()
    {
       
            //StopGameForAdd?.Invoke(true);
           
        
        if(isMobile() == false)
        {
            IsMobileON?.Invoke(false);
            mobileUI.SetActive(false);
            PlayerPrefs.SetInt("IsMobile", 0);
            tutorialForMobile.SetActive(false);
            tutorialForPC.SetActive(true);
        }
        else if (isMobile() == true)
        {
            IsMobileON?.Invoke(true);
            mobileUI.SetActive(true);
            PlayerPrefs.SetInt("IsMobile", 1);
            tutorialForMobile.SetActive(true);
            tutorialForPC.SetActive(false);
        }

    }
   

    #region WebGL is on mobile check
    [DllImport(dllName: "__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
             return IsMobile();
#endif
        return false;
    }
    #endregion

}
