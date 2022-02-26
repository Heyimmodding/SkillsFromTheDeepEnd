using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace ExamplePlugin
{
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		bool flag = NetworkServer.active && base.fixedAge < this.invulEnd;
		if (flag)
		{
			bool flag2 = !base.characterBody.HasBuff(RoR2Content.Buffs.HiddenInvincibility);
			if (flag2)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
		}
		bool flag3 = base.fixedAge >= this.invulEnd && this.invul;
		if (flag3)
		{
			bool flag4 = NetworkServer.active && base.characterBody.HasBuff(RoR2Content.Buffs.HiddenInvincibility);
			if (flag4)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
			bool flag5 = this.characterModel;
			if (flag5)
			{
				this.characterModel.invisibilityCount--;
			}
		
		
			this.invul = false;
			CapsuleCollider capsuleCollider = (CapsuleCollider)base.characterBody.mainHurtBox.collider;
			capsuleCollider.height = 1.5f;
			capsuleCollider.radius = 0.2f;
			bool flag6 = this.damageCounter > 0f;
			if (flag6)
			{
				bool isAuthority2 = base.isAuthority;
				if (isAuthority2)
				{
					this.outer.SetNextState(new EGOBlockCounter
					{
						damageCounter = this.damageCounter,
						bonusMult = this.bonusMult
					});
				}
			}
			else
			{
				base.cameraTargetParams.cameraParams = CameraParams.defaultCameraParamsRedMist;
				base.cameraTargetParams.aimMode = CameraTargetParams.AimType.Standard;
			}
		}
		bool flag7 = this.damageCounter > 0f && !base.inputBank.skill3.down && base.isAuthority;
		if (flag7)
		{
			this.outer.SetNextState(new EGOBlockCounter
			{
				damageCounter = this.damageCounter,
				bonusMult = this.bonusMult
			});
		}
		bool flag8 = base.fixedAge >= this.duration && base.isAuthority;
		if (flag8)
		{
			this.outer.SetNextStateToMain();
		}
	}
}
