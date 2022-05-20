using System;
using System.Collections.Generic;
using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using EntityStates;

namespace Skillsfromthedeepend
{
	// Token: 0x02000BCD RID: 3021
	public class NoAim : BaseSkillState
	{
		// Token: 0x060044D8 RID: 17624 RVA: 0x001164D4 File Offset: 0x001146D4
		

		// Token: 0x060044D9 RID: 17625 RVA: 0x001164E0 File Offset: 0x001146E0
		private void FireBullet(string targetMuzzle)
		{
			Util.PlaySound(NoAim.firePistolSoundString, base.gameObject);
			if (NoAim.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(NoAim.muzzleEffectPrefab, base.gameObject, targetMuzzle, false);
			}
			base.AddRecoil(-0.4f * NoAim.recoilAmplitude, -0.8f * NoAim.recoilAmplitude, -0.3f * NoAim.recoilAmplitude, 0.3f * NoAim.recoilAmplitude);
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = this.aimRay.origin,
					aimVector = this.aimRay.direction,
					minSpread = 0f,
					maxSpread = base.characterBody.spreadBloomAngle,
					damage = NoAim.damageCoefficient * this.damageStat,
					force = NoAim.force,
					tracerEffectPrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/tracers/tracerhuntresssnipe"),
					muzzleName = targetMuzzle,
					hitEffectPrefab = NoAim.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					radius = 0.2f,
					smartCollision = true,
					falloffModel = BulletAttack.FalloffModel.None,
					stopperMask = LayerIndex.world.mask
					
			}.Fire();
			}
			base.characterBody.AddSpreadBloom(NoAim.spreadBloomValue);
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x00116634 File Offset: 0x00114834
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = NoAim.baseDuration / this.attackSpeedStat;
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


		public static GameObject hunter = Deepend.pew;
		// Token: 0x04003E6D RID: 15981
		public static GameObject muzzleEffectPrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/muzzleflashes/MuzzleflashHuntress");

		// Token: 0x04003E6E RID: 15982
		public static GameObject hitEffectPrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/omnieffect/OmniImpactVFXBrotherLunarShardExplosion");

		// Token: 0x04003E70 RID: 15984
		public static float damageCoefficient = 3.75f;

		// Token: 0x04003E71 RID: 15985
		public static float force = 650f;

		// Token: 0x04003E72 RID: 15986
		public static float baseDuration = 0.55f;

		// Token: 0x04003E73 RID: 15987
		public static string firePistolSoundString = "Play_huntress_m1_shoot";

		// Token: 0x04003E74 RID: 15988
		public static float recoilAmplitude = 50f;

		// Token: 0x04003E75 RID: 15989
		public static float spreadBloomValue = 0.5f;

		// Token: 0x04003E78 RID: 15992
		private Ray aimRay;

		// Token: 0x04003E79 RID: 15993
		private float duration;
	}
}

