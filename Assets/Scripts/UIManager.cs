using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    [Header("버튼")]
    [SerializeField] private Button doorButton;
    [SerializeField] private Button backButton;
    
    [Header("문닫힘 효과")]
    [SerializeField] private GameObject door;
    [SerializeField] private float doorSpeed = 0.5f;
    [SerializeField] private float minCloseStayTime = 1f;

    private Action backEffect;
    private Vector2 originPos;

    // Start is called before the first frame update
    void Start()
    {
        doorButton.onClick.AddListener(OnDoor);
        originPos = door.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDoor()
    {
        backEffect = OnDoor;
        door.transform.DOLocalMoveY(0, 1f *  doorSpeed);
        StartCoroutine(OffDoor());
    }

    private IEnumerator OffDoor()
    {
        yield return new WaitForSeconds(minCloseStayTime);
        door.transform.DOLocalMoveY(originPos.y, 1f *  doorSpeed);
    }
}
