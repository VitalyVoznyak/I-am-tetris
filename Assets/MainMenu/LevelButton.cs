using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    public int numbOfLevel;

    public GameObject startButton;
    public void OnClick()
    {
        foreach(GameObject levelButtons in GameObject.FindGameObjectsWithTag("ButtonMenu"))
        {
            if (this.gameObject != levelButtons) { levelButtons.SetActive(false); }
        }

        startButton.SetActive(true);

        StartCoroutine(StartLevel());

        IEnumerator StartLevel()

        {
            yield return new WaitForSeconds(0.2f);

            SceneManager.LoadScene(numbOfLevel);
        }
        
    }
}
