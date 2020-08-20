using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
   public List<GameObject> canvasElements;

   public Vector3 defaultTimerPos;
   public Vector3 defaultCountdownPos;

   private Animator _gameWinAnim;

   public void Start()
   {
      Toolbox.Instance.AttachCanvasManager(this);
      DontDestroyOnLoad(this);
   }

   public void Init()
   {
      for (int ix = 0; ix < canvasElements.Count; ix++)
      {
         canvasElements[ix].SetActive(true);
      }

      _gameWinAnim = canvasElements[0].GetComponent<Animator>();
      defaultTimerPos = canvasElements[1].transform.position;
      defaultCountdownPos = canvasElements[2].transform.position;

   }
}