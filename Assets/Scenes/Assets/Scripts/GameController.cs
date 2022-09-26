using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GameController : MonoBehaviour
{
    private CubePos nowCube = new CubePos(0, 1, 0);

    public float CubeChangeSpeed = 0.5f;
    public Transform CubeToPlace;
    public GameObject AllCubes,vfx;
    public GameObject [] CubesToCreate;
    public List<GameObject> UnlockCubes = new List<GameObject>();
 

    private Rigidbody AllCubesRb;
    private bool IsLose, firstCube;
    public GameObject[] gameObjectsPage;
    public GameObject Cameras;
    private float CamMoveToYPosition;

  
    public Text ScoreTxt;
   
    //Обьявляем все коордиинаты которые мы уже использовали

    private List<Vector3> AllCubesPositions = new List<Vector3>
    {
        new Vector3(0,0,0),
        new Vector3(0,1,0),
        new Vector3(0,0,1),
        new Vector3(-1,0,1),
        new Vector3(-1,0,0),
        new Vector3(0,0-1),
        new Vector3(1,0,-1),
        new Vector3(1,0,0),
        new Vector3(1,0,1),
        new Vector3(-1,0,-1)

    };

    public Transform mainCam;
    private Coroutine showCubePlace;
    private int PrevMaxHorizontal;
   

    private void Start()
    {
        ScoreTxt.text = "<size=77> <color=#9A1818>Best:</color></size>" + PlayerPrefs.GetInt("score") + "\n" + " Now: " ;

        CamMoveToYPosition = 6.18f + nowCube.y - 1;
        Transform mainCAM = Cameras.transform;
        AllCubesRb = AllCubes.GetComponent<Rigidbody>();
        showCubePlace = StartCoroutine(ShowCubePlace());

        if (PlayerPrefs.GetInt("score") < 5) UnlockCubes.Add(CubesToCreate[0]);

        if (PlayerPrefs.GetInt("score") < 10) OpenMethod(2);

        if (PlayerPrefs.GetInt("score") < 15) OpenMethod(3);
        if (PlayerPrefs.GetInt("score") < 20) OpenMethod(4);
        if (PlayerPrefs.GetInt("score") < 25) OpenMethod(5);
        if (PlayerPrefs.GetInt("score") < 30) OpenMethod(6);
        if (PlayerPrefs.GetInt("score") < 70) OpenMethod(7);
        if (PlayerPrefs.GetInt("score") < 90) OpenMethod(8);
        if (PlayerPrefs.GetInt("score") < 120) OpenMethod(9);
        else OpenMethod(10);





        void OpenMethod(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                UnlockCubes.Add(CubesToCreate[i]);


            }


        }


    }

    private void Update()
    {  StartCoroutine(MoveCameraChangeBg());
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && AllCubes != null && CubeToPlace != null && !EventSystem.current.IsPointerOverGameObject())
        {


            if (!firstCube)
            {
                firstCube = true;
                foreach (GameObject obj in gameObjectsPage)
                {

                    Destroy(obj);
                }
            }

            GameObject CreateCube = null;
            if (UnlockCubes.Count == 1)
            {
                CreateCube = UnlockCubes[0];

            }
            else
                CreateCube = UnlockCubes[UnityEngine.Random.Range(0, UnlockCubes.Count)];
            GameObject newCube = Instantiate
                 (UnlockCubes[UnityEngine.Random.Range(0, UnlockCubes.Count)], CubeToPlace.position, Quaternion.identity) as GameObject;
           

            newCube.transform.SetParent(AllCubes.transform);
            nowCube.setVector(CubeToPlace.position);
            AllCubesPositions.Add(nowCube.getVector());

         
            AllCubesRb.isKinematic = true;
            AllCubesRb.isKinematic = false;
              GameObject destr =  Instantiate(vfx, CubeToPlace.position, Quaternion.identity);
            Destroy(destr, 2f);
            if (PlayerPrefs.GetString("music") == "Yes")
                

            {
               
                GetComponent<AudioSource>().Play();
               

            }
            


            SpawnPositions();

        }
         
        if (!IsLose && AllCubesRb.velocity.magnitude > 0.5f)
        {
     
            Destroy(CubeToPlace.gameObject);
            IsLose = true;
            StopCoroutine(showCubePlace);
        }


        mainCam.localPosition = Vector3.MoveTowards(mainCam.localPosition, new Vector3(mainCam.localPosition.x, CamMoveToYPosition, mainCam.localPosition.z), 1f * Time.deltaTime);
    }



    IEnumerator ShowCubePlace()
    {

        while (true)
        {
            SpawnPositions();
            yield return new WaitForSeconds(CubeChangeSpeed);
        }

    }
    private void SpawnPositions()
    {

        List<Vector3> positions = new List<Vector3>();
        if (IsPositionEmpty(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z)) && nowCube.x + 1 != CubeToPlace.position.x) positions.Add(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z));
        if (IsPositionEmpty(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z)) && nowCube.x - 1 != CubeToPlace.position.x) positions.Add(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z)) && nowCube.y + 1 != CubeToPlace.position.y) positions.Add(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z)) && nowCube.y - 1 != CubeToPlace.position.y) positions.Add(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1)) && nowCube.z + 1 != CubeToPlace.position.z) positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1)) && nowCube.z - 1 != CubeToPlace.position.z) positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));
        if (positions.Count > 1)
        {
            CubeToPlace.position = positions[UnityEngine.Random.Range(0, positions.Count)];
        }


        else if (positions.Count == 0)
        {

            IsLose = true;
        }
        else
            CubeToPlace.position = positions[0];
    }

    private bool IsPositionEmpty(Vector3 TargetPos)
    {
        if (TargetPos.y == 0)
            return false;
        foreach (Vector3 pos in AllCubesPositions)
        {
            if (TargetPos == pos)
            {
                return false;
            }
        }
        return true;

    }
    //это метод который 
    public int  maxY = 0;
    private IEnumerator MoveCameraChangeBg()
    {
       
        int maxX = 0, maxZ = 0, Horizont;
        foreach (Vector3 pos in AllCubesPositions)
        {
            if (maxX < Mathf.Abs(Convert.ToInt32(pos.x))) maxX = Convert.ToInt32(pos.x);
            if (maxY < Convert.ToInt32(pos.y)) maxY = Convert.ToInt32(pos.y);
            if (maxZ < Mathf.Abs(Convert.ToInt32(pos.z))) maxZ = Convert.ToInt32(pos.z);
        }
        maxY--;
        if (PlayerPrefs.GetInt("score") < maxY)
        {
            PlayerPrefs.SetInt("score",maxY);
        }
        ScoreTxt.text = "<size=77> <color=#9A1818>Best:</color></size>" + PlayerPrefs.GetInt("score") + "\n"+" Now: "+maxY;
 
        CamMoveToYPosition = 6.18f + nowCube.y - 1f;

        Horizont = Mathf.Abs((maxX)) > Mathf.Abs(maxZ) ? maxX : maxZ;

        if (Horizont % 3 == 0 && PrevMaxHorizontal != Horizont)
        {
           
            mainCam.localPosition -= new Vector3(0, 0, 2.5f);
           
            PrevMaxHorizontal = Horizont;
        }

      

        yield return new WaitForSeconds(0.1f);
    }
    

    
    
}


public struct CubePos
{

    public int x, y, z;

    public CubePos(int x, int y, int z)
    {

        this.x = x;
        this.y = y;
        this.z = z;
    }


    public Vector3 getVector()
    {
        return new Vector3(x, y, z);
    }
    public void setVector(Vector3 pos)
    {
        x = Convert.ToInt32(pos.x);
        z = Convert.ToInt32(pos.z);
        y = Convert.ToInt32(pos.y);
    }

}
