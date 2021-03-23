using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class M10A61_mangerCalculadora : MonoBehaviour
{
    // Start is called before the first frame update
    public Text texto;
    public string resultado;
    public List<string> subStrings;
    public List<Button> botones;
    public Button on, off;

    public void AddOperation(string nameOperation) {
    }


    public void AddFunction(string nameFunction) {
        if (nameFunction == "=" && resultado != "" && texto.text != "") { 

            
        }
        //if (nameFunction == "ce") {
        //    texto.text = "0";
        //    resultado = "";
        //    subStrings.Clear();
        //}

        //if (nameFunction == "on") {
        //    texto.text = "0";
        //    resultado = "";
        //    subStrings.Clear();
        //    foreach (var v in botones)
        //        v.interactable = true;
        //    on.interactable = false;
        //    off.interactable = true;
        //}

        //if (nameFunction == "off") {
        //    texto.text = "0";
        //    resultado = "";
        //    subStrings.Clear();
        //    foreach (var v in botones)
        //        v.interactable = false;
        //    on.interactable = true;
        //    off.interactable = false;
        //}
    }

    public void AddNumber(string nameNumber) {
        
        resultado = String.Concat(resultado, nameNumber);
        texto.text = resultado;

        //if (resultado.Contains("+"))
        //{
        //    subStrings.Clear();
        //    char[] spearator = { '+'};

        //    // using the method 
        //    String[] strlist = resultado.Split(spearator,StringSplitOptions.RemoveEmptyEntries);
           
        //    subStrings.Add(strlist[0]);
        //    if (strlist.Length > 1)
        //    {
        //        subStrings.Add(strlist[1]);
        //        texto.text = resultado;
        //        StartCoroutine(Add());
        //    }
        //    else {
        //        texto.text = resultado;
        //    }
        //}
        //else {
        //    texto.text = resultado;
        //}

        //if (resultado.Contains("-"))
        //{
        //    subStrings.Clear();
        //    char[] spearator = { '-' };

        //    // using the method 
        //    String[] strlist = resultado.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

        //    subStrings.Add(strlist[0]);
        //    if (strlist.Length > 1)
        //    {
        //        subStrings.Add(strlist[1]);
        //        texto.text = resultado;
        //        StartCoroutine(Substract());
        //    }
        //    else
        //    {
        //        texto.text = resultado;
        //    }
        //}
        //else
        //{
        //    texto.text = resultado;
        //}

        //if (resultado.Contains("*"))
        //{
        //    subStrings.Clear();
        //    char[] spearator = { '*' };

        //    // using the method 
        //    String[] strlist = resultado.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

        //    subStrings.Add(strlist[0]);
        //    if (strlist.Length > 1)
        //    {
        //        subStrings.Add(strlist[1]);
        //        texto.text = resultado;
        //        StartCoroutine(Multiplication());
        //    }
        //    else
        //    {
        //        texto.text = resultado;
        //    }
        //}
        //else
        //{
        //    texto.text = resultado;
        //}
        //if (resultado.Contains("/"))
        //{
        //    subStrings.Clear();
        //    char[] spearator = { '/'};

        //    // using the method 
        //    String[] strlist = resultado.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

        //    subStrings.Add(strlist[0]);
        //    if (strlist.Length > 1)
        //    {
        //        subStrings.Add(strlist[1]);
        //        texto.text = resultado;
        //        StartCoroutine(Division());
        //    }
        //    else
        //    {
        //        texto.text = resultado;
        //    }
        //}
        //else
        //{
        //    texto.text = resultado;
        //}
    }

    IEnumerator Add() {
        yield return new WaitForSeconds(0.1f);

        int r = int.Parse(subStrings[0])+ int.Parse(subStrings[1]);
        texto.text = "";
        texto.text = r.ToString();

        subStrings.Clear();
        subStrings.Add(r.ToString());
        resultado = r.ToString();


    }

    IEnumerator Substract()
    {
        yield return new WaitForSeconds(0.1f);

        int r = int.Parse(subStrings[0]) - int.Parse(subStrings[1]);
        texto.text = "";
        texto.text = r.ToString();

        subStrings.Clear();
        subStrings.Add(r.ToString());
        resultado = r.ToString();


    }
    IEnumerator Multiplication()
    {
        yield return new WaitForSeconds(0.1f);

        int r = int.Parse(subStrings[0]) * int.Parse(subStrings[1]);
        texto.text = "";
        texto.text = r.ToString();

        subStrings.Clear();
        subStrings.Add(r.ToString());
        resultado = r.ToString();


    }
    IEnumerator Division()
    {
        yield return new WaitForSeconds(0.1f);
        ;
        if (int.Parse(subStrings[1]) == 0)
        {
            texto.text = "Error";
        }
        else {
            int r = int.Parse(subStrings[0]) / int.Parse(subStrings[1]);
            texto.text = "";
            texto.text = r.ToString();

            subStrings.Clear();
            subStrings.Add(r.ToString());
            resultado = r.ToString();
        }
        
        


    }

}
