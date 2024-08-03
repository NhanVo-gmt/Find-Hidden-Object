using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HiddenItemUI : MonoBehaviour
{
    [SerializeField] private Image           image;
    [SerializeField] private TextMeshProUGUI numText;

    private LevelItem levelItem;
    
    public void BindData(LevelItem levelItem)
    {
        this.levelItem = levelItem;
        
        ItemData item = ItemManager.Instance.GetItem(levelItem.Id);
        image.sprite = item.sprite;
        numText.SetText($"0/{levelItem.Number}");
    }

    public void UpdateItem(int num)
    {
        numText.SetText($"{num}/{levelItem.Number}");
    }
}
