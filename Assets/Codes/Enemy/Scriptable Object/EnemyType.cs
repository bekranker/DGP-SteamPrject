using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "EnemyType", menuName = "Scriptable Objects/EnemyType")]
public class EnemyType : ScriptableObject
{
    [PropertySpace]
    [ShowInInspector, BoxGroup("ButtonsHealth", ShowLabel = false)] 
    public int Health;

    [PropertySpace]
    [BoxGroup("ButtonsHealth", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Decrease Health")]
    private void ButtonOnLeftHealth()
    {
        Health--;
        // Sol tarafta görünecek buton işlemleri
    }

    [PropertySpace]
    [BoxGroup("ButtonsHealth", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Increase Health")]
    private void ButtonOnRightHealth()
    {
        Health++;
        // Sağ tarafta görünecek buton işlemleri
    }



    [PropertySpace]
    [ShowInInspector, BoxGroup("ButtonsDamage", ShowLabel = false)] 
    public int Damage;
    

    [PropertySpace]
    [BoxGroup("ButtonsDamage", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Decrease Damage")]
    private void ButtonOnLeftDamage()
    {
        Damage--;
        // Sol tarafta görünecek buton işlemleri
    }

    [PropertySpace]
    [BoxGroup("ButtonsDamage", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Increase Damage")]
    private void ButtonOnRightDamage()
    {
        Damage++;
        // Sağ tarafta görünecek buton işlemleri
    }




    [PropertySpace]
    [ShowInInspector, BoxGroup("ButtonsSpeed", ShowLabel = false)] 
    public int Speed;

    [PropertySpace]
    [BoxGroup("ButtonsSpeed", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Decrease Speed")]
    private void ButtonOnLeftSpeed()
    {
        Speed--;
        // Sol tarafta görünecek buton işlemleri
    }

    [PropertySpace]
    [BoxGroup("ButtonsSpeed", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Increase Speed")]
    private void ButtonOnRightSpeed()
    {
        Speed++;
        // Sağ tarafta görünecek buton işlemleri
    }



    [PropertySpace]
    [ShowInInspector, BoxGroup("PropertyTriggerZone", ShowLabel = false)] 
    [Range(0.1f, 20)] public float TriggerZone;
    [PropertySpace]
    [ShowInInspector, BoxGroup("PropertyDistanceToPlayer", ShowLabel = false)]
    [Range(0.1f, 20)] public float DistanceToPlayer;
    [PropertySpace]
    [ShowInInspector, BoxGroup("PropertyDistanceToCombat", ShowLabel = false)]
    [Range(0.1f, 20)] public float DistanceToCombat;
    [PropertySpace]
    [ShowInInspector, BoxGroup("PropertyPatrollDelay", ShowLabel = false)] 
    [Range(0.1f, 20)] public float PatrollDelay;



    [PropertySpace]
    [ShowInInspector, BoxGroup("ButtonsPatrollMovementSpeed", ShowLabel = false)] 
    [Range(0.1f, 20)] public float PatrollMovementSpeed;


    [PropertySpace]
    [BoxGroup("ButtonsPatrollMovementSpeed", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Decrease Patroll Movement Speed")]
    private void ButtonOnLeftPatrollMovementSpeed()
    {
        PatrollMovementSpeed--;
        // Sol tarafta görünecek buton işlemleri
    }

    [PropertySpace]
    [BoxGroup("ButtonsPatrollMovementSpeed", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Increase Patroll Movement Speed")]
    private void ButtonOnRightPatrollMovementSpeed()
    {
        PatrollMovementSpeed++;
        // Sağ tarafta görünecek buton işlemleri
    }


    [PropertySpace]
    [ShowInInspector, BoxGroup("ButtonsAlertTriggerDelay", ShowLabel = false)] 
    [Range(0.1f, 20)] public float AlertTriggerDelay;


    [PropertySpace]
    [BoxGroup("ButtonsAlertTriggerDelay", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Decrease Alert Trigger Delay")]
    private void ButtonOnLeftAlertTriggerDelay()
    {
        AlertTriggerDelay--;
        // Sol tarafta görünecek buton işlemleri
    }

    [PropertySpace]
    [BoxGroup("ButtonsAlertTriggerDelay", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Increase Alert Trigger Delay")]
    private void ButtonOnRightAlertTriggerDelay()
    {
        AlertTriggerDelay++;
        // Sağ tarafta görünecek buton işlemleri
    }
    [PropertySpace]
    [ShowInInspector, BoxGroup("ButtonsCombatDelay", ShowLabel = false)] 
    [Range(0.1f, 20)] public float CombatDelay;


    [PropertySpace]
    [BoxGroup("ButtonsCombatDelay", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Decrease Combat Delay")]
    private void ButtonOnLeftCombatDelay()
    {
        CombatDelay--;
        // Sol tarafta görünecek buton işlemleri
    }

    [PropertySpace]
    [BoxGroup("ButtonsCombatDelay", ShowLabel = false)]
    [Button(ButtonSizes.Small, Name = "Increase Combat Delay")]
    private void ButtonOnRightCombatDelay()
    {
        CombatDelay++;
        // Sağ tarafta görünecek buton işlemleri
    }

    [PropertySpace]
    [ShowInInspector, BoxGroup("Component", ShowLabel = false)]
    public Patrolling PatrollType;
}