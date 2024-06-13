using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUIController : MonoBehaviour
{
    Image _image = null;
    // Start is called before the first frame update
    void Start()
    {
        _image = this.transform.GetChild(1).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        var selectedObj = EventSystem.current.currentSelectedGameObject;
        if(selectedObj == this.gameObject)
        {
            _image.enabled = true;
        }
        else
        {
            _image.enabled = false;
        }
    }
}
