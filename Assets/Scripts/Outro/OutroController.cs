using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroController : MonoBehaviour
{
   public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
