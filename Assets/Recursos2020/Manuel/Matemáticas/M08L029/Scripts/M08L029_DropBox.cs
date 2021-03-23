using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L029_DropBox : MonoBehaviour
{
    M08L029_ManagerPaint _ManagerPaint;

    [Header("Symbol Answer")] public Image _symbol;
    Image _image;

    [Header("Current Color")] public string _current;

    public enum RightColor
    {
        Green,
        Blue
    }
    [Header("Right Color")] public RightColor _right;

    void Start()
    {
        _ManagerPaint = FindObjectOfType<M08L029_ManagerPaint>();

        _ManagerPaint._dropBox.Add(this);

        _image = GetComponent<Image>();

        _symbol = transform.GetChild(0).GetComponent<Image>();
        _symbol.gameObject.SetActive(false);
    }

    public void PaintBox(string color)
    {
        _image.sprite =  color == "Green"? GetComponent<BehaviourSprite>()._selection : GetComponent<BehaviourSprite>()._disabled;
        _current = color;

        _ManagerPaint.StateButtonValidar();
    }


}
