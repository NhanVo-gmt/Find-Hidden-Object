using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HiddenItemUI : MonoBehaviour
{
    [SerializeField] private Image           image;
    [SerializeField] private TextMeshProUGUI text;

    public void BindData(LevelItem levelItem)
    {
        ItemData item = ItemManager.Instance.GetItem(levelItem.Id);
        image.sprite = item.sprite;
    }
}
