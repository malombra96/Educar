using System;
using System.Collections;
using System.Collections.Generic;
using NCalc;
using UnityEngine;
using UnityEngine.UI;


public class manager_calculadora : MonoBehaviour
{
    public Text textoReferencia, textoPantalla, memory;
    internal ControlAudio controlAudio;
    [HideInInspector]public double numero1, numero2, respuesta, caso, cadena, x;
    public Toggle rad_deg;
    [HideInInspector] public bool open, parentesis, coma;
    public static List<char> opens = new List<char> { char.Parse("{"), char.Parse("["), char.Parse("(") };
    public static List<char> close = new List<char> { char.Parse("}"), char.Parse("]"), char.Parse(")") };
    public Button MS, MC, MR;
    internal void Start()
    {
        x = 0;
        open = false;
        controlAudio = GameObject.FindObjectOfType<ControlAudio>();
        rad_deg.onValueChanged.AddListener(delegate { RadToDeg(); });
        numero1 = 0;
        textoPantalla.text = "0";
        MS.interactable = true;
        MC.interactable = false;
        MR.interactable = false;
    }
   
    private void Update()
    {
        if (textoReferencia.text.Contains("("))
        {
            parentesis = true;

        }
        else
        {
            parentesis = false;
        }
    }

    public void MemoryClear()
    {

        memory.text = "";
        MS.interactable = true;
        MR.interactable = false;
        MC.interactable = false;
    }

    public void MemoryRecall()
    {
        if (memory.text != "")
        {
            textoPantalla.text = memory.text;
        }
    }


    public void MemoryStorage()
    {
        if (textoPantalla.text != "" && textoPantalla.text != "0")
        {
            memory.text = textoPantalla.text;
            MS.interactable = false;
            MR.interactable = true;
            MC.interactable = true;
        }

    }

    public static bool isBalanced(string str)
    {

        Stack<char> stack = new Stack<char>();
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if (opens.Contains(c))
            {
                stack.Push(c);
            }
            else if (close.Contains(c))
            {

                if (stack.Count == 0) return false;

                char lastOpened = stack.Peek();

                if (close.IndexOf(c) == opens.IndexOf(lastOpened))
                {
                    stack.Pop();
                }
                else
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }

    public void RadToDeg()
    {
        controlAudio.PlayAudio(0);
        if (rad_deg.isOn)
        {
            rad_deg.transform.GetComponent<Image>().sprite = rad_deg.GetComponent<BehaviourSprite>()._selection;
        }
        else
        {
            rad_deg.transform.GetComponent<Image>().sprite = rad_deg.GetComponent<BehaviourSprite>()._default;
        }
    }

    public void AddText(string name)
    {
        if (name == "(")
        {
            open = true;
            parentesis = true;
            numero1 = 0;
            numero2 = 0;
           
        }
        if (open)
        {
            if (name == "," && !coma && !textoPantalla.text.Contains(","))
            {
                coma = true;
            }

            if (int.TryParse(name, out _))
            {
                if (coma)
                {   
                    textoPantalla.text = textoPantalla.text +"," + name;
                    coma = false;
                }
                else {
                    if (textoPantalla.text == "0")
                    {
                        textoPantalla.text = "";
                        textoPantalla.text = textoPantalla.text + name;
                    }
                    else {
                        textoPantalla.text = textoPantalla.text + name;
                    }
                    
                }
            }
            else
            {
                if (!coma)
                {
                    if (name == ")")
                    {
                        if (textoPantalla.text != "")
                        {
                            textoReferencia.text = textoReferencia.text + textoPantalla.text + name;
                            textoPantalla.text = "";
                        }
                        else
                        {
                            if (textoReferencia.text == "0")
                            {
                                textoReferencia.text = name;
                            }
                            else {
                                textoReferencia.text = textoReferencia.text + name;
                            }
                            
                        }
                    }
                    else
                    {
                        if (textoReferencia.text == "0")
                        {
                            textoReferencia.text = name;
                        }
                        else
                        {
                            textoReferencia.text = textoReferencia.text + name;
                        }
                       // textoReferencia.text = textoReferencia.text + name;
                    }
                }
            }
        }
        else
        {
            if (name == ",")
            {
                try
                {
                    if (textoPantalla.text.IndexOf(",") > 0)
                    {
                        controlAudio.PlayAudio(2);
                    }
                    else if (!textoPantalla.text.Contains(","))
                    {
                        textoPantalla.text = textoPantalla.text + ",";
                    }
                    else
                    {
                        textoPantalla.text = "0,";
                    }
                }
                catch { }
            }
            else
            {
                if (textoPantalla.text == "0")
                {
                    textoPantalla.text = "";
                }
                textoPantalla.text = textoPantalla.text + name;
            }
        }
    }

    public void AddFunction(String nameFunction)
    {
        
        if (textoReferencia.text.Length > 0 || textoPantalla.text.Length > 0)
        {
            if (textoReferencia.text.Contains("("))
            {
                open = true;
                switch (nameFunction)
                {
                    case "+/-":
                        ChangeSign();
                        break;
                    case "reciproco":
                        Reciproco();
                        break;
                    case "cuadrado":
                        Cuadrado();
                        break;
                    case "raiz":
                        RaizCuadrada();
                        break;
                    case "potencia":
                        potencia();
                        break;
                    case "potenciaBase10":
                        PotenciaBase10();
                        break;
                    case "log":
                        Logaritmo();
                        break;
                    case "ln":
                        Ln();
                        break;
                    case "sen":
                        seno();
                        break;
                    case "cos":
                        coseno();
                        break;
                    case "tan":
                        tangente();
                        break;
                    case "sec":
                        secante();
                        break;
                    case "csc":
                        coscecante();
                        break;
                    case "cot":
                        cotangente();
                        break;
                    case "valorAbsoluto":
                        ValorAbsoluto();
                        break;
                    case "exp":
                        exp();
                        break;
                    case "pi":
                        pi();
                        break;
                    case "euler":
                        euler();
                        break;
                    case "factorial":
                        Factorial();
                        break;
                    case "clear":
                        ClearText();
                        break;
                }
            }
            else
            {
                switch (nameFunction)
                {
                    case "backspace":
                        EraseText();
                        break;
                    case "clear":
                        ClearText();
                        break;
                    case "+/-":
                        ChangeSign();
                        break;
                    case "reciproco":
                        Reciproco();
                        break;
                    case "cuadrado":
                        Cuadrado();
                        break;
                    case "raiz":
                        RaizCuadrada();
                        break;
                    case "potencia":
                        potencia();
                        break;
                    case "potenciaBase10":
                        PotenciaBase10();
                        break;
                    case "log":
                        Logaritmo();
                        break;
                    case "ln":
                        Ln();
                        break;
                    case "sen":
                        seno();
                        break;
                    case "cos":
                        coseno();
                        break;
                    case "tan":
                        tangente();
                        break;
                    case "sec":
                        secante();
                        break;
                    case "csc":
                        coscecante();
                        break;
                    case "cot":
                        cotangente();
                        break;
                    case "valorAbsoluto":
                        ValorAbsoluto();
                        break;
                    case "exp":
                        exp();
                        break;
                    case "pi":
                        pi();
                        break;
                    case "euler":
                        euler();
                        break;
                    case "factorial":
                        Factorial();
                        break;
                }
            }

            switch (nameFunction)
            {
                case "Mc":
                    MemoryClear();
                    break;
                case "Mr":
                    MemoryRecall();
                    break;
                case "Ms":
                    MemoryStorage();
                    break;
            }
        }
    }

    public void ChangeSign()
    {
        if (parentesis)
        {
            if (textoReferencia.text.Length > 0) {
                if (textoReferencia.text.Substring(0).Contains("-")) {
                    textoReferencia.text = textoReferencia.text.Remove(0,1);
                }
                else {
                    textoReferencia.text = "-" + textoReferencia.text;
                }
            }
        }
        else {
            if (textoPantalla.text.Length > 0)
            {
                double x = Double.Parse(textoPantalla.text) * (-1);
                textoPantalla.text = x.ToString();
            }
        }
        
    }

    public void EraseText()
    {
        if (textoPantalla.text.Length > 0)
        {
            textoPantalla.text = textoPantalla.text.Remove(textoPantalla.text.Length - 1);
        }
        if (textoPantalla.text == "")
        {
            textoPantalla.text = "0";
            open = false;
            
        }
        
        
        numero1 = 0;
        numero2 = 0;
        caso = 0;
        cadena = 0;
        respuesta = 0;
        x = 0;
        
    }

    public void ClearText()
    {
        textoPantalla.text = "0";
        textoReferencia.text = "";
        open = false;
        textoPantalla.color = new Color32(255, 255, 255, 255);
        textoReferencia.text = "0";
    }

    public void AddOperation(String nameOperation)
    {
        if (open)
        {
            
            switch (nameOperation)
            {
                case "+":
                    if (numero1 == 0)
                    {
                        textoReferencia.text = textoReferencia.text + textoPantalla.text + nameOperation;
                    }
                    else {
                        textoReferencia.text = textoReferencia.text + nameOperation;
                    }
                    textoPantalla.text = "";
                    break;

                case "-":
                    if (numero1 == 0)
                    {
                        textoReferencia.text = textoReferencia.text + textoPantalla.text + nameOperation;
                    }
                    else
                    {
                        textoReferencia.text = textoReferencia.text + nameOperation;
                    }
                    textoPantalla.text = "";
                    break;
                case "*":
                    if (numero1 == 0)
                    {
                        textoReferencia.text = textoReferencia.text + textoPantalla.text + nameOperation;
                    }
                    else
                    {
                        textoReferencia.text = textoReferencia.text + nameOperation;
                    }
                    textoPantalla.text = "";
                    break;
                case "/":
                    if (numero1 == 0)
                    {
                        textoReferencia.text = textoReferencia.text + textoPantalla.text + nameOperation;
                    }
                    else
                    {
                        textoReferencia.text = textoReferencia.text + nameOperation;
                    }
                    textoPantalla.text = "";

                    break;
                case "mod":
                    Modulo();
                    textoPantalla.text = "";
                    break;
                case "=":
                    GetResult();
                    break;
            }
            
        }
        else
        {
            if (textoPantalla.text.Length > 0)
            {
                switch (nameOperation)
                {
                    case "+":
                        Suma();
                        break;
                    case "-":
                        Resta();
                        break;
                    case "*":
                        Multiplicacion();
                        break;
                    case "/":
                        Division();
                        break;
                    case "mod":
                        Modulo();
                        break;
                    case "=":
                        GetResult();
                        break;
                }
            }
        }
    }

    public void Suma()
    {
        numero1 = Convert.ToDouble(textoPantalla.text);
        textoPantalla.text = "";
        textoReferencia.text = numero1.ToString() + "+";
        caso = 1;
    }

    public void Resta()
    {
        numero1 = Convert.ToDouble(textoPantalla.text);
        textoPantalla.text = "";
        textoReferencia.text = numero1.ToString() + "-";
        caso = 2;
    }

    public void Multiplicacion()
    {
        numero1 = Convert.ToDouble(textoPantalla.text);
        textoPantalla.text = "";
        textoReferencia.text = numero1.ToString() + "*";
        caso = 3;
    }

    public void Division()
    {
        numero1 = Convert.ToDouble(textoPantalla.text);
        textoPantalla.text = "";
        textoReferencia.text = numero1.ToString() + "/";
        caso = 4;
    }

    public void Modulo()
    {
        if (open) {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = textoReferencia.text + numero1.ToString() + "%";
            textoPantalla.text = "";
            GetSecondNumber(3);
        }
        else {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = numero1.ToString() + " mod ";
            textoPantalla.text = "";
            caso = 7;
        }
        
    }

    public void Reciproco()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = (1 / numero1).ToString();
            textoReferencia.text = textoReferencia.text + "1 / (" + numero1.ToString() + ")";
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = (1 / numero1).ToString();
            textoReferencia.text = "1 / " + numero1.ToString();
        }
    }

    public void Cuadrado()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = textoReferencia.text + "Pow(" + numero1 + ",2)";
            textoPantalla.text = Mathf.Pow(float.Parse(textoPantalla.text), 2).ToString();
            numero1 = Convert.ToDouble(textoPantalla.text);
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = "(" + numero1 + ")^2";
            textoPantalla.text = Mathf.Pow(float.Parse(textoPantalla.text), 2).ToString();
        }
    }

    public void Cubo()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = textoReferencia.text + "Pow(" + numero1 + ",3)";
            textoPantalla.text = Mathf.Pow(float.Parse(textoPantalla.text), 3).ToString();
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = "(" + numero1 + ")^3";
            textoPantalla.text = Mathf.Pow(float.Parse(textoPantalla.text), 3).ToString();
        }
    }

    public void RaizCuadrada()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = textoReferencia.text + "Sqrt(" + numero1 + ")";
            textoPantalla.text = Mathf.Sqrt(float.Parse(textoPantalla.text)).ToString();
            numero1 = Convert.ToDouble(textoPantalla.text);
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = Mathf.Sqrt(float.Parse(textoPantalla.text)).ToString();
            textoReferencia.text = "sqrt(" + numero1 + ")";
        }
    }

    public void potencia()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = "";
            textoReferencia.text = textoReferencia.text + "Pow(" + numero1 + ",";
            GetSecondNumber(1);
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = "";
            textoReferencia.text = numero1.ToString() + "^";
            caso = 5;
        }
    }

    public void PotenciaBase10()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = textoReferencia.text + "Pow(10," + numero1 + ")";
            textoPantalla.text = Mathf.Pow(10, float.Parse(textoPantalla.text)).ToString();
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = "(10)^" + numero1;
            textoPantalla.text = Mathf.Pow(10, float.Parse(textoPantalla.text)).ToString();
        }
    }

    public void Logaritmo()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = "";
            textoReferencia.text = textoReferencia.text + "Log(" + numero1 + ",";
            GetSecondNumber(2);
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = "";
            textoReferencia.text = numero1.ToString() + " base log ";
            caso = 6;
        }
    }

    public void Ln()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = (1.0 / Math.Log(Math.E, float.Parse(textoPantalla.text))).ToString();
            textoReferencia.text = textoReferencia.text + "(1/Log(2.71828182845," + numero1 + "))";
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = "ln(" + numero1 + ")";
            textoPantalla.text = (1.0 / Math.Log(Math.E, float.Parse(textoPantalla.text))).ToString();
        }
    }

    public void seno()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Sin(grados).ToString();
            }
            else
            {

                textoPantalla.text = Math.Sin(numero1).ToString();
            }
            textoReferencia.text = textoReferencia.text + "Sin(" + numero1 + ")";
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
           
           
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Sin(grados).ToString();
            }
            else
            {
                
                textoPantalla.text = Math.Sin(numero1).ToString();
            }

            textoReferencia.text = "sen(" + numero1 + ")";
        }
    }

    public void coseno()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Cos(grados).ToString();
            }
            else
            {
                textoPantalla.text = Math.Cos(numero1).ToString();
            }
            textoReferencia.text = textoReferencia.text + "Cos(" + numero1 + ")";
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Cos(grados).ToString();
            }
            else
            {
                textoPantalla.text = Math.Cos(numero1).ToString();
            }
            textoReferencia.text = "cos(" + numero1 + ")";
        }
    }

    public void tangente()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Tan(grados).ToString();
            }
            else
            {
                textoPantalla.text = Math.Tan(numero1).ToString();
            }
            textoReferencia.text = textoReferencia.text + "Tan(" + numero1 + ")";
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Tan(grados).ToString();
            }
            else
            {
                textoPantalla.text = Math.Tan(numero1).ToString();
            }
            textoReferencia.text = "tan(" + numero1 + ")";
        }
    }

    public void secante()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Asin(grados).ToString();
            }
            else
            {
                textoPantalla.text = Math.Asin(numero1).ToString();
            }
            textoReferencia.text = textoReferencia.text + "Asin(" + numero1 + ")";
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Asin(grados).ToString();
            }
            else
            {
                textoPantalla.text = Math.Asin(numero1).ToString();
            }
            textoReferencia.text = "sec(" + numero1 + ")";
        }
    }

    public void coscecante()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Acos(grados).ToString();
            }
            else
            {
                textoPantalla.text = Math.Acos(numero1).ToString();
            }
            textoReferencia.text = textoReferencia.text + "Acos(" + numero1 + ")";
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Acos(grados).ToString();
            }
            else
            {
                textoPantalla.text = Math.Acos(numero1).ToString();
            }
            textoReferencia.text = "csc(" + numero1 + ")";
        }
    }

    public void cotangente()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Atan(grados).ToString();
            }
            else
            {
                textoPantalla.text = Math.Atan(numero1).ToString();
            }
            textoReferencia.text = textoReferencia.text + "Atan(" + numero1 + ")";
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            if (rad_deg.isOn)
            {
                double grados = numero1 * (Math.PI / 180);
                textoPantalla.text = Math.Atan(grados).ToString();
            }
            else
            {
                textoPantalla.text = Math.Atan(numero1).ToString();
            }
            textoReferencia.text = "cot(" + numero1 + ")";
        }
    }
    public void ValorAbsoluto()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = Math.Abs(numero1).ToString();
            textoReferencia.text = textoReferencia.text + "Abs(" + numero1 + ")";
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = Math.Abs(numero1).ToString();
            textoReferencia.text = "abs(" + numero1 + ")";
        }
    }

    public void exp()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = Math.Exp(numero1).ToString();
            textoReferencia.text = textoReferencia.text + "Exp(" + numero1 + ")";
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoPantalla.text = Math.Exp(numero1).ToString();
        }
    }

    public void pi()
    {
        textoPantalla.text = Math.PI.ToString();
    }

    public void euler()
    {
        textoPantalla.text = Math.E.ToString();
    }

    public void Factorial()
    {
        if (open)
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = "fact(" + numero1 + ")";
            if ((numero1 > 0 && numero1 < 20) && ((numero1 % 1) == 0))
            {
                textoPantalla.text = factorial_Recursion(numero1).ToString();
                textoReferencia.text = textoReferencia.text + textoPantalla.text;
            }
            else
            {
                textoReferencia.text = "";
                textoPantalla.text = "0";
                numero1 = 0;
                controlAudio.PlayAudio(2);
            }
        }
        else
        {
            numero1 = Convert.ToDouble(textoPantalla.text);
            textoReferencia.text = "fact(" + numero1 + ")";
            if ((numero1 > 0 && numero1 < 20) && ((numero1 % 1) == 0))
            {
                textoPantalla.text = factorial_Recursion(numero1).ToString();
            }
            else
            {
                textoReferencia.text = "";
                textoPantalla.text = "0";
                numero1 = 0;
                controlAudio.PlayAudio(2);
            }
        }
    }

    public double factorial_Recursion(double number)
    {
        if (number == 1)
            return 1;
        else
            return number * factorial_Recursion(number - 1);
    }

    public Double Deg2Rad(double number)
    {
        return number * Mathf.Deg2Rad;
    }

    public Double Rad2Deg(double number)
    {
        return number * Mathf.Rad2Deg;
    }

    public object GetAnswer(string expression)
    {
        string textoResultado = expression.Replace(',', '.');
        Expression ex = new Expression(textoResultado);
        return ex.Evaluate();
    }

    internal IEnumerator DelayResult()
    {
        yield return new WaitForSeconds(0.1f);
        if ((isBalanced(textoReferencia.text)))
        {
            if (textoPantalla.text == "")
            {
                object answer = GetAnswer(textoReferencia.text);
                textoPantalla.text = answer.ToString();
                textoReferencia.text = "";
                numero1 = Convert.ToDouble(textoPantalla.text);
            }
            else {
                object answer = GetAnswer(textoReferencia.text + textoPantalla.text);
                textoPantalla.text = answer.ToString();
                textoReferencia.text = "";
                numero1 = Convert.ToDouble(textoPantalla.text);
            }
            
        }
        else
        {
            textoPantalla.text = "Error de sintaxis";
            textoPantalla.color = new Color32(255, 0, 0, 255);
        }
    }

    public void GetSecondNumber(int value)
    {
        switch (value)
        {
            case 1:
                if (textoPantalla.text != "")
                {
                    numero2 = Convert.ToDouble(textoPantalla.text);
                    textoReferencia.text = textoReferencia.text + numero2 + ")";
                    textoPantalla.text = "";
                }
                break;
            case 2:
                if (textoPantalla.text != "")
                {
                    numero2 = Convert.ToDouble(textoPantalla.text);
                    textoReferencia.text = textoReferencia.text + numero2 + ")";
                    textoPantalla.text = "";
                }
                break;
            case 3:
                if (textoPantalla.text != "")
                {
                    numero2 = Convert.ToDouble(textoPantalla.text);
                    textoReferencia.text = textoReferencia.text + numero2;
                    textoPantalla.text = "";
                }
                break;
        }
    }

    public void GetResult()
    {
        open = false;
        if (parentesis)
        {
            int Count = 0;
            foreach (char c in textoReferencia.text)
            {
                if (c == '(')
                {
                    Count++;
                }
            }
            int k = Count;

            int kount = 0;
            foreach (char c in textoReferencia.text)
            {
                if (c == ')')
                {
                    kount++;
                }
            }
            int p = kount;

            //print("( : " + k + "/ ) : " + p);

            for (int i = p; i < k; i++)
            {
                textoReferencia.text = textoReferencia.text + textoPantalla.text;
                textoPantalla.text = "";
                if (textoPantalla.text == "")
                {
                    textoReferencia.text = textoReferencia.text + ")";
                }
            }
            StartCoroutine(DelayResult());
        }
        else
        {
            numero2 = Convert.ToDouble(textoPantalla.text);
            switch (caso)
            {
                case 1:
                    respuesta = numero1 + numero2;
                    textoReferencia.text = textoReferencia.text + numero2.ToString();
                    break;
                case 2:
                    respuesta = numero1 - numero2;
                    textoReferencia.text = textoReferencia.text + numero2.ToString();
                    break;
                case 3:
                    respuesta = numero1 * numero2;
                    textoReferencia.text = textoReferencia.text + numero2.ToString();
                    break;
                case 4:
                    if (numero2 != 0)
                    {
                        respuesta = numero1 / numero2;
                        textoReferencia.text = textoReferencia.text + numero2.ToString();
                    }
                    else
                    {
                        controlAudio.PlayAudio(2);
                        numero2 = 0;
                        textoPantalla.text = "";
                    }
                    break;
                case 5:
                    respuesta = Mathf.Pow(float.Parse(numero1.ToString()), float.Parse(numero2.ToString()));
                    textoReferencia.text = textoReferencia.text + numero2;
                    break;
                case 6:
                    respuesta = Mathf.Log(float.Parse(numero2.ToString()), float.Parse(numero1.ToString()));
                    textoReferencia.text = textoReferencia.text + numero2;
                    break;
                case 7:
                    respuesta = numero1 % numero2;
                    textoReferencia.text = textoReferencia.text + numero2;
                    break;
            }
            textoPantalla.text = respuesta.ToString();
        }
    }
}
