using System;
using System.Collections.Generic;
using System.Text;
using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using EntityStates;
using UnityEngine.Networking;
using EntityStates.QuestVolatileBattery;

namespace ExamplePlugin
{
	// Token: 0x02000BBB RID: 3003
	public class EngiDisrupt : BaseState
	{
		// Token: 0x06004470 RID: 17520 RVA: 0x00113E24 File Offset: 0x00112024
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = EngiDisrupt.baseDuration / this.attackSpeedStat;
			CharacterBody characterBody = base.characterBody;
			if (characterBody && base.isAuthority)
			{ 
				CharacterMaster characterMaster = characterBody.master;
				if (characterMaster)
				{
					for (int i = 0; i < characterMaster.deployablesList.Count; i++)
					{
						DeployableInfo deployableInfo = characterMaster.deployablesList[i];
						if (deployableInfo.slot == DeployableSlot.EngiTurret)
						{
							CharacterMaster turretMaster = deployableInfo.deployable.gameObject.GetComponent<CharacterMaster>();
							if (turretMaster)
							{
			
								if (turretMaster.GetBody())
                                {
									Vector3 corePosition = turretMaster.GetBody().corePosition;

									EffectManager.SpawnEffect(explode, new EffectData
									{
										origin = corePosition,
										scale = CountDown.explosionRadius
									}, true);
									new BlastAttack
									{

										position = corePosition + UnityEngine.Random.onUnitSphere,
										radius = CountDown.explosionRadius,
										falloffModel = BlastAttack.FalloffModel.None,
										attacker = base.gameObject,
										baseDamage = turretMaster.GetBody().healthComponent.fullCombinedHealth * damageCoefficient,
										baseForce = 5000f,
										bonusForce = Vector3.zero,
										attackerFiltering = AttackerFiltering.NeverHit,
										crit = base.RollCrit(),
										procChainMask = default(ProcChainMask),
										procCoefficient = 1.0f,
										teamIndex = base.teamComponent.teamIndex
									}.Fire();
								}

								turretMaster.TrueKill();
								if (base.skillLocator.special)
                                {

									base.skillLocator.special.ApplyAmmoPack();

                                }
							
							}
						}
					}
				}
			}

		}

		// Token: 0x06004471 RID: 17521 RVA: 0x00034313 File Offset: 0x00032513
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x00113FCD File Offset: 0x001121CD
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x00013F7C File Offset: 0x0001217C
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		public static GameObject explode = Deepend.effect;
		// Token: 0x04003DC6 RID: 15814
		public GameObject effectPrefab;

		// Token: 0x04003DC7 RID: 15815
		public static float baseDuration = 2f;

		// Token: 0x04003DCD RID: 15821
		private float duration;

		// Token: 0x04003E70 RID: 15984
		public static float damageCoefficient = 2.5f;
	}
}
