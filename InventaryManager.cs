using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaryManager : MonoBehaviour
{
    public static InventaryManager Instance { get; private set; }

    public enum ItemType { MADNESS, HUNGER, THIRST, RADIATION }
    public int resources = 4;

    [System.Serializable]
    public class ItemProperties
    {
        public int id;
        public string name;
        public ItemType type;
        public bool active;
        public int modifier;
    }

    public List<ItemProperties> items = new List<ItemProperties>()
    {
        new ItemProperties {id = 0, name ="Book", type = ItemType.MADNESS, active = false, modifier = -10},
        new ItemProperties {id = 1, name ="Diary", type = ItemType.MADNESS, active = false, modifier = 10},
    };

    private void Awake()
    {
        Instance = this;
    }
}
