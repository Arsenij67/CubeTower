
using UnityEngine;

public class Unlock : MonoBehaviour
{
 
public int UnitToUnlock;
public Material black;
    private void Start()
    {
        if (PlayerPrefs.GetInt("score") < UnitToUnlock)
        {

            GetComponent<MeshRenderer>().material = black;
        }
            
    }
    

}
