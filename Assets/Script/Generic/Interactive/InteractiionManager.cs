using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI promptText;    // 상호작용 프롬프트 텍스트
    [SerializeField] private float checkRadius = 3f;       // 탐지 반경
    [SerializeField] private LayerMask interactableLayers; // 탐지할 레이어 마스크

    private IInteractable currentInteractable;             // 현재 상호작용 가능한 객체
    private GameObject player;                             // 플레이어 객체

    private void Awake()
    {
        // 싱글톤 패턴
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // 플레이어 객체 초기화
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CheckInteractables();
        // 상호작용 키 입력 처리
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.OnInteract(player);
        }
    }

    // 근처의 상호작용 가능한 객체 탐색
    private void CheckInteractables()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, checkRadius, interactableLayers);
        IInteractable closest = null;
        float closestDistance = float.MaxValue;

        foreach (var col in colliders)
        {
            // 인터페이스를 가진 객체인지 검사
            if (col.TryGetComponent<IInteractable>(out var interactable))
            {
                float distance = Vector3.Distance(player.transform.position, col.transform.position);

                // 상호작용 가능 조건 확인
                if (distance <= interactable.GetInteractableDistance() && distance < closestDistance && interactable.CanInteract(player))
                {
                    closest = interactable;
                    closestDistance = distance;
                }
            }
        }

        // 가장 가까운 상호작용 가능한 객체 설정
        currentInteractable = closest;  
        UpdatePrompt();
    }

    // 상호작용 프롬프트 업데이트
    private void UpdatePrompt()
    {
        if (currentInteractable != null)
        {
            promptText.text = $"[E] {currentInteractable.GetInteractPrompt()}";
            promptText.gameObject.SetActive(true);
        }
        else
        {
            promptText.gameObject.SetActive(false);
        }
    }
}