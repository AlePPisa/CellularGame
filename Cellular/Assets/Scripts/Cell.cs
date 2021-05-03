using System.Collections;
using System.Collections.Generic;
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

    public void SetAlive(bool a)
    {
        if (alive == a) return;
        if (_cell == null && a)
        {
            _cell = Instantiate(cellPrefab, transform.position, Quaternion.identity, transform);
            var r = transform.GetComponent<RectTransform>();
            foreach (RectTransform child in _cell.GetComponentInChildren<RectTransform>())
            {
                child.localScale = new Vector3(r.rect.width / child.rect.width, r.rect.height / child.rect.height);
            }

            alive = true;
            return;
        }
        _cell.SetActive(a);
        alive = a;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (alive)
        {
            _cell = Instantiate(cellPrefab, transform.position,Quaternion.identity,transform);
            var r = transform.GetComponent<RectTransform>();
            foreach (RectTransform child in _cell.GetComponentInChildren<RectTransform>())
            {
                child.localScale = new Vector3(r.rect.width / child.rect.width, r.rect.height / child.rect.height);
            }
           
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("I was clicked fuckerrrr");
    }
}
