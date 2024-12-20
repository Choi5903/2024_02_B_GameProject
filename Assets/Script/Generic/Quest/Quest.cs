using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyGame.QuestSystem
{
    public class Quest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestType Type { get; set; }
        public QuestStatus Status { get; set; }
        public int Level { get; set; }

        private List<IQuestCondition> conditions = new List<IQuestCondition>();
        private List<IQuestReward> rewards = new List<IQuestReward>();
        private List<string> prerequisiteQuestIds;

        public Quest(string id, string title, string description, QuestType type, int level)
        {
            Id = id; Title = title; Description = description; Type = type; Status = QuestStatus.NotStarted; Level = level;

            this.conditions = new List<IQuestCondition>();
            this.rewards = new List<IQuestReward>();
            this.prerequisiteQuestIds = new List<string>();
        }

        public List<IQuestCondition> GetConditions()
        {
            return conditions;
        }
        public void AddCondition(IQuestCondition condition)     //퀘스트에 완료 조건을 추가하는 메서드
        {
            conditions.Add(condition);
        }
        public void AddReward(IQuestReward reward)              //퀘스트에 보상을 추가하는 메서드
        {
            rewards.Add(reward);
        }
        public void Start()                                     //퀘스트를 시작하는 메서드
        {
            if (Status == QuestStatus.NotStarted)
            {
                Status = QuestStatus.InProgress;
                foreach (var condition in conditions)
                {
                    condition.Initialize();
                }
            }
        }

        public bool CheckCompletion()                           //퀘스트 완료 조건을 검사하는 메서드
        {
            if(Status != QuestStatus.InProgress) return false;
            return conditions.All(c => c.IsMet());
        }

        public void Complete(GameObject player)
        {
            if (Status != QuestStatus.InProgress) return;
            if (!CheckCompletion()) return;

            foreach (var reward in rewards)
            {
                reward.Grant(player);
            }

            Status = QuestStatus.Completed;
        }

        public float GetProgress()                                              //퀘스트의 전체 진행도를 계산하는 메서드
        {
            if (conditions.Count == 0) return 0;
            return conditions.Average(c => c.GetProgress());
        }

        public List<string> GetConditionDescriptions()                          //모든 퀘스트 조건의 설명을 가져오는 메서드
        {
            return conditions.Select(c => c.GetDescription()).ToList();
        }

        public List<string> GetRewardDescriptions()                             //모든 퀘스트 보상의 설명을 가져오는 메서드
        {
            return rewards.Select(r => r.GetDescription()).ToList();
        }
    }
}