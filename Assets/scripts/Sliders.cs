using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sliders : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        PlanetsScale.isScaling = false;
        SystemsScale.isScaling = false;
        PlanetPosition.isScaling = false;
    }
}