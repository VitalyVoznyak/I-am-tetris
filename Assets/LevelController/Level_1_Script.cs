using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Level_1_Script : MonoBehaviour
{
// PS фаза - Gameobject содержащий в себе группу  дочерних фишиш-кубиков,
//    при прохождении одной фазы запускается следущая и так до конца уровня
    public delegate void EndPhaseEvent();//делегат для события завершения фазы
    public event EndPhaseEvent endPhase; 

    public int howManyPhases;//как много фаз в уровне
    public GameObject phasesParent;//родительский объект для всех фаз
    public int phaseCount; //какая сейчас фаза
    public GameObject[] phases;//массив фаз

    

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
        if ( AllFinishCubeActivated() == true)
        {
            forCheck = false;
            EndPhase();
        }
    }

    void EndPhase()
    {
        endPhase?.Invoke();//сообщаем всем подписанным объектам о переходе в следующую фазу

        phases[phaseCount]?.SetActive(false);// выключаем пройденную фазу

        phaseCount++;// увеличиваем номер текйщей фазы

        phases[phaseCount]?.SetActive(true);// включаем нужную фазу
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
}
