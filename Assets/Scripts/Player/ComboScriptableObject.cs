using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Normal Attack")]
public class ComboScriptableObject : ScriptableObject
{
    public PlayerComboType comboType;
    public AnimatorOverrideController animatorOv;
    public float damage; 
    public bool loop;
    public float stunTime;
    public bool isStiff;
    public float nextComboInterval;
    public float comboVitality;
    public Vector3 assaultDirection;
    public AnimationCurve assaultSpeedCurve;
    public GameObject[] vfxPrefabs;

    public ComboAnimation Init(Transform vfxParent)
    {
        ComboAnimation comboAnimation = new ComboAnimation(comboType, animatorOv, damage, loop, isStiff, stunTime, 
            nextComboInterval, comboVitality, assaultDirection, assaultSpeedCurve);

        foreach (var vfxPrefab in vfxPrefabs)
        {
            var vfxModifier = Instantiate(vfxPrefab, vfxParent, false).GetComponent<ParticleModifier>();
            if (vfxModifier is not null) comboAnimation.Effects.Add(vfxModifier);
            else Debug.LogError("vfx modifier is not valid");
        }

        return comboAnimation;
    }
}
