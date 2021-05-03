using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public GameObject cellPrefab;

    public bool alive = false;

    public string ruleSet;

    private GameObject _cell = null;

    private Animator _cellAnimator;
    
    private bool _initialCondition;

    public AudioSource[] audioSources;

    public void SetAlive(bool a)
    {
        if (alive == a) return;

        if (a) _cellAnimator.SetTrigger("Birth");
        else _cellAnimator.SetTrigger("Death");

        alive = a;
    }

    // Start is called before the first frame update
    void Start()
    {
        _initialCondition = alive;
        InitCell(alive);
    }

    private void InitCell(bool alive)
    {
        _cell = Instantiate(cellPrefab, transform.position, Quaternion.identity, transform);
        var r = transform.GetComponent<RectTransform>();
        foreach (RectTransform child in _cell.GetComponentInChildren<RectTransform>())
        {
            child.localScale = new Vector3(r.rect.width / child.rect.width, r.rect.height / child.rect.height);
        }

        _cellAnimator = _cell.GetComponent<Animator>();

        if (alive)
        {
            _cellAnimator.SetTrigger("Birth");
        }
    }

    // Update is called once per frame
    public void Reset()
    {
        SetAlive(_initialCondition);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayRandomPutSound();
        SetAlive(!alive);
        
    }

    public void ToggleShowSolution()
    {
        _cellAnimator.SetBool("ShowingSolution", !_cellAnimator.GetBool("ShowingSolution"));
    }

    public void PlayRandomPutSound()
    {

        var audio = audioSources[Random.Range(0, audioSources.Length)];
        audio.Stop();
        audio.Play();
    }
}
