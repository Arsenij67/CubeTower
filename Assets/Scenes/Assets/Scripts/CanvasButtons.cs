using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasButtons : MonoBehaviour
{
    public Sprite musicOn, musicOff;
    private void Start()
    { if (PlayerPrefs.GetString("music") == "No"&&gameObject.name == "Voice")


        {
            GetComponent<Image>().sprite = musicOff;
        }
    }
    public void RestartGame()
    {SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (PlayerPrefs.GetString("music") == "Yes")
        {

            GetComponent<AudioSource>().Play();

        }

    }
    public void LoadShop()
    {
        if (PlayerPrefs.GetString("music") == "Yes")
        {
            GetComponent<AudioSource>().Play();
           


        } SceneManager.LoadScene("Shop");
    }
    public void Close()
    {
        if (PlayerPrefs.GetString("music") == "Yes")
        {
            GetComponent<AudioSource>().Play();
        


        }    SceneManager.LoadScene("Main");
    }
    public void LoadVK()
    {
        if (PlayerPrefs.GetString("music") == "Yes")
        {
            GetComponent<AudioSource>().Play();
        }
        Application.OpenURL("https://vk.com/club195893984");
    }
    public void MusicWork()
    {
        if (PlayerPrefs.GetString("music") == "No")

        {
            PlayerPrefs.SetString("music", "Yes");
            GetComponent<AudioSource>().Play();
            GetComponent<Image>().sprite = musicOn;

        }
        else
        {
            PlayerPrefs.SetString("music","No");
            GetComponent<AudioSource>().Stop();
            GetComponent<Image>().sprite = musicOff;

        }
       
    
    
    
    }
}