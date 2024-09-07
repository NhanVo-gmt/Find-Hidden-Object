using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.AssetLibrary;
using GameFoundation.Scripts.Utilities.ObjectPool;
using Transactions.Blueprint;
using Transactions.Manager;
using Transactions.Model;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;
using Ease = Watermelon.Ease;
using Random = UnityEngine.Random;
using Tween = Watermelon.Tween;

public class AssetService
{
    private readonly ObjectPoolManager   objectPoolManager;
    private readonly ITransactionManager transactionManager;
    private readonly IGameAssets         gameAssets;

    private FloatingCloudSetting                                  floatingCloudSetting;
    private Dictionary<string, Dictionary<string, RectTransform>> assetFlyingTargetTransforms = new();

    public AssetService(ObjectPoolManager objectPoolManager, IGameAssets gameAssets)
    {
        this.objectPoolManager = objectPoolManager;
        this.gameAssets        = gameAssets;
        
        InitFloatingCloud();
    }
    
    private async void InitFloatingCloud()
    {
        this.floatingCloudSetting = new FloatingCloudSetting
        {
            CloudRadius      = 200,
            Prefab           = await this.gameAssets.LoadAssetAsync<GameObject>("CommonFloatingAsset"),
            // AppearAudioClip  = await this.gameAssets.LoadAssetAsync<AudioClip>("coin_appear"),
            // CollectAudioClip = await this.gameAssets.LoadAssetAsync<AudioClip>("coin_pickup")
        };
    }

    // public async UniTask<Sprite> GetCurrencyIcon(string currencyId)
    // {
    //     return await this.gameAssets.LoadAssetAsync<Sprite>(this.WalletManager.GetStaticData(currencyId).Icon);
    // }

    public async UniTask<Sprite> GetAssetIcon(string assetType, string assetId)
    {
        var assetIconPath = assetType switch
        {
            // AssetDefaultType.Currency => this.WalletManager.GetStaticData(assetId).Icon,
            // AssetDefaultType.Item => this.itemController.GetItemRecord(assetId).Icon,
            _ => string.Empty
        };

        if (string.IsNullOrEmpty(assetIconPath))
            return null;

        return await this.gameAssets.LoadAssetAsync<Sprite>(assetIconPath);
    }
    
    public void RegisterAssetFlyingTarget(string assetType, string assetId, RectTransform targetTransform)
    {
        if (!this.assetFlyingTargetTransforms.ContainsKey(assetType))
            this.assetFlyingTargetTransforms.Add(assetType, new Dictionary<string, RectTransform>());

        this.assetFlyingTargetTransforms[assetType][assetId] = targetTransform;
    }
    
    private bool TryGetAssetFlyingTarget(string assetType, string assetId, out RectTransform targetTransform)
    {
        targetTransform = null;
        if (!this.assetFlyingTargetTransforms.ContainsKey(assetType))
            return false;
        return this.assetFlyingTargetTransforms[assetType].TryGetValue(assetId, out targetTransform) ||
               // If assetId is empty, try to get default target
               this.assetFlyingTargetTransforms[assetType].TryGetValue("", out targetTransform);
    }
    
    private class FloatingCloudSetting
    {
        public GameObject Prefab;
        public AudioClip  AppearAudioClip;
        public AudioClip  CollectAudioClip;
        public float      CloudRadius;
    }
    
    // public async UniTask ClaimRewardWithAnimation(List<Asset> assets, Transform rewardTransform)
    // {
    //     var completeSource = new UniTaskCompletionSource();
    //     this.SpawnAssetsCloud(assets, rewardTransform, asset =>
    //         {
    //             this.transactionManager.ReceivePayout(asset);
    //         },
    //         () =>
    //         {
    //             completeSource.TrySetResult();
    //         });
    //             
    //     await completeSource.Task;
    // }
    //
    // public void SpawnAssetsCloud(List<Asset> assets, Transform rewardTransform, Action<Asset> onAssetHittedTarget, Action onComplete = null)
    // {
    //     for (var index = 0; index < assets.Count; index++)
    //     {
    //         var asset = assets[index];
    //         this.SpawnAssetCloud(asset.AssetType, asset.AssetId, rewardTransform, asset.Amount, delegate { onAssetHittedTarget?.Invoke(asset); }, index == assets.Count - 1 ? onComplete : null);
    //     }
    // }
    
    public void SpawnAssetsCloud(Sprite sprite, Transform rewardTransform, Transform targetRectTransform, int amount, Action onAssetHittedTarget = null, Action onComplete = null)
    {
        this.SpawnAssetCloud(sprite, rewardTransform, targetRectTransform, amount, delegate { onAssetHittedTarget?.Invoke(); }, onComplete : null);
    }
    
    public void SpawnAssetCloud(string assetType, string assetId, Sprite sprite, Transform rewardTransform, int elementsAmount, Action onAssetHittedTarget = null, Action onComplete = null)
    {
        if (!this.TryGetAssetFlyingTarget(assetType, assetId, out var targetRectTransform))
        {
            Debug.LogWarning($"Target transform for asset {assetType} - {assetId} not found!");
            onAssetHittedTarget?.Invoke();
            onComplete?.Invoke();
            return;
        }
            
        Debug.LogError(123);
        SpawnAssetCloud(sprite, rewardTransform, targetRectTransform, elementsAmount, onAssetHittedTarget, onComplete);
    }
    
    public void SpawnAssetCloud(string assetType, Sprite sprite, Transform rewardTransform, int elementsAmount, Action onAssetHittedTarget = null, Action onComplete = null)
    {
        if (!this.TryGetAssetFlyingTarget(assetType, assetType, out var targetRectTransform))
        {
            Debug.LogWarning($"Target transform for asset {assetType} - {assetType} not found!");
            onAssetHittedTarget?.Invoke();
            onComplete?.Invoke();
            return;
        }
            
        Debug.LogError(123);
        SpawnAssetCloud(sprite, rewardTransform, targetRectTransform, elementsAmount, onAssetHittedTarget, onComplete);
    }

    public void SpawnAssetCloud(Sprite    sprite,              Transform rewardTransform,
                                Transform targetRectTransform, int       elementsAmount,
                                Action    onAssetHittedTarget = null,
                                Action    onComplete          = null)
    {

        var targetObject = new GameObjectWrapper(targetRectTransform.gameObject);
        targetObject.SetNewParent(rewardTransform.GetComponentInParent<Canvas>().transform);
        this.floatingCloudSetting.Prefab.RecycleAll();

        // Play appear sound
        // if (this.floatingCloudSetting.AppearAudioClip != null)
        //     AudioController.PlaySound(this.floatingCloudSetting.AppearAudioClip);

        float   cloudRadius = this.floatingCloudSetting.CloudRadius;
        Vector3 centerPoint = rewardTransform.position;

        float defaultPitch         = 0.9f;
        bool  currencyHittedTarget = false;
        elementsAmount = Mathf.Min(elementsAmount, 100);
        for (int i = 0; i < elementsAmount; i++)
        {
            bool       isLastElement = i == elementsAmount - 1;
            GameObject elementObject = this.floatingCloudSetting.Prefab.Spawn();
            elementObject.transform.SetParent(targetRectTransform);

            //elementObject.transform.SetParent(targetRectTransform);
            //elementObject.transform.SetAsLastSibling();
            //elementObject.transform.SetParent(currencyCloud.coinsContainerRectTransform);

            elementObject.transform.position      = centerPoint;
            elementObject.transform.localRotation = Quaternion.identity;
            elementObject.transform.localScale    = Vector3.one;

            Image elementImage = elementObject.GetComponent<Image>();
            elementImage.sprite = sprite;
            elementImage.color  = Color.white.SetAlpha(0);

            float moveTime = Random.Range(0.6f, 0.8f);

            TweenCase     currencyTweenCase    = null;
            RectTransform elementRectTransform = (RectTransform)elementObject.transform;
            elementImage.DOFade(1, 0.2f, unscaledTime: true);
            elementRectTransform
                .DOAnchoredPosition(elementRectTransform.anchoredPosition + (Random.insideUnitCircle * cloudRadius),
                    moveTime, unscaledTime: true).SetEasing(Ease.Type.CubicOut)
                .OnComplete(delegate
                {
                    Tween.DelayedCall(0.1f, delegate
                    {
                        elementRectTransform.DOScale(0.3f, 0.5f, unscaledTime: true).SetEasing(Ease.Type.ExpoIn);
                        elementRectTransform.DOLocalMove(Vector3.zero, 0.5f, unscaledTime: true)
                            .SetEasing(Ease.Type.SineIn).OnComplete(delegate
                            {
                                if (!currencyHittedTarget)
                                {
                                    if (onAssetHittedTarget != null)
                                        onAssetHittedTarget.Invoke();

                                    currencyHittedTarget = true;
                                }

                                bool punchTarget = true;
                                if (currencyTweenCase != null)
                                {
                                    if (currencyTweenCase.state < 0.8f)
                                    {
                                        punchTarget = false;
                                    }
                                    else
                                    {
                                        currencyTweenCase.Kill();
                                    }
                                }

                                if (punchTarget)
                                {
                                    // Play collect sound
                                    // if (this.floatingCloudSetting.CollectAudioClip != null)
                                    //     AudioController.PlaySound(this.floatingCloudSetting.CollectAudioClip, pitch: defaultPitch);

                                    defaultPitch += 0.01f;

                                    currencyTweenCase = targetRectTransform.DOScale(1.2f, 0.15f, unscaledTime: true)
                                        .OnComplete(delegate
                                        {
                                            currencyTweenCase = targetRectTransform
                                                .DOScale(1.0f, 0.1f, unscaledTime: true).OnComplete(delegate
                                                {
                                                    Complete(isLastElement);
                                                });
                                        });
                                }
                                else
                                {
                                    Complete(isLastElement);
                                }

                                elementObject.transform.SetParent(targetRectTransform);
                                elementRectTransform.Recycle();
                            });
                    }, unscaledTime: true);
                });
        }

        void Complete(bool isLastElement)
        {
            if (isLastElement)
            {
                onComplete?.Invoke();
                targetObject.SetOriginParent();
            }
        }
    }
}