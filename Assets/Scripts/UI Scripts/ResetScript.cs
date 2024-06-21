using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    public void Reset()
    {
        PlayerPrefs.DeleteAll();

    }
}
