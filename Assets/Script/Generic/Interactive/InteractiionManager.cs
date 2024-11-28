using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI promptText;    // ��ȣ�ۿ� ������Ʈ �ؽ�Ʈ
    [SerializeField] private float checkRadius = 3f;       // Ž�� �ݰ�
    [SerializeField] private LayerMask interactableLayers; // Ž���� ���̾� ����ũ

    private IInteractable currentInteractable;             // ���� ��ȣ�ۿ� ������ ��ü
    private GameObject player;                             // �÷��̾� ��ü

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // �÷��̾� ��ü �ʱ�ȭ
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CheckInteractables();
        // ��ȣ�ۿ� Ű �Է� ó��
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.OnInteract(player);
        }
    }

    // ��ó�� ��ȣ�ۿ� ������ ��ü Ž��
    private void CheckInteractables()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, checkRadius, interactableLayers);
        IInteractable closest = null;
        float closestDistance = float.MaxValue;

        foreach (var col in colliders)
        {
            // �������̽��� ���� ��ü���� �˻�
            if (col.TryGetComponent<IInteractable>(out var interactable))
            {
                float distance = Vector3.Distance(player.transform.position, col.transform.position);

                // ��ȣ�ۿ� ���� ���� Ȯ��
                if (distance <= interactable.GetInteractableDistance() && distance < closestDistance && interactable.CanInteract(player))
                {
                    closest = interactable;
                    closestDistance = distance;
                }
            }
        }

        // ���� ����� ��ȣ�ۿ� ������ ��ü ����
        currentInteractable = closest;  
        UpdatePrompt();
    }

    // ��ȣ�ۿ� ������Ʈ ������Ʈ
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