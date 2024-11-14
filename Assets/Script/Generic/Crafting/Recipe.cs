using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.CraftingSystem
{
    [System.Serializable]
    public class Recipe
    {
        public string recipeId;                             //레시피 고유 ID
        public IItem resultItem;                            //결과 아이템
        public int resultAmount;                            //제작 개수
        public Dictionary<int, int> requiredMaterails;      //필요한 재료 <아이템 ID, 수량>
        public int requiredLevel;                           //요구 제작 레벨
        public float baseSuccessRate;                       //기본 성공 확률
        public float craftTiem;                             //제작 시간
        
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