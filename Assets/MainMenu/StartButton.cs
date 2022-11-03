using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
   public void OnClick()
   {
        buttons[1].SetActive(true);

        if (PlayerPrefs.GetInt("Level_Squl") == 1)
        {buttons[2].SetActive(true);}

        if (PlayerPrefs.GetInt("Level_Girl") == 1)
        {buttons[3].SetActive(true);}

        if (PlayerPrefs.GetInt("Level_Like") == 1)
        {buttons[4].SetActive(true);}

        if (PlayerPrefs.GetInt("Level_Cup") == 1)
        {buttons[5].SetActive(true);}

        if (PlayerPrefs.GetInt("Level_Arrow") == 1)
        {buttons[6].SetActive(true);}

        if (PlayerPrefs.GetInt("Level_Heart") == 1)
        {buttons[7].SetActive(true);}
        this.gameObject.SetActive(false) ;
   }
   public GameObject[] buttons;
}

//Squl
//girl 
//like
//cup
//heart
//arrow
//clocks

