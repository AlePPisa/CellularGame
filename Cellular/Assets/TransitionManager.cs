using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public GameObject transitionObject;
    private Animator _transitionAnimator;
    
    private void Start()
    {
        _transitionAnimator = transitionObject.GetComponent<Animator>();
        FadeOut();
    }

    public void FadeIn()
    {
        transitionObject.SetActive(true);
        StartCoroutine(EnumeratorFadeIn());
    }

    public void FadeOut()
    {
        transitionObject.SetActive(true);
        StartCoroutine(EnumeratorFadeOut());
    }
    
    IEnumerator EnumeratorFadeOut()
    {
        _transitionAnimator.SetTrigger("FadeOut");
        
        yield return new WaitForSeconds(1.5f);
        
        transitionObject.SetActive(false);
    }

    IEnumerator EnumeratorFadeIn()
    {
        yield return new WaitForSeconds(2f);
        
        _transitionAnimator.SetTrigger("FadeIn");
        
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
