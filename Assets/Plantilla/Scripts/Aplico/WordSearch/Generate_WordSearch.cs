using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Generate_WordSearch : MonoBehaviour
{
    [Header("Cantidad de filas")] [SerializeField] private int rows; // cantidad de filas

    [Header("Cantidad de columnas")] [SerializeField] private int cols; // cantidad de columnas

    [Header("Espacio entre botones")] [SerializeField] private float tileSize; // espacio entre botones

    [Header("Imagen de cada boton-letra")] public Sprite imagenBoton; // imagen del boton

    ControlAudio _controlAudio;
    Manager_WordSearch _manager;

    [Header("Fuente del texto")] public Font fuente; // fuente del texto

    public List<string> palabrasHorizontales, palabrasVerticales;

    public Dictionary<string, Word> DictionaryHorizontales = new Dictionary<string, Word>();

    public Dictionary<string, Word> DictionaryVerticales = new Dictionary<string, Word>();

    [HideInInspector] public List<char> Listaletras;

    [HideInInspector] public List<GameObject> listaToogles;

    [HideInInspector] public int contadorHorizontales = 0, contadorVertical = 0;

    [Header("Setup Colors")]
    public Color32 fondoSeleccionar;
    public Color32 seleccionarLetra;
    public Color32 fondoDefault;
    public Color32 defaultLetra;
    public Color32 fondoVerdadero;
    public Color32 verdaderoLetra;

    [Header("Setup WordSearch")]
    public float scale;

    public int tamanoTexto;

    public bool estado;

    
    public enum Tipo
    {
        automatico,
        manual
    }

    [Header("Tamano Sopa de letras")]
    public Tipo type;

   

    private void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _manager = FindObjectOfType<Manager_WordSearch>();

        estado = true;
        switch (type)
        {
            // Se calcula un tamano optimo teniendo en cuenta la cantidad de palabras y la longitud de la palabra mas larga;
            case Tipo.automatico:

                float RowH =0,ColH = 0;
                float RowV =0,ColV = 0;
                
                foreach (var horizontales in palabrasHorizontales)
                {
                    if (horizontales.Length > ColH)
                    {
                        ColH = horizontales.Length;
                    }
                }

                foreach (var verticales in palabrasVerticales)
                {
                    if (verticales.Length > RowV)
                    {
                        RowV = verticales.Length;
                    }
                }
                
                
                RowH = palabrasHorizontales.Count+2;
                ColV = palabrasVerticales.Count+2;
                ColH = Mathf.CeilToInt(ColH + (ColH / 2));
                RowV = Mathf.CeilToInt(RowV+(RowV)/2);
                
                /*print(RowH+" - " +ColH);
                print(RowV+" - "+ ColV);*/

                float resultadoX = Mathf.FloorToInt(((RowH + RowV) / 2)+Mathf.Min(RowH,RowV));
                float resultadoY = Mathf.CeilToInt(((ColH + ColV) / 2)+Mathf.Min(ColH,ColV));

                //print(resultadoX +" - "+resultadoY);
                
                rows = (int)(Mathf.Min(resultadoX, resultadoY));
                cols = (int)(Mathf.Max(resultadoX, resultadoY));
                
                break;
            
            case Tipo.manual:
                
                
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(tileSize*cols,tileSize*rows);

        AddToDictionary();
        splitWords();
        GenerateGrid();
        SetRandom();
    }

   
// anañade cada palabra al diccionario (palabra, cantaidad de letras, grupo al que pertenece)
    void AddToDictionary()
    {
        
        for (int i = 0; i < palabrasHorizontales.Count; i++)
        {
            Word palabraTemp = new Word(palabrasHorizontales[i], i + 1);
            DictionaryHorizontales.Add(palabrasHorizontales[i], palabraTemp);
        }
        
        
        for (int i = 0; i < palabrasVerticales.Count; i++)
        {
            Word palabraTemp = new Word(palabrasVerticales[i], i + 1);
            DictionaryVerticales.Add(palabrasVerticales[i], palabraTemp);
        }
    }

    
    /// <summary>
    /// divide cada una de las palabras en caracteres
    /// </summary>
    void splitWords()
    {
       
        for (int i = 0; i < DictionaryHorizontales.Count; i++)
        {
            char[] letrasTemp = DictionaryHorizontales.ElementAt(i).Value.letras;
            foreach (var x in letrasTemp)
            {
                Listaletras.Add(x);
            }
        }
        
        for (int i = 0; i < DictionaryVerticales.Count; i++)
        {
            char[] letrasTemp = DictionaryVerticales.ElementAt(i).Value.letras;
            foreach (var x in letrasTemp)
            {
                Listaletras.Add(x);
            }
        }
    }

    
    /// <summary>
    /// Crea la grilla de fila x columna con elementos nuevos
    /// </summary>
    void GenerateGrid()
    {
        
        GameObject letra = new GameObject("letra");
        GameObject texto_Letra = new GameObject("texto_Letra");

        texto_Letra.AddComponent<Text>();

        texto_Letra.transform.SetParent(letra.transform);
        texto_Letra.GetComponent<Text>().text = "A";
        texto_Letra.GetComponent<Text>().color = defaultLetra;
        texto_Letra.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        texto_Letra.GetComponent<Text>().fontSize = tamanoTexto;;
        texto_Letra.GetComponent<Text>().font = fuente;

        letra.AddComponent<Toggle>();
        letra.AddComponent<Image>();

        letra.GetComponent<Image>().sprite = imagenBoton;
        letra.GetComponent<Image>().color = fondoDefault;
        letra.GetComponent<Image>().SetNativeSize();
        letra.GetComponent<Image>().transform.localScale = new Vector2(scale,scale);
        letra.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        letra.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        letra.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        letra.GetComponent<Toggle>().transition = Selectable.Transition.ColorTint;
        letra.GetComponent<Toggle>().isOn = false;
        
        var newColorBlock = letra.GetComponent<Toggle>().colors;
        newColorBlock.disabledColor = new Color32(255, 255, 255,255);
        letra.GetComponent<Toggle>().colors = newColorBlock;

        texto_Letra.GetComponent<RectTransform>().sizeDelta = letra.GetComponent<RectTransform>().sizeDelta;

        letra.AddComponent<Behaviour_Character>();
        letra.GetComponent<Behaviour_Character>().enabled = true;
        letra.GetComponent<Behaviour_Character>()._manager = _manager;
        letra.GetComponent<Behaviour_Character>()._controlAudio = _controlAudio;
        letra.GetComponent<Behaviour_Character>().tieneRandom = true;
        letra.GetComponent<Behaviour_Character>().calificado = false;

        letra.GetComponent<Behaviour_Character>().fondoSeleccionar = fondoSeleccionar;
        letra.GetComponent<Behaviour_Character>().seleccionarLetra = seleccionarLetra;
        
        letra.GetComponent<Behaviour_Character>().defaultLetra = defaultLetra;
        letra.GetComponent<Behaviour_Character>().fondoDefault = fondoDefault;
        
        letra.GetComponent<Behaviour_Character>().fondoVerdadero = fondoVerdadero;
        letra.GetComponent<Behaviour_Character>().verdaderoLetra = verdaderoLetra;

        letra.AddComponent<BehaviourPuntero>();

        

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject) Instantiate(letra, transform);

                float posX = col * tileSize;
                float posY = row * tileSize;

                listaToogles.Add(tile);

                tile.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, -posY);
                tile.GetComponent<Behaviour_Character>().coordenadaX = (int) (posY/tileSize);
                tile.GetComponent<Behaviour_Character>().coordenadaY = (int) (posX/tileSize);
                tile.name = "Tile " + tile.GetComponent<Behaviour_Character>().coordenadaX + "," + tile.GetComponent<Behaviour_Character>().coordenadaY;
            }
        }

        DestroyImmediate(letra);

        //AsignarABotones();
        
    }
    
    public void AsignarABotones()
    {
        int indexTemp = 0;

        int i = 0;
        while (i < DictionaryHorizontales.Count)
        {
            if (i == 0)
            {
               for (int j = indexTemp; j < indexTemp + DictionaryHorizontales.ElementAt(i).Value.letras.Length; j++)
                {
                    listaToogles[j].GetComponent<Behaviour_Character>().letra =DictionaryHorizontales.ElementAt(i).Value.letras[j];
                    listaToogles[j].GetComponent<Behaviour_Character>().tieneRandom = false;
                    listaToogles[j].transform.GetChild(0).GetComponent<Text>().text =DictionaryHorizontales.ElementAt(i).Value.letras[j].ToString();
                    listaToogles[j].GetComponent<Behaviour_Character>().grupo =DictionaryHorizontales.ElementAt(i).Value.grupo;
                }

                indexTemp = indexTemp + DictionaryHorizontales.ElementAt(i).Value.letras.Length;
            }
            else
            {
                int j = indexTemp;

                while (j < indexTemp + DictionaryHorizontales.ElementAt(i).Value.letras.Length)
                {
                    int k = 0;
                    while (k < DictionaryHorizontales.ElementAt(i).Value.letras.Length)
                    {
                        listaToogles[j].GetComponent<Behaviour_Character>().letra =DictionaryHorizontales.ElementAt(i).Value.letras[k];
                        listaToogles[j].GetComponent<Behaviour_Character>().tieneRandom = false;
                        listaToogles[j].transform.GetChild(0).GetComponent<Text>().text =DictionaryHorizontales.ElementAt(i).Value.letras[k].ToString();
                        listaToogles[j].GetComponent<Behaviour_Character>().grupo =DictionaryHorizontales.ElementAt(i).Value.grupo;

                        k++;
                        j++;
                    }
                }

                indexTemp = indexTemp + DictionaryHorizontales.ElementAt(i).Value.letras.Length;
            }

            i++;
        }
    }

    
    /// <summary>
    /// Genera el random entre palabras verticales y horizontales
    /// </summary>
    public void SetRandom()
    {
       RandomHorizontal();
       //RandomVertical();

    }
    
    public void SetRandom2()
    {
        //RandomHorizontal();
        RandomVertical();

    }
    
    
    /// <summary>
    /// Genera el random de la posicion de cada palabra horizontales
    /// </summary>
    void RandomHorizontal()
    {
        int random;
        
        if (contadorHorizontales<DictionaryHorizontales.Count)
        {
            random = (UnityEngine.Random.Range(0,rows)*cols)+(UnityEngine.Random.Range(0,(cols-DictionaryHorizontales.ElementAt(contadorHorizontales).Value.letras.Length)));
          
            //print("PALABRA: "+DictionaryPalabras.ElementAt(contadorPalabras).Value.palabra + " RANDOM: "+random);
            
            PintarHorizontal(random,contadorHorizontales);
            
        }
        else
        {
            //print("Termino");
            RandomVertical();
        }
        

    }

    
    /// <summary>
    /// Genera el random de la posicion de cada palabra vertical
    /// </summary>
    void RandomVertical()
    {
        int random;

        if (contadorVertical<DictionaryVerticales.Count)
        {
            random = ((UnityEngine.Random.Range(0,
                           (rows - (DictionaryVerticales.ElementAt(contadorVertical).Value.letras.Length))) * cols) +
                      (UnityEngine.Random.Range(0, (cols - 1))));

            //print("RANDOM: "+random);
            PintarVertical(random,contadorVertical);
        }
    }

    
    /// <summary>
    /// ubica cada letra de la palabra horizontal desde la "posicion" generada
    /// </summary>
    /// <param name="posicion"></param>
    /// <param name="indexPalabra"></param>
    public void PintarHorizontal(int posicion,int indexPalabra)
    {
        bool state = false;
        
        int aux = posicion;
        
        

        for (int k = aux; k < (DictionaryHorizontales.ElementAt(indexPalabra).Value.letras.Length) + aux; k++)
        {
            /*print("K: "+k +
                  " - (X: "+ listaToogles[k].GetComponent<Behaviour_N03A0031T>().coordenadaX+
                  ", Y: "+ listaToogles[k].GetComponent<Behaviour_N03A0031T>().coordenadaY);*/
            
            
            if (listaToogles[k].GetComponent<Behaviour_Character>().pintado)// si encuentra un boton ubicado genera el random de nuevo
            {
                state = true;
                break;
            }
           
        }

        if (!state) // 
        {
            for (int j = 0; j < DictionaryHorizontales.ElementAt(indexPalabra).Value.letras.Length; j++)
            {
                listaToogles[posicion].GetComponent<Image>().color = fondoDefault;
                listaToogles[posicion].GetComponent<Behaviour_Character>().pintado = true;
                listaToogles[posicion].GetComponent<Behaviour_Character>().letra =Char.ToUpper(DictionaryHorizontales.ElementAt(indexPalabra).Value.letras[j]);
                listaToogles[posicion].transform.GetChild(0).GetComponent<Text>().text =Char.ToUpper(DictionaryHorizontales.ElementAt(indexPalabra).Value.letras[j]).ToString();
                listaToogles[posicion].transform.GetChild(0).GetComponent<Text>().color = defaultLetra;
                listaToogles[posicion].GetComponent<Behaviour_Character>().grupo =DictionaryHorizontales.ElementAt(indexPalabra).Value.grupo;
                listaToogles[posicion].GetComponent<Behaviour_Character>().tieneRandom = false;

                /*print("PALABRA: "+ DictionaryPalabras.ElementAt(indexPalabra).Value.palabra+
                      " - (X: "+ listaToogles[posicion].GetComponent<Behaviour_N03A0031T>().coordenadaX+
                      ", Y: "+ listaToogles[posicion].GetComponent<Behaviour_N03A0031T>().coordenadaY+
                      ") - FUE PINTADO: "+ listaToogles[posicion].GetComponent<Behaviour_N03A0031T>().pintado);*/
                posicion++;
            }
            contadorHorizontales++;
            SetRandom();
        }
        else
        {
            SetRandom();
        }
    }

    
    /// <summary>
    /// ubica cada letra de la palabra vertical desde la "posicion" generada
    /// </summary>
    /// <param name="posicion"></param>
    /// <param name="indexPalabra"></param>
    public void PintarVertical(int posicion,int indexPalabra)
    {
        bool state = false;
        
        int aux = posicion;

        int indexTemp = posicion;
        
     
        
        for (int k = aux; k < (DictionaryVerticales.ElementAt(indexPalabra).Value.letras.Length) + aux; k++ )
        {
            
         /*print("K: "+k +
                       " - (X: "+ listaToogles[indexTemp].GetComponent<Behaviour_N03A0031T>().coordenadaX+
                       ", Y: "+ listaToogles[indexTemp].GetComponent<Behaviour_N03A0031T>().coordenadaY);*/
            
            if (listaToogles[indexTemp].GetComponent<Behaviour_Character>().pintado)
            {
                state = true;
                
                break;
            }
           
            indexTemp = indexTemp + cols;

            
        }

        if (!state)
        {
            for (int j = 0; j < DictionaryVerticales.ElementAt(indexPalabra).Value.letras.Length; j++)
            {
                listaToogles[posicion].GetComponent<Image>().color = fondoDefault;
                listaToogles[posicion].GetComponent<Behaviour_Character>().pintado = true;
                listaToogles[posicion].GetComponent<Behaviour_Character>().letra =Char.ToUpper(DictionaryVerticales.ElementAt(indexPalabra).Value.letras[j]);
                listaToogles[posicion].transform.GetChild(0).GetComponent<Text>().text = Char.ToUpper(DictionaryVerticales.ElementAt(indexPalabra).Value.letras[j]).ToString();
                listaToogles[posicion].transform.GetChild(0).GetComponent<Text>().color = defaultLetra;
                listaToogles[posicion].GetComponent<Behaviour_Character>().grupo = DictionaryVerticales.ElementAt(indexPalabra).Value.grupo;
                listaToogles[posicion].GetComponent<Behaviour_Character>().tieneRandom = false;

                /*print("PALABRA: "+ DictionaryVerticales.ElementAt(indexPalabra).Value.palabra+
                      " - (X: "+ listaToogles[posicion].GetComponent<Behaviour_N03A0031T>().coordenadaX+
                      ", Y: "+ listaToogles[posicion].GetComponent<Behaviour_N03A0031T>().coordenadaY+
                      ") - FUE PINTADO: "+ listaToogles[posicion].GetComponent<Behaviour_N03A0031T>().pintado);*/
                posicion= posicion+cols;
            }
            contadorVertical++;
            SetRandom2();
        }
        else
        {
            SetRandom2();
        }
    }

    public void resetAll()
    {
        foreach (var toggles in listaToogles)
        {
            toggles.GetComponent<Image>().color = Color.black;
            toggles.GetComponent<Behaviour_Character>().pintado = false;
            toggles.GetComponent<Behaviour_Character>().calificado = false;
            toggles.GetComponent<Toggle>().interactable = true;
            toggles.GetComponent<Toggle>().isOn = false;
            toggles.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            Destroy(toggles);
            
        }
        
         
        
        switch (type)
        {
            // Se calcula un tamano optimo teniendo en cuenta la cantidad de palabras y la longitud de la palabra mas larga;
            case Tipo.automatico:

                float RowH =0,ColH = 0;
                float RowV =0,ColV = 0;
                
                foreach (var horizontales in palabrasHorizontales)
                {
                    if (horizontales.Length > ColH)
                    {
                        ColH = horizontales.Length;
                    }
                }

                foreach (var verticales in palabrasVerticales)
                {
                    if (verticales.Length > RowV)
                    {
                        RowV = verticales.Length;
                    }
                }
                
                
                RowH = palabrasHorizontales.Count+2;
                ColV = palabrasVerticales.Count+2;
                ColH = Mathf.CeilToInt(ColH + (ColH / 2));
                RowV = Mathf.CeilToInt(RowV+(RowV)/2);
                
                /*print(RowH+" - " +ColH);
                print(RowV+" - "+ ColV);*/

                float resultadoX = Mathf.FloorToInt(((RowH + RowV) / 2)+Mathf.Min(RowH,RowV));
                float resultadoY = Mathf.CeilToInt(((ColH + ColV) / 2)+Mathf.Min(ColH,ColV));

                print(resultadoX +" - "+resultadoY);
                
                rows = (int)(Mathf.Min(resultadoX, resultadoY));
                cols = (int)(Mathf.Max(resultadoX, resultadoY));
                
                break;
            
            case Tipo.manual:
                
                
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }


        DictionaryHorizontales = new Dictionary<string, Word>();
        DictionaryVerticales = new Dictionary<string, Word>();
        
        
        contadorVertical = 0;
        contadorHorizontales = 0;
        listaToogles.Clear();
        AddToDictionary();
        splitWords();
        GenerateGrid();
        SetRandom();
        SetRandom2();
    }
}