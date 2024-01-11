
using System;
using UnityEngine;
using UnityEngine.UI;

public class Discromatropia : MonoBehaviour
{
   [SerializeField] private Sprite activeSprite;
   [SerializeField] private Sprite disabledSprite;
   [SerializeField] private UIController.UIColorblindMode colorblindMode;
   private bool _isActive;
   private void Start()
   {
      EventController.OnChangeColorblindMode += CheckToDisable;
      gameObject.GetComponent<Button>().onClick.AddListener(ChangeColorblindMode);
      _isActive = false;   
      if (colorblindMode == UIController.UIColorblindMode.Base)
      {
         ChangeColorblindMode();
      }
   }

   private void CheckToDisable(UIController.UIColorblindMode selectedColorblindMode)
   {
      if (selectedColorblindMode != colorblindMode)
      {
         gameObject.GetComponent<Image>().sprite = disabledSprite;
         _isActive = false;
      }
   }

   private void OnEnable()
   {
      EventController.OnChangeColorblindMode += CheckToDisable;
   }

   private void OnDisable()
   {
      EventController.OnChangeColorblindMode -= CheckToDisable;
   }

   private void ChangeColorblindMode()
   {
      if (_isActive)
      {
         return;
      }
      _isActive = true;
      UIController.Instance.ChangeColorblindMode(colorblindMode);
      if (_isActive)
      {
         gameObject.GetComponent<Image>().sprite = activeSprite;
      }
      else
      {
         gameObject.GetComponent<Image>().sprite = disabledSprite;
      }
   }
}
