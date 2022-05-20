using EntityStates;
using EntityStates.Commando.CommandoWeapon;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Skillsfromthedeepend

{
	// Token: 0x02000BCD RID: 3021
	public class Embeddeddets : BaseSkillState, SteppedSkillDef.IStepSetter
	{
		// Token: 0x060044D8 RID: 17624 RVA: 0x001164D4 File Offset: 0x001146D4
		void SteppedSkillDef.IStepSetter.SetStep(int i)
		{
			this.pistol = i;
		}
		// Token: 0x060044D8 RID: 17624 RVA: 0x001164D4 File Offset: 0x001146D4


		// Token: 0x060044D9 RID: 17625 RVA: 0x001164E0 File Offset: 0x001146E0
		private void FireBullet(string targetMuzzle)
		{
			Util.PlaySound(Embeddeddets.firePistolSoundString, base.gameObject);
			if (Embeddeddets.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(Embeddeddets.muzzleEffectPrefab, base.gameObject, targetMuzzle, false);
			}
			base.AddRecoil(-0.4f * Embeddeddets.recoilAmplitude, -0.8f * Embeddeddets.recoilAmplitude, -0.3f * Embeddeddets.recoilAmplitude, 0.3f * Embeddeddets.recoilAmplitude);
			if (base.isAuthority)
			{
				BulletAttack bulletAttack = new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = this.aimRay.origin,
					aimVector = this.aimRay.direction,
					minSpread = 0f,
					maxSpread = base.characterBody.spreadBloomAngle,
					damage = Embeddeddets.damageCoefficient * this.damageStat,
					force = Embeddeddets.force,
					tracerEffectPrefab = FirePistol2.tracerEffectPrefab,
					muzzleName = targetMuzzle,
					hitEffectPrefab = FirePistol2.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					radius = 0.1f,
					smartCollision = true


				};
				bulletAttack.hitCallback = delegate (BulletAttack bulletAttack, ref BulletAttack.BulletHit info)
				{
					
				
				    bool success = BulletAttack.defaultHitCallback(bulletAttack, ref info);
					if (base.isAuthority)
					{
						
						HealthComponent healthComponent = info.hitHurtBox ? info.hitHurtBox.healthComponent : null;
						if (healthComponent && healthComponent.alive && FriendlyFireManager.ShouldDirectHitProceed(healthComponent, base.GetTeam()))
						{
							
							EmbeddedDetsTracker tracker = base.GetComponent<EmbeddedDetsTracker>();
							if (tracker)
							{
								
								tracker.victims.Add(healthComponent);
							}
						}
					}
					return success;
				};
				bulletAttack.Fire();
			}
			base.characterBody.AddSpreadBloom(Embeddeddets.spreadBloomValue);
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x00116634 File Offset: 0x00114834
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Embeddeddets.baseDuration / this.attackSpeedStat;
			this.aimRay = base.GetAimRay();
			base.StartAimMode(this.aimRay, 3f, false);
			if (this.pistol % 2 == 0)
			{
				base.PlayAnimation("Gesture Additive, Left", "FirePistol, Left");
				this.FireBullet("MuzzleLeft");
				return;
			}
			base.PlayAnimation("Gesture Additive, Right", "FirePistol, Right");
			this.FireBullet("MuzzleRight");
			
		}

		
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge < this.duration || !base.isAuthority)
			{
				return;
			}
			if (base.activatorSkillSlot.stock < base.activatorSkillSlot.maxStock )
			{
				this.outer.SetNextState(new ReloadPistols());
				return;
			}
			this.outer.SetNextStateToMain();
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x00013F7C File Offset: 0x0001217C
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04003E6D RID: 15981
		public static GameObject muzzleEffectPrefab;

		// Token: 0x04003E6E RID: 15982
		public static GameObject hitEffectPrefab;

		// Token: 0x04003E6F RID: 15983
		public static GameObject tracerEffectPrefab;

		// Token: 0x04003E70 RID: 15984
		public static float damageCoefficient = 0.75f;

		// Token: 0x04003E71 RID: 15985
		public static float force;

		// Token: 0x04003E72 RID: 15986
		public static float baseDuration = 0.2f;

		// Token: 0x04003E73 RID: 15987
		public static string firePistolSoundString;

		// Token: 0x04003E74 RID: 15988
		public static float recoilAmplitude = 1f;

		// Token: 0x04003E75 RID: 15989
		public static float spreadBloomValue = 0.3f;

		// Token: 0x04003E76 RID: 15990
		public static float commandoBoostBuffCoefficient = 0.4f;

		// Token: 0x04003E77 RID: 15991
		private int pistol;

		// Token: 0x04003E78 RID: 15992
		private Ray aimRay;

		// Token: 0x04003E79 RID: 15993
		private float duration;
	}
}
