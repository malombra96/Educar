using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L001_ManagerDropDown : MonoBehaviour
{
   ControlPuntaje _controlPuntaje;
   ControlNavegacion _controlNavegacion; 
   [HideInInspector] public ControlAudio _controlAudio;
   
   [Header("Colors State")] 
   public Color32 _rightColor;
   public Color32 _wrongColor;
   
   [HideInInspector] public List<M10L001_BehaviourDropDown> _dropdowns;
   
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
         
         switch (dropdown._type)
         {
            case M10L001_BehaviourDropDown.Type.text:

               SetTextDropDown(dropdown.GetComponent<Dropdown>().captionText,dropdown._indexCurrent == dropdown._indexRight);
               SetParentDropDown(dropdown.transform.parent.GetComponent<Image>(),dropdown._indexCurrent == dropdown._indexRight);

               break;
            
            case M10L001_BehaviourDropDown.Type.image:

               SetSpriteDropDown(dropdown.GetComponent<Image>(),dropdown._indexCurrent == dropdown._indexRight);
               
               break;
         }
      }
      
      _controlAudio.PlayAudio(correct == _dropdowns.Count? 1:2);
      _controlNavegacion.Forward(2);
      
   }

   /// <summary>
   /// Realiza el cambio de estado [right or wrong] para la imagen recibida
   /// </summary>
   /// <param name="select"></param>
   /// <param name="state"></param>
   void SetSpriteDropDown(Image select, bool state)
   {
      switch (state)
      {
         case true:

            select.color = _rightColor;
            correct++;
            _controlPuntaje.IncreaseScore();
            
            break;
         
         case false:
            
            select.color = _wrongColor;
            
            break;
      }
   }

   /// <summary>
   /// Realiza el cambio de estado [right or wrong] para el texto recibido
   /// </summary>
   /// <param name="select"></param>
   /// <param name="state"></param>
   void SetTextDropDown(Text select, bool state)
   {
      switch (state)
      {
         case true:

            select.color = _rightColor;
            correct++;
            _controlPuntaje.IncreaseScore();
            
            break;
         
         case false:
            
            select.color = _wrongColor;
            
            break;
      }
   }

    /// <summary>
   /// Realiza el cambio de estado [right or wrong] para el parent del dropdown
   /// </summary>
   /// <param name="select"></param>
   /// <param name="state"></param>
   void SetParentDropDown(Image select, bool state)
   {
      select.sprite = state? 
         select.GetComponent<BehaviourSprite>()._right : 
         select.GetComponent<BehaviourSprite>()._wrong;        
   }

   void DefaultTextDropDown(Text select) => select.color = Color.black;
   
   void DefaultSpriteDropDown(Image select) => select.color = Color.white;

   void DefaultParentDropDown(Image select) => select.sprite =  select.GetComponent<BehaviourSprite>()._default;
   

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
         var bh = dropdown.GetComponent<M10L001_BehaviourDropDown>();
         var dd = dropdown.GetComponent<Dropdown>();
         
         dropdown.reset = true;

         dd.value = 0;
         bh._indexCurrent = dd.value;

         if (bh._type == M10L001_BehaviourDropDown.Type.text)
         {
            dd.captionText.text = dd.options[0].text;
            DefaultTextDropDown(dd.captionText);
            DefaultParentDropDown(dropdown.transform.parent.GetComponent<Image>());
         }
         else
         {
            dd.captionImage.sprite = dd.options[0].image;
            DefaultSpriteDropDown(dd.GetComponent<Image>());
         }
            
         
         bh.state = false;
         dd.interactable = true;
         dropdown.reset = false;
      }
      
      _validar.interactable = false;
      correct = 0;
   }
}
