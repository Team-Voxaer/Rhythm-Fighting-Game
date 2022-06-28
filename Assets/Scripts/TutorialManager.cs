using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialManager : ScoreManager
{
    public TutorialText tutorialText;

    /* Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    */
    
    public override void CastSkill() {
        CheckSkills();
        if (lsSkill.Count > 0) {
            if (lsSkill[0] == skillCodeDefend) { // Todo: Change to heal
                player.Defend(); 
            } else if (lsSkill[0] == skillCodeAttack) {
                player.Attack();
            } else if (lsSkill[0] == skillCodeSword) {
                player.UseSword();
                tutorialText.countdown(2);
            } else if (lsSkill[0] == skillCodeGrandCross) {
                player.UseGrandCross();
                tutorialText.countdown(3);
            } else if (lsSkill[0] == skillCodeThunder) {
                player.UseThunder();
                tutorialText.countdown(1);
            } else if (lsSkill[0] == skillCodeHealing) {
                player.UseHealing();
                tutorialText.countdown(4);
            }
            tutorialText.countdown(0);

            // Zhian Li: We always upload the Analytic when the AI is disabled
            // or when we are tracking the left player (which we always upload)
            if (!GameManager.CheckAI() || player.gameObject.name == "LightBandit"){
                 // Send AnalyticsManager combo data
                 AnalyticManager.OnComboReleased(lsSkill[0]);
            }

            lsSkill.RemoveAt(0);
        } else {
            // TODO: a visualization for no skills when casting
        }
    }
}
