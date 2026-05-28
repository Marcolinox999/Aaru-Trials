using System;
using UnityEngine;

public class SwapAbility : MonoBehaviour
{
   [SerializeField] GameObject stagecompleted;
   private StageCompleted swapped;
   
   [SerializeField] Canvas canvas;

   private void Awake()
   {
      swapped = stagecompleted.GetComponent<StageCompleted>();
   }

   private void Update()
   {
      if (swapped.swap)
      {
         swapped.manager.ui_Complete.GetComponent<Canvas>().enabled = false;
         canvas.enabled = true;
      }
      else
      {
         canvas.enabled = false;
      }
   }
}
