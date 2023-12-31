using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : ItemObject
{
    [SerializeField] private ComboScriptableObject comboScriptableObject;
    public override void Interact(GameObject target)
    {
        var playerCombat = target.GetComponent<PlayerCombat>();
        if (playerCombat is null)
        {
            Debug.LogError("player combat invalid");
            return;
        }

        playerCombat.AddCombo(comboScriptableObject);
        base.Interact(target);
    }
}
