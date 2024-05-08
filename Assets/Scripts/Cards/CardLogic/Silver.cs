using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;

public class Silver : Unit,IPointerClickHandler
{
     public new void OnPointerClick(PointerEventData eventData)
     {
        if(GameManager.Instance.CurrentState==GameState.DecoyState)
        {
            CardManager.Instance.InvokeDecoy(this);
        }
        else
        {
            base.OnPointerClick(eventData);
        }

     }    
     public void ReturnToHand()
        {
            CardIsInHand = true;
        }
}
