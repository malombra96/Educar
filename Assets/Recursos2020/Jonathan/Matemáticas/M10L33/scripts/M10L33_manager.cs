using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L33_manager : MonoBehaviour
{
    public List<GameObject> diamonds, coins, starts, questionsDiamonds, questionsCoins, questionsStarts;

    public GameObject Level1,level2,level3;

    public GameObject life1;

    public int lifes1,gift1,gift2,gist3;

    public GameObject aplico, desempeño;

    public GameObject instruccion2, instruccion3,button2,button3;
    bool a, b;

    public M10L33_player player;


    // Start is called before the first frame update
    void Start()
    {
        
        life1.gameObject.SetActive(true);
        Level1.SetActive(true);
        
        level2.SetActive(false);
        
        level3.SetActive(false);

        foreach (var item in diamonds) {
            item.SetActive(true);
        }
        foreach (var item in coins)
        {
            item.SetActive(false);
        }
        foreach (var item in starts)
        {
            item.SetActive(false);
        }

        foreach (var item in questionsDiamonds)
        {
            item.SetActive(false);
        }
        foreach (var item in questionsCoins)
        {
            item.SetActive(false);
        }
        foreach (var item in questionsStarts)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetElement(GameObject element) {

        int index = -1;
        if (diamonds.Contains(element)) {
            index = diamonds.IndexOf(element);
            ShowQuestion(1,index);
            life1.gameObject.SetActive(true);
            Level1.SetActive(true);
            level2.SetActive(false);
            level3.SetActive(false);



        } else if (coins.Contains(element)) {
            index = coins.IndexOf(element);
            ShowQuestion(2, index);
            level2.SetActive(true);
            Level1.SetActive(false);
            level3.SetActive(false);

        } else if (starts.Contains(element)) {
            index = starts.IndexOf(element);
            ShowQuestion(3, index);
            level3.SetActive(true);
            level2.SetActive(false);
            Level1.SetActive(false);
        }

    }

    public void ShowQuestion(int _case,int index) {

        switch (_case) {
            case 1:
                questionsDiamonds[index].SetActive(true);
                break;
            case 2:
             
                questionsCoins[index].SetActive(true);
                break;
            case 3:
            
                questionsStarts[index].SetActive(true);
                break;
        }

    }


    public void NextElement(GameObject x) {
        if (x.name == "Diamonds") {
            player.pausaElementos();

            instruccion2.GetComponent<Image>().enabled = true;
            button2.SetActive(true);
            level2.SetActive(true);
            
            Level1.SetActive(false);
            level3.SetActive(false);

        } else if (x.name == "Coins") {
            
            player.pausaElementos();
            instruccion3.GetComponent<Image>().enabled = true;
            button3.SetActive(true);
            level3.SetActive(true);
            level2.SetActive(false);
            Level1.SetActive(false);
            
        } else if (x.name == "Stars") {
            aplico.SetActive(false);
            desempeño.SetActive(true);
        
        }

    }
    
    public void RestLifes() {
        if (lifes1 < life1.transform.childCount) {
            lifes1++;
            if (lifes1 == life1.transform.childCount)
            {
                aplico.SetActive(false);
                desempeño.SetActive(true);

            }
            else {
                life1.transform.GetChild(lifes1).gameObject.SetActive(true);   
            }   
        }
    }

    public void AddGifts(int level) {
        switch (level)
        {
            case 1:
                if (gift1< Level1.transform.childCount)
                {
                    
                    Level1.transform.GetChild(gift1).gameObject.SetActive(true);
                    gift1++;
                }
                    
                break;
            case 2:
                if (gift2 < level2.transform.childCount)
                {
                    
                    level2.transform.GetChild(gift2).gameObject.SetActive(true);
                    gift2++;
                }
                    
                break;
            case 3:
                
                if (gist3 < level3.transform.childCount)
                {
                    
                    level3.transform.GetChild(gist3).gameObject.SetActive(true);
                    gist3++;
                }
                    
                break;
        }
    }
}
