using UnityEngine;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour
{
   private GameObject menu;
   private GameObject replay;

   // Start is called before the first frame update
   private void Start()
   {
      menu = GameObject.FindGameObjectWithTag("Menu");
      replay = GameObject.FindGameObjectWithTag("Replay");
   }

   // Update is called once per frame
   private void Update()
   {
      bool toMenu = Toolbox.Instance.Vars.endScreenSelection;

      menu.GetComponent<Animator>().SetBool("Selected", toMenu);
      replay.GetComponent<Animator>().SetBool("Selected", !toMenu);

      if (menu.GetComponent<Animator>().GetBool("Selected"))
      {
         menu.GetComponentInChildren<Animator>().GetComponentInChildren<Image>().color = Color.white;
         replay.GetComponentInChildren<Animator>().GetComponentInChildren<Image>().color = Color.clear;
      }
      else
      {
         menu.GetComponentInChildren<Animator>().GetComponentInChildren<Image>().color = Color.clear;
         replay.GetComponentInChildren<Animator>().GetComponentInChildren<Image>().color = Color.white;
      }
   }
}
