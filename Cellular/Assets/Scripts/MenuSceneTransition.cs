using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneTransition : MonoBehaviour
{
    public float transitionTime;
    public Animator transitionAnimationTitle;
    public GameObject transition;
    
    private Animator _transitionAnimationFade;

    // Start is called before the first frame update
    void Start()
    {
        transition.SetActive(false);
        _transitionAnimationFade = transition.GetComponent<Animator>();
    }
    
    public void NextScene()
    {
        StartCoroutine(NextSceneCoroutine());
    }

    IEnumerator NextSceneCoroutine()
    {
        transitionAnimationTitle.SetTrigger("FadeOut");
        transition.SetActive(true);

        yield return new WaitForSeconds(transitionTime-1.5f);
        
        _transitionAnimationFade.SetTrigger("FadeIn");
        
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
