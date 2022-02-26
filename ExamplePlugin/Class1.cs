using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using EntityStates;
using UnityEngine.Networking;


namespace ExamplePlugin
{
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		bool flag = base.fixedAge >= this.attackStart && !this.fired;
		if (flag)
		{
			this.fired = true;
			List<HurtBox> list = new List<HurtBox>();
			SphereSearch sphereSearch = new SphereSearch();
			sphereSearch.mask = LayerIndex.entityPrecise.mask;
			sphereSearch.radius = 40f;
			sphereSearch.ClearCandidates();
			sphereSearch.origin = base.characterBody.corePosition;
			sphereSearch.RefreshCandidates();
			sphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
			TeamMask enemyTeams = TeamMask.GetEnemyTeams(base.teamComponent.teamIndex);
			sphereSearch.FilterCandidatesByHurtBoxTeam(enemyTeams);
			sphereSearch.GetHurtBoxes(list);
			foreach (HurtBox hurtBox in list)
			{
				bool flag2 = hurtBox.healthComponent && hurtBox.healthComponent.body && hurtBox.healthComponent.body != base.characterBody;
				if (flag2)
				{
					this.DelayedDamage(hurtBox);
				}
			}
		}
		bool flag3 = base.fixedAge >= this.blinkDuration && this.invul;
		if (flag3)
		{
			bool flag4 = this.characterModel;
			if (flag4)
			{
				this.characterModel.invisibilityCount--;
			}
			bool flag5 = this.hurtboxGroup;
			if (flag5)
			{
				HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
				int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
				hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
			}

		}
		bool flag6 = base.fixedAge >= this.duration && base.isAuthority;
		if (flag6)
		{
			this.outer.SetNextStateToMain();
		}
	}
}