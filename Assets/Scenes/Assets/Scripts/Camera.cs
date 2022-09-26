
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float speed = 5f;
    internal object backgroundColor;
    private Transform _rotation;
    
    void Start()
    {
        
        _rotation = GetComponent<Transform>();

        
    }

    
    void Update()
    {
        if (PlayerPrefs.GetString("music") == "No")
            GetComponent<AudioSource>().Play();
        _rotation.Rotate(0, speed * Time.deltaTime, 0);
        
    }
}
