using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public static class BattleStatic {
    public static Battle instance;
    
    public static void AttachStatic(Battle b) {
        instance = b;
        SpecialStatic.AttachStatic();
        ItemStatic.AttachStatic();
    } 
}

public static class SpecialStatic {
    public static GameObject description;
    public static void AttachStatic() {
        description = BattleStatic.instance.specialMenu.transform.RecursiveFind("Description").gameObject;

    } 
}

public static class ItemStatic {
    public static ItemSpace instance;
    public static TextMeshProUGUI description;
    public static void AttachStatic() {
        instance = BattleStatic.instance.itemSpace.GetComponent<ItemSpace>();
        description = BattleStatic.instance.itemSpace.transform.RecursiveFind("Description").gameObject.GetComponent<TextMeshProUGUI>();
    }
}