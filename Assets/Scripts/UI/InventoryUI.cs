using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * InventoryUI
 * 플레이어 인벤토리 UI 관리
 * 기능 요약 : 
 * - UI 슬롯 생성
 * - 슬롯에 아이템과 아이콘 수량 표시
 * - 슬롯은 slotPrefab를 통해 동적으로 생성
 */
public class InventoryUI : MonoBehaviour
{
    Character character;

    public Transform slotParent;
    public GameObject slotPrefeb;

    public Sprite keySprite;
    public Sprite posionSprite;

    List<GameObject> slotList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        character = Character.Instance;
    }

    private void OnEnable()
    {
        if(character==null)
            character = Character.Instance;
        UpdateInventoryUI();
    }
    private void UpdateInventoryUI()
    {
        if (character.inventory == null)
            return;
        foreach (var slot in slotList)
        {
            if(slot!=null)
                Destroy(slot);
        }
        slotList.Clear();
        
        Dictionary<ItemType, int> itemCounts = new Dictionary<ItemType, int>();
        foreach(var item in character.inventory)
        {
            if (!itemCounts.ContainsKey(item))
                itemCounts[item] = 0;
            itemCounts[item]++;
        }

        foreach (var pair in itemCounts)
        {
            GameObject newSlot = Instantiate(slotPrefeb, slotParent);
            Image icon = newSlot.transform.Find("Icon").GetComponent<Image>();
            Text countText = newSlot.transform.Find("Count").GetComponent<Text>();

            switch (pair.Key)
            {
                case ItemType.Key:
                    icon.sprite = keySprite;
                    countText.text = "1";
                    break;
                case ItemType.Posion:
                    icon.sprite = posionSprite;
                    countText.text = character.posionCount.ToString();
                    break;
            }
            slotList.Add(newSlot);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
