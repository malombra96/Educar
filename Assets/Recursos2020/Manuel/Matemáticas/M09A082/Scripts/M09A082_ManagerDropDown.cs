using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A082_ManagerDropDown : MonoBehaviour
{
   ControlPuntaje _controlPuntaje;
   ControlNavegacion _controlNavegacion; 
   [HideInInspector] public ControlAudio _controlAudio;
   
   [HideInInspector] public List<M09A082_DropDown> _dropdowns;
   
   [Header("Validar")] public Button _validar;

   int correct;
   
   private void Start()
   {
      _controlPuntaje = FindObjectOfType<ControlPuntaje>();
      _controlNavegacion = FindObjectOfType<ControlNavegacion>();
      _controlAudio = FindObjectOfType<ControlAudio>();
      
      if(_validar)
         _validar.onClick.AddListener(EvaluateDropDown);

      _validar.interactable = false;
   }

   void EvaluateDropDown()
   {
      foreach (var dropdown in _dropdowns)
      {
         _validar.interactable = false;
         dropdown.GetComponent<Dropdown>().interactable = false;
         
         SetSpriteDropDown(dropdown,dropdown._indexCurrent == dropdown._indexRight);
      
      }
      
      if(correct == _dropdowns.Count)
          _controlPuntaje.IncreaseScore();

      _controlAudio.PlayAudio(correct == _dropdowns.Count? 1:2);
      _controlNavegacion.Forward(2);
      
   }

   /// <summary>
   /// Realiza el cambio de estado [right or wrong] para la imagen recibida
   /// </summary>
   /// <param name="select"></param>
   /// <param name="state"></param>
   void SetSpriteDropDown(M09A082_DropDown dropDown, bool state)
   {
       Sprite select = dropDown.GetComponent<Dropdown>().captionImage.sprite;
       int index = dropDown._default.IndexOf(select); 

      switch (state)
      {
         case true:

            dropDown.GetComponent<Dropdown>().captionImage.sprite = dropDown._right[index];
            correct++;
            
            break;
         
         case false:
            
            dropDown.GetComponent<Dropdown>().captionImage.sprite = dropDown._wrong[index];
            break;
      }
   }

   /// <summary>
   /// Set State Button Validar
   /// </summary>
   public void StateBtnValidar()
   {
      foreach (var dropdown in _dropdowns)
      {
         if(dropdown.state)
            _validar.interactable = true;
         else
         {
            _validar.interactable = false;
            break;
         }
            
      }
      
   }

    public void ResetManagerDropDown()
    {
        foreach (var dropdown in _dropdowns)
        {
            var bh = dropdown.GetComponent<M09A082_DropDown>();
            var dd = dropdown.GetComponent<Dropdown>();

            dropdown.reset = true;

            dd.value = 0;
            bh._indexCurrent = dd.value;


            dd.captionImage.sprite = dd.options[0].image;

            for (int i = 1; i < dd.options.Count; i++)
                dd.options[i].image = bh._default[i-1];

            bh.state = false;
            dd.interactable = true;
            dropdown.reset = false;
        }

        _validar.interactable = false;
        correct = 0;
    }
}
