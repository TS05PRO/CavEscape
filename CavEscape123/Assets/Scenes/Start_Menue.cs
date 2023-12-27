using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menue : MonoBehaviour
{
    [SerializeField] private AudioSource ClickingButtonSound;
    
    public void OnMouseDown()
    {
        ClickingButtonSound.Play();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
