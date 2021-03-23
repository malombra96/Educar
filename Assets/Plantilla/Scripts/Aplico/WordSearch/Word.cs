using System;

public class Word
{
    public String palabra;

    public int grupo;

    public char[] letras;



    public Word(String nuevoPalabra, int nuevoGrupo )
    {
        palabra = nuevoPalabra;
        grupo = nuevoGrupo;
        letras = palabra.ToCharArray();
     

    }

}
