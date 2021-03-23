using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class M8A39_manager : MonoBehaviour
{

    public List<GameObject> groupClues, groupCluesCanvas, keys ,questions,enemies;
    public M8A39_player player;
    public GameObject life,aplico_1,aplico_2,desempeño;
    public int counterLife,cantidadX;
    public Text cantidad;

    public void GetClue(GameObject currentClue) {
        GameObject currentKey = currentClue.GetComponent<M8A39_clue>().key.gameObject;
        for (int i = 0; i< groupCluesCanvas.Count; i++) {
            for (int j = 0; j < groupCluesCanvas[i].transform.childCount; j++) {
                if (groupCluesCanvas[i].transform.GetChild(j).name == currentClue.name) {
                    groupCluesCanvas[i].transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
        int indexClue = currentKey.GetComponent<M8A39_keys>().clues.IndexOf(currentClue);
        currentKey.transform.GetChild(indexClue).gameObject.SetActive(true);
        ShowQuestion(currentKey);
    }

    public void ShowQuestion(GameObject currentKey) {
        
        int indexKey = keys.IndexOf(currentKey);
        int counter = 0;
        for (int i = 0; i < currentKey.transform.childCount;i++) {
            if (currentKey.transform.GetChild(i).gameObject.activeSelf) {
                counter++;
            }
        }

        if (counter == currentKey.transform.childCount) {
            currentKey.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            questions[indexKey].SetActive(true);
            player.canMove = false;
            foreach (var item in enemies) {
                item.GetComponent<M8A39_enemy>().canMove = false;
            }
        }
    }

    public void ShowNextClues(GameObject currentQuestion) {
        int indexQuestion = questions.IndexOf(currentQuestion);
        print(indexQuestion);
        currentQuestion.SetActive(false);
        if (indexQuestion < questions.Count-1) {
            groupClues[indexQuestion + 1].SetActive(true);
        }
        else if (indexQuestion == questions.Count-1) {

            aplico_1.SetActive(false);
            aplico_2.SetActive(true);

        }
        foreach (var item in enemies)
        {
            item.GetComponent<M8A39_enemy>().canMove = true;
            item.transform.position = item.GetComponent<M8A39_enemy>().initialPosition;
        }
        
    }

    public void RestLife() {
        print("xsa");
        if (counterLife >= 0) {
            counterLife--;
            life.transform.GetChild(counterLife).gameObject.SetActive(false);
        }
        if (counterLife == 0) {
            aplico_1.SetActive(false);
            desempeño.SetActive(true);
            
        }
    }

    public void AddCantidad(int x) { 
        cantidadX = cantidadX + x;
        cantidad.text = cantidadX.ToString();
    }
}

