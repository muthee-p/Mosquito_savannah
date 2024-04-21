using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventories")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot>Container = new  List<InventorySlot>();
    public void AddItem(ItemObject _item,int _amount, Sprite _sprite){
        bool hasItem = false;

        for (int i = 0; i<Container.Count; i++){
            if (Container[i].item == _item){
                Container[i].item.sprite = _sprite;
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem){
            Container.Add(new InventorySlot(_item, _amount, _sprite));
        }
    }
}

[System.Serializable]
public class InventorySlot{
    public ItemObject item;
    public int amount;
    public Sprite sprite;
    public InventorySlot(ItemObject _item, int _amount, Sprite _sprite){
        item = _item;
        amount = _amount;
        sprite = _sprite;
    }
    public void AddAmount(int value){
        amount += value;
    }
}
