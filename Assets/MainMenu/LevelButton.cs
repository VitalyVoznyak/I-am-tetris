using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    public Scene level;
    public void OnClick()
    {
        switch (this.gameObject.name)
        {
            case "Level 1 Button":
                                      SceneManager.LoadScene("Level_Squl");
            break;
            case "Level 2 Button":
                                      SceneManager.LoadScene("Level_Girl");
            break;
            case "Level 3 Button":
                                      SceneManager.LoadScene("Level_Like");
            break;
            case "Level 4 Button":
                                      SceneManager.LoadScene("Level_Cup");
            break;
            case "Level 5 Button":
                                      SceneManager.LoadScene("Level_Arrow");
            break;
            case "Level 6 Button":
                                      SceneManager.LoadScene("Level_Heart");
            break;
            case "Level 7 Button":
                                      SceneManager.LoadScene("Level_Clocks");
            break;
        }  
    }
}
