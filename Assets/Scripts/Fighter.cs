using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonParameter;
// using Input;

public class Fighter : MonoBehaviour
{
    public int health;
    int combo;
    public int skillUse;
    int skillHold;  // Currently we only support hold one single skill. Collecting new skill will override the skill in hold.
    int buff;

    // status
    public int debuff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Fighter(bool isLeft) {
        this.health = 100;
        this.combo = 0;

        this.skillUse = CommonParameter.Skill.none;
        this.skillHold = CommonParameter.Skill.none;

        this.debuff = CommonParameter.Debuff.none;
        this.buff = CommonParameter.Buff.none;

        // if (isLeft) {
        //     this.KeyCode.Skill = CommonParameter.KeyCode.LeftSkill;
        // } else {
        //     this.KeyCode.Skill = CommonParameter.KeyCode.RightSkill;
        // }
    }

    public void ApplyDebuff(int debuffValue) {
        // TODO: to interact with the sound strack group
        this.debuff = debuffValue;

    }

    public void ApplyBuff(int buffValue) {
        // TODO: to interact with the sound strack group
        this.buff = buffValue;

    }

    void CastSkill() {
        // detect key value
        if (Input.GetKeyDown(KeyCode.F)) {
            this.skillHold = this.skillUse;
            this.skillUse = CommonParameter.Skill.none;
            // TODO: this may cause skill collection failure.
        }
    }

    void CollectSkill() {
        // detect key value to replace this.isCollectSkill
        if (this.combo > CommonParameter.comboThreshold && Input.GetKeyDown(KeyCode.D)) {
            this.skillHold = CommonParameter.Skill.one;
        }
    }
}
