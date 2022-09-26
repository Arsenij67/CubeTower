
using UnityEngine;

public class ExploadCubes : MonoBehaviour
{
    public new GameObject camera;
    public GameObject Restart;
    public GameObject Cameras,boom;

    private bool CollisionSet = true;

    private void OnCollisionEnter (Collision collision)
    {
        

        if (collision.gameObject.tag == "Cube" && CollisionSet)
        {
            
            for (int i = collision.transform.childCount - 1; i >= 0; i--)

            {
               
                Transform child = collision.transform.GetChild(i);
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(80f,Vector3.up,5f);
                child.SetParent(null); 
            
            }
            Restart.SetActive(true);
            camera.transform.position -= new Vector3(0,0,3f);
          
            Cameras.AddComponent<CameraShake>();
           
        Destroy(collision.gameObject);
            GameObject newVFX = Instantiate(boom, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collision.contacts[0].point.z), Quaternion.identity);
            Destroy(newVFX,3f);
            if (PlayerPrefs.GetString("music") == "Yes")

            {
               
                GetComponent<AudioSource>().Play();
                

            } 
           

            CollisionSet = false;
         
          
            
           
        
        }
        
    }
    
   
}
