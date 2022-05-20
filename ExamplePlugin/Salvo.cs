using System;
using System.Collections.Generic;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using EntityStates;


namespace Skillsfromthedeepend
{
	// Token: 0x02000BCD RID: 3021
	public class Splitter : BaseSkillState
	{

		private void FireBullet(string targetMuzzle)
		{
			Util.PlaySound(Splitter.firePistolSoundString, base.gameObject);
			if (Splitter.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(Splitter.muzzleEffectPrefab, base.gameObject, targetMuzzle, false);
			}
			base.AddRecoil(-0.4f * Splitter.recoilAmplitude, -0.8f * Splitter.recoilAmplitude, -0.3f * Splitter.recoilAmplitude, 0.3f * Splitter.recoilAmplitude);
			if (base.isAuthority)
			{
				IsCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
				if (IsCrit)
				{
					Bulletcount = 6;
					tracerEffectPrefab = Deepend.pew;
				}
				else
				{
					Bulletcount = 3;
					tracerEffectPrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/tracers/tracerhuntresssnipe");
				}

					new BulletAttack
					{
						owner = base.gameObject,
						weapon = base.gameObject,
						origin = this.aimRay.origin,
						aimVector = this.aimRay.direction,
						minSpread = 3f,
						maxSpread = 3f,
						damage = Splitter.damageCoefficient * this.damageStat,
						force = Splitter.force,
						muzzleName = targetMuzzle,
						hitEffectPrefab = Splitter.hitEffectPrefab,
						radius = 0.2f,
						smartCollision = true,
						falloffModel = BulletAttack.FalloffModel.DefaultBullet,
						bulletCount = Bulletcount,
						isCrit = IsCrit,
						spreadPitchScale = 0f,
						spreadYawScale = 2f,
						tracerEffectPrefab = Splitter.tracerEffectPrefab,




					}.Fire();
				
			}
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x00116634 File Offset: 0x00114834
		public override void OnEnter()
		{ 
			base.OnEnter();
			this.duration = Splitter.baseDuration / this.attackSpeedStat;
			this.aimRay = base.GetAimRay();
			base.StartAimMode(this.aimRay, 3f, false);
			base.PlayCrossfade("Gesture, Override", "FireSeekingShot", "FireSeekingShot.playbackRate", this.duration, this.duration * 0.2f / this.attackSpeedStat);
			base.PlayCrossfade("Gesture, Additive", "FireSeekingShot", "FireSeekingShot.playbackRate", this.duration, this.duration * 0.2f / this.attackSpeedStat);
			this.FireBullet("Muzzle");
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x001166B8 File Offset: 0x001148B8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.characterBody.SetSpreadBloom(0, true);
			if (base.fixedAge < this.duration || !base.isAuthority)
			{
				return;
			}
			
			this.outer.SetNextStateToMain();
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x00013F7C File Offset: 0x0001217C
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}


		public static GameObject tracerEffectPrefab;
		// Token: 0x04003E6D RID: 15981
		public static GameObject muzzleEffectPrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/muzzleflashes/MuzzleflashHuntress");

		// Token: 0x04003E6E RID: 15982
		public static GameObject hitEffectPrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/omnieffect/OmniImpactVFXBrotherLunarShardExplosion");

		// Token: 0x04003E70 RID: 15984
		public static float damageCoefficient = 2f;

		// Token: 0x04003E71 RID: 15985
		public static float force = 650f;

		// Token: 0x04003E72 RID: 15986
		public static float baseDuration = 0.9f;

		// Token: 0x04003E73 RID: 15987
		public static string firePistolSoundString = "Play_huntress_m1_shoot";

		// Token: 0x04003E74 RID: 15988
		public static float recoilAmplitude = 6f;

		// Token: 0x04003E75 RID: 15989
		public static float spreadBloomValue = 0.5f;

		// Token: 0x04003E78 RID: 15992
		private Ray aimRay;

		// Token: 0x04003E79 RID: 15993
		private float duration;

		public uint Bulletcount;

		public bool IsCrit;


	}
}

