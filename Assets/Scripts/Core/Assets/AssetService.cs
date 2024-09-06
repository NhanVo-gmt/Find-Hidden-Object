using System;
using DG.Tweening;
using GameFoundation.Scripts.Utilities.ObjectPool;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;
using Ease = Watermelon.Ease;
using Random = UnityEngine.Random;
using Tween = Watermelon.Tween;

public class AssetService
{
    private readonly ObjectPoolManager objectPoolManager;

    private FloatingCloudSetting floatingCloudSetting;

    public AssetService(ObjectPoolManager objectPoolManager)
    {
        this.objectPoolManager = objectPoolManager;
    }
    
    private class FloatingCloudSetting
    {
        public GameObject Prefab;
        public AudioClip  AppearAudioClip;
        public AudioClip  CollectAudioClip;
        public float      CloudRadius;
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

            float   cloudRadius = 10f;
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
                elementRectTransform.DOAnchoredPosition(elementRectTransform.anchoredPosition + (Random.insideUnitCircle * cloudRadius), moveTime, unscaledTime: true).SetEasing(Ease.Type.CubicOut)
                    .OnComplete(delegate
                    {
                        Tween.DelayedCall(0.1f, delegate
                        {
                            elementRectTransform.DOScale(0.3f, 0.5f, unscaledTime: true).SetEasing(Ease.Type.ExpoIn);
                            elementRectTransform.DOLocalMove(Vector3.zero, 0.5f, unscaledTime: true).SetEasing(Ease.Type.SineIn).OnComplete(delegate
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
                                    if (this.floatingCloudSetting.CollectAudioClip != null)
                                        // AudioController.PlaySound(this.floatingCloudSetting.CollectAudioClip, pitch: defaultPitch);

                                    defaultPitch += 0.01f;

                                    currencyTweenCase = targetRectTransform.DOScale(1.2f, 0.15f, unscaledTime: true).OnComplete(delegate
                                    {
                                        currencyTweenCase = targetRectTransform.DOScale(1.0f, 0.1f, unscaledTime: true).OnComplete(delegate { Complete(isLastElement); });
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