using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonParameter;
using Input;

public class Fighter : MonoBehaviour
{
    public int health;
    int combo;
    public bool isCollectSkill;
    public int skillUse;
    int skillHold;  // Currently we only support hold one single skill. Collecting new skill will override the skill in hold.

    // status
    public int debuff;

    // Start is called before the first frame update
    void Start(int isLeftOrRight)
    {
        this.health = 100;
        this.combo = 0;

        this.isCollectSkill = 0;  // To be deleted

        this.skillUse = CommonParameter.Skill.none;
        this.skillHold = CommonParameter.Skill.none;

        this.debuff = CommonParameter.Debuff.none;
        this.buff = CommonParameter.Buff.none;

        if (isLeft) {
            this.KeyCode.Skill = CommonParameter.KeyCode.LeftSkill;
        } else {
            this.KeyCode.Skill = CommonParameter.KeyCode.RightSkill;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDebuff(int debuffValue) {
        // TODO: to interact with the sound strack group
        self.debuff = debuffValue;

    }

    public void ApplyBuff(int buffValue) {
        // TODO: to interact with the sound strack group
        self.buff = buffValue;

    }

    void CastSkill() {
        // detect key value
        if (Input.GetKeyDown(CommonParameter.Userkey.skill)) {
            this.skillHold = this.skillUse;
            this.skillUse = CommonParameter.Skill.none;
            // TODO: this may cause skill collection failure.
        }
    }

    void CollectSkill() {
        // detect key value to replace this.isCollectSkill
        if (this.combo > CommonParameter.comboThreshold && this.isCollectSkill) {
            this.skillHold = CommonParameter.Skill.one;
        }
    }
}
