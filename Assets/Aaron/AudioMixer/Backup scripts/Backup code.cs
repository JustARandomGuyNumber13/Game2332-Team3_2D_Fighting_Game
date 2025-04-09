using UnityEngine;

public class Backupcode : MonoBehaviour
{
    /*private SO_SkillStat[] p1EquippedSkills = new SO_SkillStat[3];
    private SO_SkillStat[] p2EquippedSkills = new SO_SkillStat[3];*/

    /*//Player Skill Input Fields
    [Header("Player 1 Skill Inputs")]
    [SerializeField] private InputActionReference p1Skill1;
    [SerializeField] private InputActionReference p1Skill2;
    [SerializeField] private InputActionReference p1Skill3;

    [Header("Player 2 Skill Inputs")]
    [SerializeField] private InputActionReference p2Skill1;
    [SerializeField] private InputActionReference p2Skill2;
    [SerializeField] private InputActionReference p2Skill3;*/


    /*private void BindSkillInput()
    {
        EnableIA();

        //Player 1
        p1Skill1.action.performed += _ => PlaySkillAudio("Player 1", 0);
        p1Skill2.action.performed += _ => PlaySkillAudio("Player 1", 1);
        p1Skill3.action.performed += _ => PlaySkillAudio("Player 1", 2);

        //Player 2
        p2Skill1.action.performed += _ => PlaySkillAudio("Player 2", 0);
        p2Skill2.action.performed += _ => PlaySkillAudio("Player 2", 1);
        p2Skill3.action.performed += _ => PlaySkillAudio("Player 2", 2);
    }

    private void UnbindSkillInput()
    {
        DisableIA();

        //Player 1
        p1Skill1.action.performed -= _ => PlaySkillAudio("Player 1", 0);
        p1Skill2.action.performed -= _ => PlaySkillAudio("Player 1", 1);
        p1Skill3.action.performed -= _ => PlaySkillAudio("Player 1", 2);

        //Player 2
        p2Skill1.action.performed -= _ => PlaySkillAudio("Player 2", 0);
        p2Skill2.action.performed -= _ => PlaySkillAudio("Player 2", 1);
        p2Skill3.action.performed -= _ => PlaySkillAudio("Player 2", 2);
    }

    private void PlaySkillAudio(string playerName, int skillIndex)
    {
        SO_SkillStat skill = null;

        if (playerName == "Player 1")
        {
            skill = p1EquippedSkills[skillIndex];
        }
        else if (playerName == "Player 2")
        {
            skill = p2EquippedSkills[skillIndex];
        }

        if (skill != null && _sfxMapping.TryGetValue(skill, out AudioClip skillClip))
        {
            Debug.Log($"{playerName} triggered skill {skill.name} with audio {skillClip.name}");

            if (playerName == "Player 1")
            {
                Public_PlayP1SoundEffect(skillClip);
            }
            else if (playerName == "Player 2")
            {
                Public_PlayP2SoundEffect(skillClip);
            }
            else
            {
                Debug.LogWarning($"No SFX found for {playerName} skill at index {skillIndex}");
            }
        }
    }

    public void AssignSkillIndex(string playername, int skillIndex, SO_SkillStat skill)
    {
        if (playername == "Player 1")
        {
            p1EquippedSkills[skillIndex] = skill;
        }
        else if (playername == "Player 2")
        {
            p2EquippedSkills[skillIndex] = skill;
        }
        Debug.Log($"{playername} equipped {skill.name} in slot {skillIndex}");
    }*/


    /*p1Skill1.action.Enable();
        p1Skill2.action.Enable();
        p1Skill3.action.Enable();*/

    /*p2Skill1.action.Enable();
        p2Skill2.action.Enable();
        p2Skill3.action.Enable();*/

    /*p1Skill1.action.Disable();
        p1Skill2.action.Disable();
        p1Skill3.action.Disable();*/

    /*p2Skill1.action.Disable();
        p2Skill2.action.Disable();
        p2Skill3.action.Disable();*/


    /*if ( _scene.name == "Main-GamePlayer_Scene")
        {
            BindSkillInput();
        }

        else
        {
            UnbindSkillInput();
        }*/


    /*public void Public_PlaySkillSFX(SO_SkillStat skillStat)
    {
        if (skillStat == null)
        {
            Debug.LogWarning("SkillStat is null. Cannot play SFX.");
            return;
        }

        if (_sfxMapping.TryGetValue(skillStat, out AudioClip sfxClip))
        {
            // Get the calling GameObject during runtime (e.g., skill execution context)
            GameObject skillCaller = skillStat.SkillPrefab; // SkillPrefab might point to the instantiation context

            if (skillCaller == null)
            {
                Debug.LogWarning("Skill caller is null. Cannot determine which player triggered the skill.");
                return;
            }

            Debug.Log($"Skill triggered by: {skillCaller.name}");

            // Match the caller to player instances
            if (skillCaller.transform.IsChildOf(p1PrefabInst.transform))
            {
                Debug.Log($"Player 1 triggered skill {skillStat.name}, playing audio...");
                Public_PlayP1SoundEffect(_sfxMapping[skillStat]);
            }
            else if (skillCaller.transform.IsChildOf(p2PrefabInst.transform))
            {
                Debug.Log($"Player 2 triggered skill {skillStat.name}, playing audio...");
                Public_PlayP2SoundEffect(_sfxMapping[skillStat]);
            }
            else
            {
                Debug.LogWarning($"Skill caller does not match registered players for skill: {skillStat.name}. Playing default audio.");
                Public_PlaySoundEffect(_sfxMapping[skillStat]); // Fallback
            }


            //Debug.Log($"Playing SFX for skill: {skillStat.name}, AudioClip: {sfxClip.name}");
            //Public_PlaySoundEffect(sfxClip);
        }
        else
        {
            Debug.LogWarning($"No SFX found for skill: {skillStat.name}");
        }
    }*/
}
