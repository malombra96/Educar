using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A81_managerCard : MonoBehaviour
{
    public GameObject cube1, cube2;

    public List<GameObject> cubes_a, cubes_b, allCubes, subCube_a, subCube_b;
    public int counter, punt;

    public GameObject grid;

    public ControlAudio audio;

    public ControlPuntaje puntaje;

    public ControlNavegacion navegacion;

    M9A81_StopClock _Stopwatch;

    bool stopGame, firstTime;
    public Button _buttonStart;
    public Text puntuacion;


    private void Awake()
    {
        _Stopwatch = GameObject.FindObjectOfType<M9A81_StopClock>();
        
        stopGame = true;
    }
    private void Start()
    {
        firstTime = true;
        _buttonStart.onClick.AddListener(StartGame);
        //punt = 100;
        //puntuacion.text = punt.ToString();
    }

    private void Update()
    {
        if (stopGame)
        {
            if (_Stopwatch.timeLeft <= 0.1f)
            {
                _Stopwatch.timerActive = false;
                stopGame = false;
                foreach (var item in allCubes)
                {
                    item.GetComponent<Button>().interactable = false;
                }
                navegacion.Forward(1);

            }
        }


    }

    public void StartGame()
    {
        audio.PlayAudio(0);
        _buttonStart.interactable = false;
        foreach (var item in allCubes)
        {
            item.GetComponent<M9A81_card>().StartGame();
            item.GetComponent<Button>().interactable = true;
        }

        _Stopwatch.timerButton();
    }
    public void CompararCubos(GameObject cube)
    {
        if (cube1 == null)
        {
            //print("cube1");
            cube1 = cube;
            cube1.GetComponent<Button>().interactable = false;

        }
        else if (cube1 != null && cube2 == null)
        {
            //print("cube2");
            cube2 = cube;
            cube2.GetComponent<Button>().interactable = false;
            foreach (var item in allCubes)
            {
                item.GetComponent<Button>().interactable = false;
            }
            StartCoroutine(x());

        }



    }

    IEnumerator x()
    {
        yield return new WaitForSeconds(1f);

        if (cube1.GetComponent<M9A81_card>().coupleCard == cube2)
        {

            puntaje.IncreaseScore();
            counter++;
            StartCoroutine(hideRightsCubes());
        }
        else
        {
            StartCoroutine(ReturnCubes());
        }
        NextLayout();
    }


    // 79056868360 ahorros bancolombia sergio jimenez asias
    IEnumerator hideRightsCubes()
    {
        yield return new WaitForSeconds(1f);
       
        audio.PlayAudio(1);
        cubes_a.Add(cube1);
        cubes_b.Add(cube2);
        subCube_a.Add(cube1.GetComponent<M9A81_card>().subCard);
        subCube_b.Add(cube2.GetComponent<M9A81_card>().subCard);
        cube1.GetComponent<M9A81_card>().subCard.GetComponent<M9A81_subCube>().review = true;
        cube2.GetComponent<M9A81_card>().subCard.GetComponent<M9A81_subCube>().review = true;
        cube1.SetActive(false);
        cube2.SetActive(false);
        cube1.GetComponent<M9A81_card>().subCard.SetActive(false);
        cube2.GetComponent<M9A81_card>().subCard.SetActive(false);
        cube1.GetComponent<M9A81_card>().subCard.GetComponent<Animator>().SetBool("x", false);
        cube1.GetComponent<M9A81_card>().review = true;
        cube2.GetComponent<M9A81_card>().subCard.GetComponent<Animator>().SetBool("x", false);
        cube2.GetComponent<M9A81_card>().review = true;
        cube1.GetComponent<M9A81_card>().subCard.GetComponent<Image>().sprite = cube1.GetComponent<M9A81_card>().subCard.GetComponent<M9A81_subCube>().imagenes[1];
        cube2.GetComponent<M9A81_card>().subCard.GetComponent<Image>().sprite = cube2.GetComponent<M9A81_card>().subCard.GetComponent<M9A81_subCube>().imagenes[1];
        cube1.SetActive(false);
        cube2.SetActive(false);
        cube1 = null;
        cube2 = null;
        foreach (var item in allCubes)
        {
            item.GetComponent<Button>().interactable = true;
        }
    }

    IEnumerator ReturnCubes()
    {
        yield return new WaitForSeconds(1f);

        audio.PlayAudio(2);
        cube1.GetComponent<Animator>().SetBool("x", false);
        cube2.GetComponent<Animator>().SetBool("x", false);
        cube1.GetComponent<Button>().interactable = true;
        cube2.GetComponent<Button>().interactable = true;
        cube1.GetComponent<M9A81_card>().subCard.GetComponent<Image>().sprite = cube1.GetComponent<M9A81_card>().subCard.GetComponent<M9A81_subCube>().imagenes[0];
        cube2.GetComponent<M9A81_card>().subCard.GetComponent<Image>().sprite = cube2.GetComponent<M9A81_card>().subCard.GetComponent<M9A81_subCube>().imagenes[0];
        cube1 = null;
        cube2 = null;

        foreach (var item in allCubes)
        {
            item.GetComponent<Button>().interactable = true;
        }
    }

    public void NextLayout()
    {

        if ((allCubes.Count / 2) == counter || punt == 10)
        {
            _Stopwatch.timerActive = false;
            navegacion.Forward(2.5f);
        }
    }

    public void ReviewCubes()
    {
        if (firstTime) {
            _Stopwatch.timerActive = false;
            print("si");
            foreach (var item in allCubes)
            {
                item.SetActive(false);
                item.GetComponent<M9A81_card>().subCard.SetActive(false);
            }


            for (int i = 0; i < subCube_a.Count; i++)
            {
                if (subCube_a[i].GetComponent<M9A81_subCube>().review)
                {
                    subCube_a[i].SetActive(true);
                    subCube_a[i].GetComponent<M9A81_subCube>().ReviewSub();
                }
                else
                {
                    subCube_a[i].SetActive(false);
                    subCube_a[i].GetComponent<M9A81_subCube>().review = false;
                }
            }

            for (int i = 0; i < subCube_b.Count; i++)
            {
                if (subCube_b[i].GetComponent<M9A81_subCube>().review)
                {
                    subCube_b[i].SetActive(true);
                    subCube_b[i].GetComponent<M9A81_subCube>().ReviewSub();
                }
                else
                {
                    subCube_b[i].SetActive(false);
                    subCube_b[i].GetComponent<M9A81_subCube>().review = false;
                }
            }

            for (int i = 0; i < cubes_a.Count; i++)
            {
                if (cubes_a[i].GetComponent<M9A81_card>().review)
                {
                    //cubes_a[i].SetActive(true);
                    cubes_a[i].GetComponent<M9A81_card>().subCard.gameObject.transform.SetParent(grid.transform);
                }
                else
                {
                    cubes_a[i].SetActive(false);
                }

                // cubes_a[i].GetComponent<M8A31_card>().subCard.gameObject.GetComponent<Animator>().SetBool("x", true);
                //cubes_b[i].GetComponent<M8A31_card>().subCard.gameObject.GetComponent<Animator>().SetBool("x", true);
            }

            for (int i = 0; i < cubes_b.Count; i++)
            {

                if (cubes_b[i].GetComponent<M9A81_card>().review)
                {
                    //cubes_b[i].SetActive(true);
                    cubes_b[i].GetComponent<M9A81_card>().subCard.gameObject.transform.SetParent(grid.transform);
                }
                else
                {
                    cubes_b[i].SetActive(false);
                }



                // cubes_a[i].GetComponent<M8A31_card>().subCard.gameObject.GetComponent<Animator>().SetBool("x", true);
                //cubes_b[i].GetComponent<M8A31_card>().subCard.gameObject.GetComponent<Animator>().SetBool("x", true);


            }
        }
        

        
    }

    public void ResetAll()
    {

        if (firstTime)
        {
            
            //puntuacion.text = "0";
            counter = 0;
            for (int i = 0; i < allCubes.Count; i++)
            {
                allCubes[i].SetActive(true);
                allCubes[i].GetComponent<M9A81_card>().a = false;
                allCubes[i].GetComponent<M9A81_card>().review = false;
                allCubes[i].GetComponent<M9A81_card>().subCard.gameObject.transform.SetParent(gameObject.transform);
                allCubes[i].GetComponent<M9A81_card>().subCard.gameObject.SetActive(true);
                allCubes[i].GetComponent<M9A81_card>().subCard.gameObject.GetComponent<Animator>().SetBool("x", false);
                allCubes[i].GetComponent<Animator>().SetBool("x",false);
                allCubes[i].GetComponent<Button>().enabled = true;
                allCubes[i].GetComponent<Button>().interactable = true;
                allCubes[i].GetComponent<Image>().sprite = allCubes[i].GetComponent<M9A81_card>()._sprite[0];
                allCubes[i].transform.SetParent(transform);
                allCubes[i].GetComponent<RectTransform>().anchoredPosition = allCubes[i].GetComponent<M9A81_card>().posicion;
                allCubes[i].GetComponent<M9A81_card>().subCard.GetComponent<RectTransform>().anchoredPosition = allCubes[i].GetComponent<M9A81_card>().posicionSub;
                allCubes[i].GetComponent<M9A81_card>().subCard.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                allCubes[i].GetComponent<M9A81_card>().subCard.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                allCubes[i].GetComponent<M9A81_card>().subCard.GetComponent<Image>().sprite = allCubes[i].GetComponent<M9A81_card>().subCard.GetComponent<M9A81_subCube>().imagenes[0];

                allCubes[i].GetComponent<M9A81_card>().subCard.GetComponent<Image>().color = new Color(255,255,255,255);
            }


            _buttonStart.interactable = false;

            puntaje.resetScore();
            _buttonStart.interactable = true;
            _Stopwatch.timerActive = false;
            _Stopwatch.textBox.text = "00:00";
            _Stopwatch.text.text = "02:00";
            
            _Stopwatch.timeStart = 0;
            _Stopwatch.timeLeft = 120;
            stopGame = true;
            cubes_a.Clear();
            cubes_b.Clear();
            subCube_a.Clear();
            subCube_b.Clear();
            punt = 100;
        }

    }
}
