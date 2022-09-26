using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    public List <Color> color = new List<Color>(3);
    private GameController controller;
    private int BestScore1 = 5;
    private int BestScore2 = 8;
    private int BestScore3 = 10;
    public Camera Maincamera;
    
    // Start is called before the first frame update
    void Awake()
    {



        controller = GetComponent<GameController>();
        
        
    }
  
    void Update()
    {



        print(controller.maxY );
        if  (controller.maxY == BestScore1)Maincamera.backgroundColor = color[0]; 
        if  (controller.maxY == BestScore2)Maincamera.backgroundColor = color[1]; 
        if  (controller.maxY == BestScore3)Maincamera.backgroundColor = color[2]; 


    }
       



        
       
}

