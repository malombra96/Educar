using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A39_manger3 : MonoBehaviour
{
    public List<GameObject> groupInput, goals, barreras, tanksGroup;
    public M8A39_player2 player;
    public GameObject life, aplico_1, aplico_2;
    public int counterLife, countergoals, cantidadX;
    public Text cantidad;
    public GameObject mision;

    public void GetInputGroup(GameObject barrera)
    {
        foreach (var item in groupInput)
        {
            item.SetActive(false);
        }
        int index = barreras.IndexOf(barrera);
        groupInput[index].SetActive(true);
        player.canMove = false;
    }

    public void RestLife()
    {
        if (counterLife > 0)
        {
            counterLife--;
            life.transform.GetChild(counterLife).gameObject.SetActive(false);
        }
        if (counterLife == 0)
        {
            aplico_1.SetActive(false);
            aplico_2.SetActive(true);
            print("nesx");
        }
        player.canMove = true;
    }

    public void AddGoals(bool value, GameObject currentGroup)
    {

        StartCoroutine(x(value, currentGroup));


    }
    IEnumerator x(bool value, GameObject currentGroup)
    {
        yield return new WaitForSeconds(1);
        currentGroup.SetActive(false);
        countergoals++;
        int index = groupInput.IndexOf(currentGroup);
        if (value)
        {
            goals[index].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            
            GameObject currentTank = tanksGroup[index];
            for (int i = 0; i < currentTank.transform.childCount; i++)
            {
                currentTank.transform.GetChild(i).GetComponent<M8A39_tank>().visionRadius = currentTank.transform.GetChild(i).GetComponent<M8A39_tank>().visionRadius + 4f;
                currentTank.transform.GetChild(i).GetComponent<M8A39_tank>().speed = currentTank.transform.GetChild(i).GetComponent<M8A39_tank>().speed + 2.5f;
            }
        }
        player.canMove = true;

        if (countergoals == groupInput.Count)
        {
            //aplico_1.SetActive(false);
            //aplico_2.SetActive(true);
            mision.SetActive(true);
        }
    }

    public void AddCantidad(int x)
    {
        cantidadX = cantidadX + x;
        cantidad.text = cantidadX.ToString();
    }
}

