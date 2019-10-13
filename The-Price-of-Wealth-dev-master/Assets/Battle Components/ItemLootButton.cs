using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemLootButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public bool inBag = false;
    [HideInInspector]
    public Transform bagContainer;
    public Transform lootContainer;
    public TextMeshProUGUI descriptor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData e) {
		descriptor.text = item.GetDescription();
	}

	public void OnPointerExit(PointerEventData e) {
		descriptor.text = "";
	}

    public void ToggleLocation(){
        if(!inBag && bagContainer.childCount < 10) {
            transform.SetParent(bagContainer);
            inBag = true;
        } else if (inBag) {
            transform.SetParent(lootContainer);
            inBag = false;
        }
    }

    
}
