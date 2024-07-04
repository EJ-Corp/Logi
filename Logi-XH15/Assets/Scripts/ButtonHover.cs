using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI buttonText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = Color.grey; 
    }

  public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = Color.white; 
    }
}