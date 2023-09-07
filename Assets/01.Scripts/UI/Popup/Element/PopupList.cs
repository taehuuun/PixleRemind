using System.Collections.Generic;
using UnityEngine;

public class PopupList : PopupElement
{
    [SerializeField] private Transform listContainer;
    [SerializeField] private GameObject listItemPrefab;

    private List<GameObject> _listItems = new List<GameObject>();

    public void AddItem()
    {
        GameObject newItem = Instantiate(listItemPrefab, listContainer);
        _listItems.Add(newItem);
    }

    public void ClearList()
    {
        foreach (var item in _listItems)
        {
            Destroy(item);
        }
        
        _listItems.Clear();
    }
    
    public override void ResetElements()
    {
        ClearList();
    }
    public override void UpdateElements() { }
}