using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BushTrimmer : MonoBehaviour
{
   public Animator trimmers;
   public SpriteRenderer trimmerRenderer;
   public Animator victoryBG;
   public List<GameObject> easyBushes;
   public List<GameObject> mediumBushes;
   public List<GameObject> hardBushes;

   private GameObject bush;
   private List<int> tileIndexes = new List<int>();
   private List<Rigidbody2D> tileBodies = new List<Rigidbody2D>();
   private List<GameObject> tracking = new List<GameObject>();

   // Start is called before the first frame update
   private void Start()
   {
      int tileCount = 0;

      switch (GameVars.Instance.difficulty)
      {
         case GameVars.Difficulty.Easy:
            bush = easyBushes[Random.Range(0, easyBushes.Count)];
            break;

         case GameVars.Difficulty.Medium:
            bush = mediumBushes[Random.Range(0, mediumBushes.Count)];
            break;

         case GameVars.Difficulty.Hard:
            bush = hardBushes[Random.Range(0, hardBushes.Count)];
            break;

         default:
            Debug.LogError("Why did you let this happen");
            break;
      }
      tileBodies = bush.GetComponentsInChildren<Rigidbody2D>().ToList();
      tileCount = tileBodies.Count;

      for (int ix = 0; ix < tileCount; ix++)
      {
         tileIndexes.Add(ix);
      }
   }

   // Update is called once per frame
   private void Update()
   {
      if (trimmers.GetBool("Snip"))
      {
         trimmers.ResetTrigger("Snip");
      }

      if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
      {
         if (tileIndexes.Count > 0)
         {
            FlingTile();
         }
      }

      if (tileIndexes.Count == 0)
      {
         GameState.Instance.state.SetTrigger("Win");
         victoryBG.SetTrigger("Display");
         trimmerRenderer.color = Color.clear;
      }

      foreach(GameObject obj in tracking)
      {
         if ((Mathf.Abs(obj.transform.position.x) >= 6.0f) || (Mathf.Abs(obj.transform.position.y) >= 5.0f))
         {
            tracking.Remove(obj);
            Destroy(obj, 0.1f);
         }
      }
   }

   private void FlingTile()
   {
      int index = -1;
      int listIdx = Random.Range(0, tileIndexes.Count);
      index = tileIndexes[listIdx];
      tileIndexes.RemoveAt(listIdx);
      tileBodies[index].gravityScale = 1.0f;
      trimmers.transform.position = tileBodies[index].transform.position;
      trimmers.SetTrigger("Snip");
      tileBodies[index].AddForce(RandomDirection());
   }

   private Vector2 RandomDirection()
   {
      float xforce = Random.Range(-20f, 20f);
      float yforce = Random.Range(-20f, 20f);
      return new Vector2(xforce, yforce);
   }
}