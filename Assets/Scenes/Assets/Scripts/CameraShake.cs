
using UnityEngine;
public class CameraShake : MonoBehaviour
{

    private Transform camTransform;
    private float ShakeDur = 1f; //длительность тряски
    private float ShakeAmount =0.16f; // сила тряски
    private float decreseFactor = 1.5f; // скорость реинкорнации

    private Vector3 OriginPosition;

    private void Start()
    {
        camTransform = GetComponent<Transform>();
        OriginPosition = camTransform.localPosition;
    }

    private void Update()
    {
        if (ShakeDur > 0)
        {
   
            camTransform.localPosition = OriginPosition + Random.insideUnitSphere * ShakeAmount * Time.deltaTime;
            ShakeDur -=  Time.deltaTime * decreseFactor;
        }
        else
            ShakeDur = 0;
        camTransform.localPosition = OriginPosition;

    }

}