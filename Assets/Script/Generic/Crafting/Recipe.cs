using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.CraftingSystem
{
    [System.Serializable]
    public class Recipe
    {
        public string recipeId;                             //������ ���� ID
        public IItem resultItem;                            //��� ������
        public int resultAmount;                            //���� ����
        public Dictionary<int, int> requiredMaterails;      //�ʿ��� ��� <������ ID, ����>
        public int requiredLevel;                           //�䱸 ���� ����
        public float baseSuccessRate;                       //�⺻ ���� Ȯ��
        public float craftTiem;                             //���� �ð�
        
        public Recipe(string id, IItem result, int amount)
        {
            recipeId = id;
            resultItem = result;
            resultAmount = amount;
            requiredMaterails = new Dictionary<int, int>();
            requiredLevel = 1;
            baseSuccessRate = 1;
            craftTiem = 0;
        }

        public void AddRequiredMaterial(int itemId, int amount)
        {
            if (requiredMaterails.ContainsKey(itemId))
            {
                requiredMaterails[itemId] += amount;
            }
            else
            {
                requiredMaterails[itemId] = amount;
            }
        }
    }
}