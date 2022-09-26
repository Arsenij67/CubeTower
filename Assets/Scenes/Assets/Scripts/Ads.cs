using UnityEngine.Advertisements;
using UnityEngine;
using System.Collections;

public class Ads : MonoBehaviour
{

    private string GameID = "3587398", type = "video";
    private bool TestMode = false,needToStop;
    private Coroutine ShowAd;
    private static int CountLothes;

    private void Start()
    {
        Advertisement.Initialize(GameID, TestMode);
        CountLothes++;
        if (CountLothes % 4 == 0)
        {
            ShowAd
      = StartCoroutine(ShowAdd());
        }
    }
   private void Update()
    {
        if (needToStop)
        {

            needToStop = false;
            StopCoroutine(ShowAd);

        }
    
    
    }



    IEnumerator ShowAdd()
    {
        while (true)
        {

            
            if (Advertisement.IsReady(type))
            {
                Advertisement.Show(type);
     
                needToStop = true;
            }
               yield return new WaitForSeconds(2f);

            
        }
    }
}