using Blueprints;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.AssetLibrary;
using GameFoundation.Scripts.UIModule.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserData.Controller;
using Zenject;


public class GameRewardItemView : MonoBehaviour
{
    public Image rewardImg;
    public TextMeshProUGUI  rewardText;
    
    #region Inject

    [Inject] private IGameAssets     gameAssets;
    [Inject] private CurrencyManager currencyManager;

    #endregion
    
    public async UniTask BindData(LevelRewardRecord model)
    {
        CurrencyRecord currencyRecord = currencyManager.GetCurrencyById(model.RewardId);
        rewardText.text  = model.RewardNumber.ToString();
        rewardImg.sprite = await gameAssets.LoadAssetAsync<Sprite>(currencyRecord.Icon);
    }
}

