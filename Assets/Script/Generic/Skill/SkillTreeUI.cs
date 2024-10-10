using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    public SkillTree skillTree;
    public GameObject skillNodePrefabs;
    public RectTransform skillTreePanel;
    public float NodeSpacing = 100f;
    public Text SkillPointText;
    public int totalSkillPoint = 10;

    private Dictionary<string, Button> skillButtons = new Dictionary<string, Button>();

    void Start()
    {
        InitalizeSkillTree();
        CreateSkillTreeUI();
        UpdateSkillPointsUI();
    }

    void InitalizeSkillTree()
    {
        skillTree = new SkillTree();

        skillTree.AddNode(new SkillNode("Fireball1", "Fireball1",
            new Skill<ISkillTarget, DamageEffect>("Fireball1", new DamageEffect(20)),
            new Vector2(0, 0), "Fireball1", 1));

        skillTree.AddNode(new SkillNode("Fireball2", "Fireball2",
            new Skill<ISkillTarget, DamageEffect>("Fireball2", new DamageEffect(30)),
            new Vector2(1, 0), "Fireball1", 2, new List<string> { "Fireball1" }));

        skillTree.AddNode(new SkillNode("Fireball3", "Fireball3",
            new Skill<ISkillTarget, DamageEffect>("Fireball3", new DamageEffect(40)),
            new Vector2(2, 0), "Fireball1", 3, new List<string> { "Fireball2" }));

        skillTree.AddNode(new SkillNode("FireBolt1", "FireBolt1",
            new Skill<ISkillTarget, DamageEffect>("FireBolt1", new DamageEffect(50)),
            new Vector2(0, 1), "FireBolt", 1));

        skillTree.AddNode(new SkillNode("FireBolt2", "FireBolt2",
            new Skill<ISkillTarget, DamageEffect>("FireBolt2", new DamageEffect(90)),
            new Vector2(1, 1), "FireBolt", 2, new List<string> { "FireBolt1" }));

        skillTree.AddNode(new SkillNode("FireBolt3", "FireBolt3",
            new Skill<ISkillTarget, DamageEffect>("FireBolt3", new DamageEffect(140)),
            new Vector2(2, 1), "FireBolt", 3, new List<string> { "FireBolt2" }));

        skillTree.AddNode(new SkillNode("FireBolt4", "FireBolt4",
            new Skill<ISkillTarget, DamageEffect>("FireBolt4", new DamageEffect(200)),
            new Vector2(3, 1), "FireBolt", 4, new List<string> { "FireBolt3" }));
    }

    void CreateSkillTreeUI()
    {
        foreach (var node in skillTree.Nodes)
        {
            CreateSkillNodeUI(node);
        }
    }
    void CreateSkillNodeUI(SkillNode node)
    {
        GameObject nodeObj = Instantiate(skillNodePrefabs, skillTreePanel);
        RectTransform rectTransform = nodeObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = node.Position * NodeSpacing;

        Button button = nodeObj.GetComponent<Button>();
        Text text = nodeObj.GetComponentInChildren<Text>();
        text.text = node.Name;

        button.onClick.AddListener(() => OnSkillNodeClicked(node.Id));
        skillButtons[node.Id] = button;
        UpdateNodeUI(node);
    }

    private void OnSkillNodeClicked(string skillId)
    {
        SkillNode node = skillTree.GetNode(skillId);

        if (node == null) return;

        if (node.isUnlocked)
        {
            if (skillTree.LockSkill(skillId))
            {
                totalSkillPoint++;
                UpdateSkillPointsUI();
                UpdateNodeUI(node);
                UpdateConnectedSkills(skillId);
            }
            else
            {
                Debug.Log("���� ���� ��ų�� �־ ������ �ȵ˴ϴ�.");
            }
        }
        else if(totalSkillPoint > 0 && CanUnlockSkill(node))
        {
            if(skillTree.UnlockSkill(skillId))
            {
                totalSkillPoint--;
                UpdateSkillPointsUI();
                UpdateNodeUI(node);
                UpdateConnectedSkills(skillId);
            }
        }
    }

    private void UpdateNodeUI(SkillNode node)
    {
        if (skillButtons.TryGetValue(node.Id, out Button button))
        {
            bool canUnlock = !node.isUnlocked;
            button.interactable = (canUnlock && totalSkillPoint > 0) || node.isUnlocked;
            button.GetComponent<Image>().color = node.isUnlocked ? Color.green : (canUnlock ? Color.yellow : Color.red);
        }
    }

    private bool CanUnlockSkill(SkillNode node)
    {
        foreach (var requiredSkillId in node.RequiredSkillds)
        {
            if(!skillTree.IsSkillUnlock(requiredSkillId))
            {
                return false;
            }
        }

        return true;
    }

    void UpdateSkillPointsUI()
    {
        SkillPointText.text = $"Skill Points: {totalSkillPoint}";
    }

    void UpdateConnectedSkills(string skillId)
    {
        foreach(var node in skillTree.Nodes)
        {
            if(node.RequiredSkillds.Contains(skillId))
            {
                UpdateNodeUI(node);
            }
        }
    }

}