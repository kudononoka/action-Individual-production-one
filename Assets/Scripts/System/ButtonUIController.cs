using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUIController : MonoBehaviour
{
    Image _image = null;

    [SerializeField]
    SceneState _nextSceneState;
    // Start is called before the first frame update
    void Start()
    {
        _image = this.transform.GetChild(1).GetComponent<Image>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => GameManager.Instance.ChangeScene(_nextSceneState));
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
