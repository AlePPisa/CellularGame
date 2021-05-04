using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource _audioSource;
    private float audioHoverCooldown = 0.5f;
    public bool canPlayHoverAudio = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!canPlayHoverAudio) return;

        canPlayHoverAudio = false;
        StartCoroutine(AudioCooldown());
        
        _audioSource.Play();
    }

    IEnumerator AudioCooldown()
    {
        yield return new WaitForSeconds(audioHoverCooldown);
        canPlayHoverAudio = true;
    }
}
