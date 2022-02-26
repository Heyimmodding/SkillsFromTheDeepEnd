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

            
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            charge = Mathf.Clamp01(base.fixedAge / ChargeDuration);
            if(base.isAuthority)
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
            if(!ShouldKeepChargingAuthority())
            {
                outer.SetNextState(GetNextStateAuthority());
            }


        }

        protected virtual bool ShouldKeepChargingAuthority()
        {
            return base.IsKeyDownAuthority();
        }

        protected virtual EntityState GetNextStateAuthority()
        {
            return new Splitter
            {
                Charge = charge
            };

        }






        public float BaseChargeDuration;

        public float ChargeDuration;

        public float charge;
    }

}
