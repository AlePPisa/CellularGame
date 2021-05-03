using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        
    }

    public void RestartLevel()
    {
        //TODO: Make this have a fade in fade out
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StepSimulation()
    {
        
    }

    public void RunSimulation()
    {
        
    }
}
