using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionVariables : MonoBehaviour
{
    public float invert_true=1f;

    public void invert_val()
    {
        invert_true = -invert_true;
        Debug.Log((invert_true).ToString());
        PlayerPrefs.SetFloat("inv_val", invert_true);
    }
    
}
