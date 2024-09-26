using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public PlayerTarget player;
    public EnemyTarget enemy;

    public List<EnemyTarget> enemyTargets;

    public Skill<ISkillTarget, DamageEffect> fireBall;
    public Skill<PlayerTarget, HealEffect> healSpell;
    public Skill<ISkillTarget, DamageEffect> multiTargeSkill;

    // Start is called before the first frame update
    void Start()
    {
        fireBall = new Skill<ISkillTarget, DamageEffect>("Fireball", new DamageEffect(20));
        healSpell = new Skill<PlayerTarget, HealEffect>("Heal", new HealEffect(30));
        multiTargeSkill = new Skill<ISkillTarget, DamageEffect>("AoE Attack", new DamageEffect(10));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fireBall.Use(enemy);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            healSpell.Use(player);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach(var target in enemyTargets)
            {
                multiTargeSkill.Use(target);
            }
        }
    }
}
