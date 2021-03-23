using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A31_manager : MonoBehaviour
{

    public GameObject cube1, cube2;

    public List<GameObject> cubes_a, cubes_b, allCubes;
    public int counter,punt;

    public GameObject grid;

    ControlAudio audio;

    ControlPuntaje puntaje;

    ControlNavegacion navegacion;

    M8A31_stopWatch _Stopwatch;

    bool stopGame, firstTime;
    public Button _buttonStart;
    public Text puntuacion;

    private void Awake()
    {
        _Stopwatch = GameObject.FindObjectOfType<M8A31_stopWatch>();
        audio = GameObject.FindObjectOfType<ControlAudio>();
        puntaje = GameObject.FindObjectOfType<ControlPuntaje>();
        navegacion = GameObject.FindObjectOfType<ControlNavegacion>();
        stopGame = true;
    }
    private void Start()
    {
        firstTime = true;
        _buttonStart.onClick.AddListener(StartGame);

    }

    private void Update()
    {
        if (stopGame)
        {
            if (_Stopwatch.timeStart > 300f)
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
        _buttonStart.interactable = false;
        foreach (var item in allCubes)
        {
            item.GetComponent<M8A31_card>().StartGame();
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
            foreach (var item in allCubes) {
                item.GetComponent<Button>().interactable = false;
            }
            StartCoroutine(x());

        }



    }

    IEnumerator x()
    {
        yield return new WaitForSeconds(2.5f);

        if (cube1.GetComponent<M8A31_card>().coupleCard == cube2)
        {
            print("+1");
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
        yield return new WaitForSeconds(2.5f);
        punt++;
        puntuacion.text = punt.ToString();
        audio.PlayAudio(1);
        cubes_a.Add(cube1);
        cubes_b.Add(cube2);
        cube1.SetActive(false);
        cube2.SetActive(false);
        cube1.GetComponent<M8A31_card>().subCard.SetActive(false);
        cube2.GetComponent<M8A31_card>().subCard.SetActive(false);

        cube1 = null;
        cube2 = null;
        foreach (var item in allCubes)
        {
            item.GetComponent<Button>().interactable = true;
        }
    }

    IEnumerator ReturnCubes()
    {
        yield return new WaitForSeconds(3f);
        audio.PlayAudio(2);
        cube1.GetComponent<Animator>().Play("cubeIdle");
        cube1.GetComponent<M8A31_card>().subCard.GetComponent<Animator>().Play("subCubeIdle");
        cube1.GetComponent<Button>().interactable = true;
        cube2.GetComponent<Animator>().Play("cubeIdle");
        cube2.GetComponent<Button>().interactable = true;
        cube2.GetComponent<M8A31_card>().subCard.GetComponent<Animator>().Play("subCubeIdle");
        cube1 = null;
        cube2 = null; 
        
        foreach (var item in allCubes)
        {
            item.GetComponent<Button>().interactable = true;
        }
    }

    public void NextLayout()
    {

        if ((allCubes.Count / 2) == counter)
        {
            navegacion.Forward(2.5f);
        }
    }

    public void ReviewCubes() {
        for (int i = 0; i< cubes_a.Count;i++) {
            cubes_a[i].GetComponent<M8A31_card>().subCard.gameObject.transform.SetParent(grid.transform);
            cubes_b[i].GetComponent<M8A31_card>().subCard.gameObject.transform.SetParent(grid.transform);

            cubes_a[i].GetComponent<M8A31_card>().subCard.gameObject.SetActive(true);
            cubes_b[i].GetComponent<M8A31_card>().subCard.gameObject.SetActive(true);

            cubes_a[i].GetComponent<M8A31_card>().subCard.gameObject.GetComponent<Animator>().Play("subCube_Rotate");
            cubes_b[i].GetComponent<M8A31_card>().subCard.gameObject.GetComponent<Animator>().Play("subCube_Rotate");
        }
    }

    public void ResetAll() {

        if (firstTime)
        {
            punt = 0;
            puntuacion.text = "0";
            counter = 0;
            for (int i = 0; i < allCubes.Count; i++)
            {
                allCubes[i].SetActive(true);
                allCubes[i].GetComponent<M8A31_card>().a = false;
                allCubes[i].GetComponent<M8A31_card>().subCard.gameObject.transform.SetParent(gameObject.transform);
                allCubes[i].GetComponent<M8A31_card>().subCard.gameObject.SetActive(true);
                allCubes[i].GetComponent<M8A31_card>().subCard.gameObject.GetComponent<Animator>().Play("subCubeIdle");
                allCubes[i].GetComponent<Animator>().Play("cubeIdle");
                allCubes[i].GetComponent<Button>().enabled = true;
                allCubes[i].GetComponent<Button>().interactable = false;
                allCubes[i].GetComponent<Image>().sprite = allCubes[i].GetComponent<M8A31_card>()._sprite[0];

            }


            _buttonStart.interactable = false;

            puntaje.resetScore();
            _buttonStart.interactable = true;
            _Stopwatch.timerActive = false;
            _Stopwatch.textBox.text = "00:00";
            _Stopwatch.seconds = 0;
            _Stopwatch.timeStart = 0;
            stopGame = true;
        }
        
    }
}

