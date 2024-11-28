using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��� ��ȣ�ۿ� ������ ��ü�� �����ؾ��ϴ� �⺻ �������̽�
public interface IInteractable
{
    string GetInteractPrompt();              // ��ȣ�ۿ� �� ǥ���� �ؽ�Ʈ
    void OnInteract(GameObject player);      // ��ȣ�ۿ� �� ����� �޼���
    float GetInteractableDistance();         // ��ȣ�ۿ� ���� �Ÿ�
    bool CanInteract(GameObject player);     // ��ȣ�ۿ� ���� ����
}
