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
    
    [Header("참조")]

    private Action<bool> _backEffect;
    private Action Change;
    private Vector2 originPos;
    

    // Start is called before the first frame update
    void Start()
    {
        doorButton.onClick.AddListener(() => OnDoorEffect(true));
        backButton.onClick.AddListener(Back);
        originPos = door.transform.position;
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
        _backEffect = OnDoorEffect; // 복귀 효과 저장
        await DownDoor();
        await ChangePage(isToEnd);//화면 전환 대기
        await Task.Delay((int)minCloseStayTime*1000);//
        door.transform.DOLocalMoveY(originPos.y, 1f *  doorSpeed);
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
}
