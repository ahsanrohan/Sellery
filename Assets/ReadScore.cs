using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("Player Score"));
    }
    
}
