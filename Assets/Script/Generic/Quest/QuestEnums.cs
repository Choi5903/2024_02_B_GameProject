using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyGame.QuestSystem
{
    //����Ʈ�� ���� ���¸� ��Ÿ���� ������


    public enum QuestStatus
    {
        NotStarted,                     //����Ʈ�� ���� ���۵��� ���� ����
        InProgress,                     //����Ʈ�� ���� �������� ����
        Completed,                      //����Ʈ�� �Ϸ�� ����
        Failed                          //����Ʈ�� ������ ����
    }

    public enum QuestType               //����Ʈ�� ������ �����ϴ� ������
    {
        Collection,                     //�������� �����ϴ� ����Ʈ
        Kill,                           //���͸� óġ�ϴ� ����Ʈ
        Dialog,                         //NPC�� ��ȭ�ϴ� ����Ʈ
        Excort                          //NPC�� ��ȣ/ȣ���ϴ� ����Ʈ
    }
}
