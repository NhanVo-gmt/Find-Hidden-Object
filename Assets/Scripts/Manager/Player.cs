using GameFoundation.Scripts.Utilities.Extension;
using UnityEngine;
using UserData.Controller;
using Zenject;

public class Player : MonoBehaviour
{
    #region Inject

    [Inject] private LevelManager    levelManager;

    #endregion

    [Header("Find")]
    [SerializeField] private string         sound = "pickup_sound";
    [SerializeField] private ParticleSystem findParticle;
    
    [Header("Hint")]
    [SerializeField] private Hint           hint;
    
    private void Start()
    {
        this.GetCurrentContainer().Inject(this);
    }

    private void OnEnable()
    {
        levelManager.OnUseHint += UseHint;
    }

    private void OnDisable()
    {
        levelManager.OnUseHint -= UseHint;
    }

    private void Update()
    {
        GetTouchInput();
    }

    void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(touch.position));
                
                if (rayHit.transform != null && rayHit.transform.TryGetComponent<Item>(out Item item))
                {
                    FindItem(item);
                }
            }
        }
    }

    void FindItem(Item item)
    {
        MasterAudio.Instance.PlaySound(sound);
        levelManager.SelectItem(item.Id, item.Index);
        item.Click();

        findParticle.transform.position = item.transform.position;
        findParticle.Play();
    }

    void UseHint()
    {
        hint.ChooseRandomTarget();
    }
}
