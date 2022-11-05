using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

public class Level_Controller : MonoBehaviour
{
// PS фаза - Gameobject содержащий в себе группу  дочерних фишиш-кубиков,
//    при прохождении одной фазы запускается следущая и так до конца уровня
    public delegate void EndPhaseEvent();//делегат для события завершения фазы
    public delegate void RestartPhase();//делегат для рестарта фазы
    public delegate void EndLastPhaseEvent();//делегат для окончания последней фазы
    
    public event EndPhaseEvent endPhase;
    public event RestartPhase restartPhase;
    public event EndLastPhaseEvent endLastPhase;

    public int howManyPhases;//как много фаз в уровне
    public GameObject phasesParent;//родительский объект для всех фаз
    public int phaseCount; //какая сейчас фаза
    public GameObject[] phases;//массив фаз
    public bool levelCompleted;

    void Start()
    {
        phases = new GameObject[40];

        phases[0] = null; //нет фазы под номером 0

        phaseCount = 1;  //сразу включаем первую фазу

        for(int i = 1; i <= howManyPhases; i++ ) //заполняем массив фаз
        {
            phases[i] = phasesParent.transform.Find($"Phase{i}").gameObject;
        }  
        
        foreach(GameObject phase in phases) // выключаем все фазы кроме первой
        {
            if (phase != phases[1] && phase != phases[0])
            {
                phase.SetActive(false);
            }
        }
    }

    
    void Update()
    {
        if (levelCompleted == false)
        {
             if ( AllFinishCubeActivated() == true )
            {
               forCheck = false;
               EndPhase();
            }
        }
       


        if (Input.GetKey(KeyCode.Backspace) == true){ EndLastPhase();endLastPhase.Invoke();}
    }
    public void onPressRestartButton()
    {
        restartPhase.Invoke();
    }

    void EndPhase()
    {
        endPhase?.Invoke();//сообщаем всем подписанным объектам о переходе в следующую фазу

        phases[phaseCount]?.SetActive(false);// выключаем пройденную фазу

        phaseCount++;// увеличиваем номер текйщей фазы

        if (phaseCount <= howManyPhases)
        {
            phases[phaseCount]?.SetActive(true);// включаем нужную фазу
        }
        else
        {
            EndLastPhase();
            endLastPhase.Invoke();
        }
    }
   
    
    void EndLastPhase()
    {
        levelCompleted = true;
        GameObject[] allRedCubes = GameObject.FindGameObjectsWithTag("RedCube");
        foreach (GameObject redCube in allRedCubes)
        {
           redCube.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0,255,0,255);
        }

        PlayerPrefs.SetInt($"{this.gameObject.scene.name}",1);
        StartCoroutine(GoBackToMenu());
    }

    [SerializeField] bool forCheck;  
    public FinishCube[] finishCubes;

     bool AllFinishCubeActivated()//проверяет, вставленны ли кубы на свои места, возвращает true или false
    {
        finishCubes = phases[phaseCount].GetComponentsInChildren<FinishCube>();

          forCheck = true;

          foreach(FinishCube finishCube in finishCubes)
          {
               if(finishCube.activated == false)
               {
                    forCheck = false;
               }
               
          }
          return forCheck;  
    }
    IEnumerator GoBackToMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

}
