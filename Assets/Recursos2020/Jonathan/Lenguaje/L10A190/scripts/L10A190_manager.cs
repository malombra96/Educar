using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10A190_manager : MonoBehaviour
{
    public List<GameObject> groupClues, groupCluesCanvas, keys, questions, enemies;
    public L10A190_player player;
    public GameObject life, aplico_1, desempeño;
    public int counterLife, cantidadX;
    public Text cantidad;
    int counter = 0;
    public void GetClue(GameObject currentClue)
    {
        //currentClue.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        currentClue.GetComponent<L10A190_clue>().pregunta.SetActive(true);
        player.canMove = false;
        foreach (var item in enemies)
        {
            item.GetComponent<L10A190_enemy>().canMove = false;
        }
        GameObject currentKey = currentClue.GetComponent<L10A190_clue>().marca;
        currentKey.SetActive(true);
        //for (int i = 0; i < groupCluesCanvas.Count; i++)
        //{
        //    for (int j = 0; j < groupCluesCanvas[i].transform.childCount; j++)
        //    {
        //        if (groupCluesCanvas[i].transform.GetChild(j).name == currentClue.name)
        //        {
        //            groupCluesCanvas[i].transform.GetChild(j).gameObject.SetActive(true);
        //        }
        //    }
        //}
        //int indexClue = currentKey.GetComponent<L10A190_key>().clues.IndexOf(currentClue);
        //currentKey.transform.GetChild(indexClue).gameObject.SetActive(true);
        //counter++;
        ShowQuestion(currentKey);
        
    }

    public void ShowQuestion(GameObject currentKey)
    {
        currentKey.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        //int indexKey = keys.IndexOf(currentKey);
        //int counter = 0;
        //for (int i = 0; i < currentKey.transform.childCount; i++)
        //{
        //    if (currentKey.transform.GetChild(i).gameObject.activeSelf)
        //    {
        //        counter++;
        //    }
        //}

        //if (counter == 3)
        //{
        //    currentKey.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        //    questions[0].SetActive(true);
        //    player.canMove = false;
        //    foreach (var item in enemies)
        //    {
        //        item.GetComponent<L10A190_enemy>().canMove = false;
        //    }
        //}


    }

    public void ShowNextClues(GameObject currentQuestion)
    {
        int indexQuestion = questions.IndexOf(currentQuestion);
        print(indexQuestion);
        currentQuestion.SetActive(false);
        if (indexQuestion < questions.Count - 1)
        {
            groupClues[indexQuestion + 1].SetActive(true);
        }
        else if (indexQuestion == questions.Count - 1)
        {

            aplico_1.SetActive(false);
            desempeño.SetActive(true);

        }
        foreach (var item in enemies)
        {
            item.GetComponent<L10A190_enemy>().canMove = true;
            item.transform.position = item.GetComponent<L10A190_enemy>().initialPosition;
        }

    }

    public void RestLife()
    {
        print("xsa");
        if (counterLife >= 0)
        {
            counterLife--;
            life.transform.GetChild(counterLife).gameObject.SetActive(false);
        }
        if (counterLife == 0)
        {
            aplico_1.SetActive(false);
            desempeño.SetActive(true);

        }
    }

    public void AddCantidad(int x)
    {
        cantidadX = cantidadX + x;
        cantidad.text = cantidadX.ToString();
    }

    public void pasar() {
        counter++;
        if (counter == 3) {
            aplico_1.SetActive(false);
            desempeño.SetActive(true);
        }
    }
}
