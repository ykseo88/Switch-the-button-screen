using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [Header("화면상태")] 
    [SerializeField] private Image startPage;
    [SerializeField] private Image endPage;
    
    [Header("버튼")]
    [SerializeField] private Button doorButton;
    [SerializeField] private Button backButton;
    
    [Header("문닫힘 효과")]
    [SerializeField] private GameObject door;
    [SerializeField] private float doorSpeed = 0.5f;
    [SerializeField] private float minCloseStayTime = 1f;
    [SerializeField] private float shakeMaintainTime = 0.2f;
    [SerializeField] private float shakeDistance = 0.1f;
    [SerializeField] private GameObject hammer;

    [Header("음향 효과")] 
    [SerializeField] private AudioClip doorLand;
    [SerializeField] private AudioClip click;
    public AudioClip hammerslam;
    
    [Header("참조")]
    public SoundManager soundManager;
    
    private Action<bool> _backEffect;
    private Action Change;
    private Vector2 originPos;
    private Vector2 shakeOriginPos;
    private Camera mainCam;
    

    // Start is called before the first frame update
    void Start()
    {
        doorButton.onClick.AddListener(() => OnDoorEffect(true));
        backButton.onClick.AddListener(Back);
        originPos = door.transform.position;
        hammer.transform.TryGetComponent(out AnimationEvents ani);
        ani.uiManager = this;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Back()
    {
        _backEffect(false);
        
    }

    private async void OnDoorEffect(bool isToEnd)
    {
        soundManager.OnSound(click);
        _backEffect = OnDoorEffect; // 복귀 효과 저장
        await DownDoor();
        OnShake();
        soundManager.OnSound(doorLand);
        await ChangePage(isToEnd);//화면 전환 대기
        if (isToEnd) backButton.gameObject.SetActive(false);
        await Task.Delay((int)minCloseStayTime*1000);//
        await UpDoor();
        hammer.SetActive(isToEnd);
    }

    private async Task ChangePage(bool isToEnd)
    {
        endPage.gameObject.SetActive(isToEnd);
        await Task.Yield();
    }

    private async Task DownDoor()
    {
        await door.transform.DOLocalMoveY(0, 1f * doorSpeed).AsyncWaitForCompletion();
    }

    private async Task UpDoor()
    {
        await door.transform.DOLocalMoveY(originPos.y, 1f * doorSpeed).AsyncWaitForCompletion();
    }

    public void OnShake()
    {
        mainCam.DOShakePosition(shakeMaintainTime, shakeDistance);
    }
}
