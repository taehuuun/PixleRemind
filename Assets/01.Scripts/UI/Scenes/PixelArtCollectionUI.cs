using System.Collections.Generic;
using UnityEngine;

public class PixelArtCollectionUI : MonoBehaviour
{
    public GameObject[] pages = new GameObject[3];
    
    public CollectTopicSlot collectTopicSlotPrefab;
    public CollectPixelArtSlot collectPixelArtSlotPrefab;
    public Transform collectTopicSlotContainer;
    public Transform collectPixelArtSlotContainer;
        
    private CollectTopicData _selectTopicData;
    private CollectPixelArtData _selectPixelArtData;

    private List<CollectTopicSlot> _collectTopicSlots = new List<CollectTopicSlot>();
    private List<CollectPixelArtSlot> _collectPixelArtSlots = new List<CollectPixelArtSlot>();
    
    private readonly int _createSlotCnt = 9;
    private void CreateSlots()
    {
        for (int i = 0; i < _createSlotCnt; i++)
        {
            CollectTopicSlot newCollectTopicSlot = Instantiate(collectTopicSlotPrefab, collectTopicSlotContainer);
            CollectPixelArtSlot newCollectPixelArtSlot = Instantiate(collectPixelArtSlotPrefab, collectPixelArtSlotContainer);
            
            newCollectTopicSlot.gameObject.SetActive(false);
            newCollectPixelArtSlot.gameObject.SetActive(false);
            
            _collectTopicSlots.Add(newCollectTopicSlot);
            _collectPixelArtSlots.Add(newCollectPixelArtSlot);
        }
    }
}