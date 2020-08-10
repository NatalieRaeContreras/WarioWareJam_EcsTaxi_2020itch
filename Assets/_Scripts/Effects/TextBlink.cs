using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TextBlink : MonoBehaviour
{
   public float timeToFlash = 1.0f;
   public float blankTime = 1.0f;

   private float timer = 0.0f;
   private bool visible = true;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {

      if (timer >= timeToFlash && visible)
      {
         this.GetComponent<Image>().color = new Color(0, 0, 0, 0);
         visible = false;
         timer = 0.0f;
      }

      if (timer >= blankTime && !visible)
      {
         this.GetComponent<Image>().color = new Color(255, 255, 255, 255);
         visible = true;
         timer = 0.0f;
      }

      timer += Time.deltaTime;
   }
}
