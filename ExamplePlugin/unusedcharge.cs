using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using EntityStates;
using UnityEngine;

namespace ExamplePlugin
{
    class Charge : BaseSkillState
    {   public override void OnEnter()
        {
            base.OnEnter();
            ChargeDuration = BaseChargeDuration / base.attackSpeedStat;
            animator = base.GetModelAnimator();
            childLocator = base.GetModelChildLocator();
            Transform transform = this.childLocator.FindChild("MuzzleFace") ?? base.characterBody.coreTransform;
            if (transform && chargeEffectPrefab)
            {
                this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(chargeEffectPrefab, transform.position, transform.rotation);
                this.chargeEffectInstance.transform.parent = transform;
                ScaleParticleSystemDuration component = chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
                ObjectScaleCurve component2 = chargeEffectInstance.GetComponent<ObjectScaleCurve>();
                if (component)
                {
                    component.newDuration = ChargeDuration;
                }
                if (component2)
                {
                    component2.timeMax = ChargeDuration;
                }
            }
            DefaultCrosshair = base.characterBody.crosshairPrefab;
            if (!CrosshairPrefab)
            {
                CrosshairPrefab = Resources.Load<GameObject>("prefabs/crosshair/MageCrosshair");
            }
            if (CrosshairPrefab)
            {
                base.characterBody.crosshairPrefab = CrosshairPrefab;
            }
            base.StartAimMode(ChargeDuration + 2f, false);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            charge = Mathf.Clamp01(base.fixedAge / ChargeDuration);
            if (base.isAuthority)
            {
                AuthorityFixedUpdate();
            }
          
        }

        public override void Update()
        {
            base.Update();
            Mathf.Clamp01(base.age / ChargeDuration);
        }

        private void AuthorityFixedUpdate()
        {
          
            if (!ShouldKeepChargingAuthority())
            {
                outer.SetNextState(GetNextStateAuthority());
            }


        }

        public override void OnExit()
        {   if(base.characterBody)
            {
                base.characterBody.crosshairPrefab = DefaultCrosshair;
            }
            base.OnExit();
        }
        protected virtual bool ShouldKeepChargingAuthority()
        {
            return base.IsKeyDownAuthority();
        }

        protected virtual EntityState GetNextStateAuthority()
        {
            if(base.fixedAge >= mincharge || base.fixedAge >= ChargeDuration)
            {
                
            }
            return new Splitter
            {
                 
            };

        }





        public static GameObject CrosshairPrefab;

        public static GameObject chargeEffectPrefab = Resources.Load<GameObject>("prefabs/effects/muzzleflashes/muzzleflashmagefire");

        private GameObject chargeEffectInstance;

        public float mincharge = 3f;

        public float ChargeDuration;

        public float BaseChargeDuration = 6f;

        public static float charge;

        private Animator animator;

        // Token: 0x040001CA RID: 458
        private ChildLocator childLocator;

        public GameObject DefaultCrosshair;
    }

}
