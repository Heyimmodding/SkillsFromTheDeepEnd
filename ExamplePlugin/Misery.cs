using System;
using System.Collections.Generic;
using System.Text;
using EntityStates;
using RoR2;
using UnityEngine;
using R2API;

namespace Skillsfromthedeepend
{
    class Misery : BaseSkillState
    {
        public override void OnEnter()
        {
            CharacterBody characterBody = base.characterBody;
            AcridComponent = base.GetComponent<CrocoDamageTypeController>();
            CorePosition = characterBody.corePosition;
            AcridCryAboutit();
        }
        public void AcridCryAboutit()
        {
            Util.PlaySound("Play_acrid_shift_land", base.gameObject);
            if (base.isAuthority && characterBody)
            {
                DamageType damageType = AcridComponent ?
                AcridComponent.GetDamageType() : DamageType.Generic;
                
                EffectManager.SpawnEffect(Effect, new EffectData
                { scale = Radius,
                    origin = CorePosition

                }, true);

                BlastAttack blastAttack = new BlastAttack
                {
                    position = CorePosition,
                    radius = Radius,
                    falloffModel = BlastAttack.FalloffModel.None,
                    attacker = base.gameObject,
                    baseDamage = DamageCoefficent * characterBody.damage,
                    baseForce = NormalForce,
                    bonusForce = Vector3.zero,
                    attackerFiltering = AttackerFiltering.NeverHitSelf,
                    crit = base.RollCrit(),
                    procChainMask = default(ProcChainMask),
                    procCoefficient = 1.0f,
                    teamIndex = base.teamComponent.teamIndex,
                    damageType = damageType,
                    damageColorIndex = DamageColorIndex.Poison


                };
                DamageAPI.AddModdedDamageType(blastAttack, Deepend.acridBuffHandler);
                blastAttack.Fire();
            }

        }

        public CrocoDamageTypeController AcridComponent;

        public Vector3 CorePosition;

        public static GameObject Effect = Resources.Load<GameObject>("prefabs/effects/impacteffects/CrocoLeapExplosion");

        public static float Radius = 20f;

        public static float DamageCoefficent = 2.0f;

        public static float NormalForce = 1000f;

        
    }
}
