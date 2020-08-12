using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoxes : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("DestructibleProp"))
      {
         Destroy(other);
      }
   }
}
