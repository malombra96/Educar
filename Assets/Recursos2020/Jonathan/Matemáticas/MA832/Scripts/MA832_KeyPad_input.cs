using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MA832_KeyPad_input : MonoBehaviour
{
    ControlAudio _controlAudio;
    
    [Header("Input Texto")]public InputField _InputName;
    private string _name; // name
    private bool _firstWord = true; // State first word
    private int _contadorWords; // Contador de letras ingresadas
    [Header("Lista de teclas disponibles")]public List<Button> _words; // Lista de letras del teclado 
    [Header("Cantidad de caracteres")] public int _limitCharacter;
    
    [Header("Function Buttons")]
    public Button _backspace;
    public Toggle _mayus;
    public Button _save;
    public Button _space;
    public Button _clearAll;

    private Animator _animator;

    [Header("Fuente del texto")] public Font fuente;
    [Header("Tamaño de la fuente")] public int size;


    /// <summary>
    /// Obtiene controlador de animaciones y ejecuta animacion open
    /// </summary>
    private void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _animator = GetComponent<Animator>();
        _animator.Play("KeyBoard_in");
    }

    private void Start()
    {
        // Asigna la fuente y tamaño a c/u tecla
        foreach (var letter in _words)
        {
            letter.transform.GetChild(0).GetComponent<Text>().font = fuente;
            letter.transform.GetChild(0).GetComponent<Text>().fontSize = size;
        }
        
        /// Inicializa variables locales y desactiva el boton enter
        _name = "";
        _contadorWords = 0;
        _save.gameObject.SetActive(false);
        
        // Asigna el metodo que se ejecuta al presionar c/u tecla 
        foreach (var word in _words)
        {
            // Vocales 
            if (word.transform.childCount < 2)
            {
                word.onClick.AddListener(delegate { GetWord(word.name);}); // Asigna a cada tecla o letra la funcion de escritura
            }
            else // Consonantes y simbolos
            {
                word.onClick.AddListener(delegate { GetWord(word,word.name);}); // Asigna a cada tecla o letra la funcion de escritura
            }
            
            
        }
        
        // Inicializa el teclado en altas        
        _mayus.isOn = true;
        MayusWord(_mayus.isOn);
        LettersToUpper();
        
        // Asigna los metodos particulares para las teclas especiales
        _backspace.onClick.AddListener(DeleteLetter);
        _clearAll.onClick.AddListener(DeleteWord);
        _mayus.onValueChanged.AddListener(delegate { MayusWord(_mayus.isOn); });
        _space.onClick.AddListener(delegate { GetWord(" ");}); 
       
    }

    /// <summary>
    /// Obtener la letra del boton presionado [c]
    /// </summary>
    /// <param name="c"></param>
    private void GetWord(string c)
    {   
        
        _controlAudio.PlayAudio(0);
        
        // Para evitar que el primer caracter sea "space"
        if (_firstWord && c.Contains(" ")) print("if first");
        
        else
        {
            if (_contadorWords < _limitCharacter) // Limite de caracteres 
            {            
                if (_firstWord) 
                {
                    c = c.ToUpper(); // Poner en altas el primer caracter
                    _firstWord = false; 
                    
                    _mayus.isOn = false; // Cambiar el estado del toggle mayus
                    MayusWord(_mayus.isOn);
                }

           

                _name = string.Concat(_name, c); // Concat caracters
                _contadorWords++;
            }
        
        
            // Identifica que ya por lo menos un (1) caracter presionado y activa el boton enter
            if (_contadorWords > 0)
            {
                _save.gameObject.SetActive(true);
            } 
        }

        
    }
    
    /// <summary>
    /// Obtener la letra del boton presionado [c] o activar el subKey
    /// </summary>
    /// <param name="c"></param>
    private void GetWord(Button b,string c)
    {

        if (!b.gameObject.GetComponent<KeyPressed>().hold) // Evalua si es la tecla con subteclas solo fue presionada [No hold]
        {
            _controlAudio.PlayAudio(0);
 
            if (_firstWord && c.Contains(" ")) print("if first");
        
            else
            {
                if (_contadorWords < _limitCharacter) // Limite de caracteres 
                {            
                    if (_firstWord) 
                    {
                        c = c.ToUpper(); // Poner en altas el primer caracter
                        _firstWord = false;
                        
                        _mayus.isOn = false;
                        MayusWord(_mayus.isOn);
                        //LettersToLower();
                    }

           

                    _name = string.Concat(_name, c); // Concat caracters
                    _contadorWords++;
                }
        
        
                if (_contadorWords > 0)
                {
                    _save.gameObject.SetActive(true);
                } 
            }      
        }
        
    }

    /// <summary>
    /// Funcion para borrar c/u caracter 
    /// </summary>
    void DeleteLetter()
    {
        _controlAudio.PlayAudio(0);
        
        if ((_name.Length - 1) == 0) // Activar primer digito en Altas
        {
            _save.gameObject.SetActive(false);
            _firstWord = true;
            
            _mayus.isOn = true;
            MayusWord(_mayus.isOn);
            
        }

        if (_name.Length > 0)
        {
            _name = _name.Remove(_name.Length - 1);
            _contadorWords--;
        }
        

    }

    /// <summary>
    /// Funcion para borrar el texto completo
    /// </summary>
    void DeleteWord()
    {
        _controlAudio.PlayAudio(0);
        _contadorWords = 0;
        _name = "";
        LettersToUpper();
        _firstWord = true;
        _save.gameObject.SetActive(false);
    }

    /// <summary>
    /// Para activar el toggle en altas
    /// </summary>
    /// <param name="state"></param>
    void MayusWord(bool state)
    {
        _controlAudio.PlayAudio(0);
        
        if (state)
        {
            LettersToUpper();
            _mayus.transform.GetChild(0).GetComponent<Image>().color =new Color32(237,120,7,255);
        }
        else
        {
            LettersToLower();
            _mayus.transform.GetChild(0).GetComponent<Image>().color =new Color32(87,87,87,255);
        }
    }

    /// <summary>
    /// Para activar c/u teclas en altas
    /// </summary>
    void LettersToUpper()
    {
        foreach (var letter in _words)
        {
            string w =  letter.name;
            w= w.ToUpper();
            letter.name = w;
                
            string s =  letter.transform.GetChild(0).GetComponent<Text>().text;
            s= s.ToUpper();
            letter.transform.GetChild(0).GetComponent<Text>().text = s;

        }
    }

    /// <summary>
    /// Para activar c/u teclas en bajas
    /// </summary>
    void LettersToLower()
    {
        foreach (var letter in _words)
        {
            string w =  letter.name;
            w= w.ToLower();
            letter.name = w;
                
            string s =  letter.transform.GetChild(0).GetComponent<Text>().text;
            s= s.ToLower();
            letter.transform.GetChild(0).GetComponent<Text>().text = s;
        }
    }

    private void Update()
    {
        _InputName.text = _name; // Asign text to input
    }

   
    
    
}
