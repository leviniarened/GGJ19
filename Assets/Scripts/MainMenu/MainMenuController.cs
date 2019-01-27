using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void StartSlides()
    {
        anim.SetTrigger("ShowStartSlides");
    }

}
