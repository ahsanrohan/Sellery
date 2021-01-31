using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public Text playerName;
    public Text placeHolder;
    public SceneSwitcher sceneManager;
    private string name = "";

    public void checkName(string scene)
    {
        if (name == "")
        {
            placeHolder.text = "Name please!";
            return;
        }
        sceneManager.switchScene(scene);
        //GetComponent<SceneSwitcher>().switchScene(scene);
    }
    public void setName()
    {
        name = playerName.text.ToString();
    }
}
