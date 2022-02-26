using BepInEx;
using EntityStates;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ExamplePlugin
{
    [BepInDependency(R2API.R2API.PluginGUID)]


    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]


    [R2APISubmoduleDependency(nameof(LanguageAPI), nameof(LoadoutAPI), nameof(PrefabAPI), nameof(EffectAPI), nameof(BuffAPI))]


    public class Deepend : BaseUnityPlugin
    {

        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Heyimnoob";
        public const string PluginName = "FuckersStoleMyAutoaim";
        public const string PluginVersion = "1.4.2";


        public static Sprite CreateSpriteBadWay(byte[] resourceBytes)
        {
            if (resourceBytes == null)
            {
                throw new ArgumentNullException("resourceBytes");
            }
            Texture2D texture2D = new Texture2D(128, 128, TextureFormat.RGBA32, false);
            texture2D.LoadImage(resourceBytes, false);
            Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f));
            return sprite;
        }
        private static void CharacterAltSkill(GenericSkill genericSkill, RoR2.Skills.SkillDef skillDef, string name, string description)
        {
            SkillFamily skillFamily = genericSkill.skillFamily;

            int length = skillFamily.variants.Length;

            string characterName = genericSkill.gameObject.name.ToUpper();
            string skillSlot = genericSkill.skillName.ToUpper();

            skillDef.skillName = characterName + "_" + skillSlot + "_DEEPENDALT" + length + "_NAME";
            skillDef.skillNameToken = characterName + "_" + skillSlot + "_DEEPENDALT" + length + "_NAME";
            skillDef.skillDescriptionToken = characterName + "_" + skillSlot + "_DEEPENDALT" + length + "_DESCRIPTION";

            LanguageAPI.Add(skillDef.skillNameToken, name);
            LanguageAPI.Add(skillDef.skillDescriptionToken, description);

            Array.Resize(ref skillFamily.variants, length + 1);
            skillFamily.variants[skillFamily.variants.Length - 1] = new SkillFamily.Variant
            {
                skillDef = skillDef,
                viewableNode = new ViewablesCatalog.Node(skillDef.skillNameToken, false, null)
            };

            LoadoutAPI.AddSkillDef(skillDef);
        }
        public void Fusillade()
        {
            GameObject character = Resources.Load<GameObject>("prefabs/characterbodies/HuntressBody");

            RoR2.Skills.SkillDef skillDef = ScriptableObject.CreateInstance<RoR2.Skills.SkillDef>();
            //Did you know that the critically acclaimed MMORPG Final Fantasy XIV has a free trial,
            //and includes the entirety of A Realm Reborn AND the award-winning Heavensward expansion up to level 60 with no restrictions on playtime?
            //Sign up, and enjoy Eorzea today!

            skillDef.activationState = new SerializableEntityStateType(typeof(NoAim));
            skillDef.interruptPriority = InterruptPriority.Any;
            skillDef.activationStateMachineName = ("Weapon");
            skillDef.baseRechargeInterval = 0;
            skillDef.cancelSprintingOnActivation = false;
            skillDef.canceledFromSprinting = false;
            skillDef.isCombatSkill = true;
            skillDef.keywordTokens = new string[] { "KEYWORD_AGILE" };
            skillDef.icon = CreateSpriteBadWay(Skillsfromthedeepend.Properties.Resources.Fusillade128_2);
            //Message #charybdis
            CharacterAltSkill(character.GetComponent<SkillLocator>().primary, skillDef, "Fusillade", "Shoot a modified arrow from your bow that does <style=cIsDamage>375% damage</style> and has <style=cDeath>NO AUTOAIM</style>. <style=cKeywordName>Pierces.</style>");

            LoadoutAPI.AddSkill(typeof(NoAim));
        }

        public void Untitled()
        {
            GameObject character = Resources.Load<GameObject>("prefabs/characterbodies/EngiBody");


            EngiSkillDef skillDef = ScriptableObject.CreateInstance<EngiSkillDef>();

            skillDef.activationState = new SerializableEntityStateType(typeof(EngiDisrupt));
            skillDef.interruptPriority = InterruptPriority.Any;
            skillDef.activationStateMachineName = ("Weapon");
            skillDef.baseRechargeInterval = 30;
            //change this later ^
            skillDef.cancelSprintingOnActivation = true;
            skillDef.canceledFromSprinting = false;
            skillDef.isCombatSkill = true;
            skillDef.icon = CreateSpriteBadWay(Skillsfromthedeepend.Properties.Resources.Greener);

            CharacterAltSkill(character.GetComponent<SkillLocator>().utility, skillDef, "Jury-rigged Turrets", "Weaponize your crappy turrets with this one simple trick! On activation, explodes all currently alive turrets for <style=cIsDamage>250% of their</style> <style=cDeath>Max HP</style> in a large radius. Refunds turrets on use, but has a long cooldown");

            LoadoutAPI.AddSkill(typeof(EngiDisrupt));


        }

        public void OOOOOYEAHBROTHERRRRR()
        {
            GameObject character = Resources.Load<GameObject>("prefabs/characterbodies/CommandoBody");


            SteppedSkillDef skillDef = ScriptableObject.CreateInstance<SteppedSkillDef>();
            skillDef.activationState = new SerializableEntityStateType(typeof(Embeddeddets));
            skillDef.interruptPriority = InterruptPriority.Any;
            skillDef.activationStateMachineName = ("Weapon");
            skillDef.canceledFromSprinting = true;
            skillDef.cancelSprintingOnActivation = true;
            skillDef.isCombatSkill = true;
            skillDef.baseRechargeInterval = 2;
            skillDef.baseMaxStock = 10;
            skillDef.stockToConsume = 1;
            skillDef.dontAllowPastMaxStocks = true;
            skillDef.requiredStock = 1;
            skillDef.rechargeStock = 10;
            skillDef.resetCooldownTimerOnUse = true;
            skillDef.resetStepsOnIdle = false;
            skillDef.stepCount = 2;
            skillDef.icon = CreateSpriteBadWay(Skillsfromthedeepend.Properties.Resources.EmDet);

            CharacterAltSkill(character.GetComponent<SkillLocator>().primary, skillDef, "Embedded Detonators", "Fire <style=cDeath>10 bullets</style>, which Embed into enemies and do <style=cIsDamage>75% damage</style> on impact, before having to reload, causing all bullets in enemies to explode for <style=cIsDamage>200% damage</style>.");


            LoadoutAPI.AddSkill(typeof(Embeddeddets));

            effect = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/effects/omnieffect/OmniExplosionVFX"), "CommandoExplode", false);
            {

                EffectComponent effectComponent = effect.GetComponent<EffectComponent>();
                effectComponent.soundName = "Play_captain_shift_impact";
                effectComponent.applyScale = true;

            }
            EffectAPI.AddEffect(effect);

            character.AddComponent<EmbeddedDetsTracker>();
            On.RoR2.GenericSkill.RestockSteplike += (orig, self) =>
            {
                {
                    int previousStock = self.stock;
                    orig(self);
                    if (self.stock > previousStock && self.skillDef == (SkillDef)skillDef && NetworkServer.active)
                    {

                        EmbeddedDetsTracker tracker = self.GetComponent<EmbeddedDetsTracker>();
                        if (tracker && self.characterBody && self.characterBody.master)
                        {

                            for (int i = 0; i < tracker.victims.Count; i++)
                            {

                                HealthComponent healthComponent = tracker.victims[i];
                                if (healthComponent)
                                {

                                    GameObject gameObject = healthComponent.gameObject;
                                    EffectManager.SpawnEffect(effect, new EffectData
                                    {
                                        origin = gameObject.transform.position,
                                        scale = 4f,


                                    }, true);
                                    BlastAttack blastAttack = new BlastAttack
                                    {
                                        radius = 5f,
                                        procCoefficient = 0.2f,
                                        position = tracker.victims[i].transform.position,
                                        attacker = self.gameObject,
                                        crit = Util.CheckRoll(self.characterBody.crit, self.characterBody.master),
                                        baseDamage = self.characterBody.damage * 2.0f,
                                        falloffModel = BlastAttack.FalloffModel.SweetSpot,
                                        damageType = DamageType.AOE,
                                        baseForce = 0
                                    };
                                    blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
                                    blastAttack.attackerFiltering = AttackerFiltering.NeverHit;
                                    blastAttack.Fire();
                                }
                            }
                            tracker.victims = new List<HealthComponent>();




                        }

                    }


                }


            };

        }

        public class EngiSkillDef : RoR2.Skills.SkillDef
        {       // Token: 0x06002A22 RID: 10786 RVA: 0x000AB92B File Offset: 0x000A9B2B
            public override bool CanExecute(GenericSkill skillSlot)
            {
                return this.isAvailable(skillSlot) && base.CanExecute(skillSlot);
            }

            // Token: 0x06002A23 RID: 10787 RVA: 0x000AB93E File Offset: 0x000A9B3E
            public override bool IsReady(GenericSkill skillSlot)
            {
                return this.isAvailable(skillSlot) && base.IsReady(skillSlot);
            }

            public bool isAvailable(GenericSkill skillSlot)

            {
                if (skillSlot.characterBody)
                {
                    CharacterMaster CharacterMaster = skillSlot.characterBody.master;

                    if (CharacterMaster)
                    {
                        if (CharacterMaster.GetDeployableCount(DeployableSlot.EngiTurret) > 0)
                        {
                            return true;

                        }


                    }
                }

                return false;

            }

        }

        public void Thej()
        {
            GameObject character = Resources.Load<GameObject>("prefabs/characterbodies/EngiBody");


            SkillDef skillDef = ScriptableObject.CreateInstance<SkillDef>();

            skillDef.activationState = new SerializableEntityStateType(typeof(Engiburst));
            skillDef.interruptPriority = InterruptPriority.Any;
            skillDef.activationStateMachineName = ("Weapon");
            skillDef.baseRechargeInterval = 10;
            //change this later ^
            skillDef.cancelSprintingOnActivation = true;
            skillDef.canceledFromSprinting = false;
            skillDef.isCombatSkill = true;
            skillDef.icon = CreateSpriteBadWay(Skillsfromthedeepend.Properties.Resources.image);

            CharacterAltSkill(character.GetComponent<SkillLocator>().utility, skillDef, "Unstable Turrets", "Trade the exploding blast of Jury rigged turrets for an extremely powerful blast that will send you flying. <style=cDeath>Has no damage</style>");

            LoadoutAPI.AddSkill(typeof(Engiburst));

        }

        public void Splitshot()
        {
            GameObject character = Resources.Load<GameObject>("prefabs/characterbodies/HuntressBody");

            SkillDef skillDef = ScriptableObject.CreateInstance<RoR2.Skills.SkillDef>();


            skillDef.activationState = new SerializableEntityStateType(typeof(Charge));
            skillDef.interruptPriority = InterruptPriority.Any;
            skillDef.activationStateMachineName = ("Weapon");
            skillDef.baseRechargeInterval = 0;
            skillDef.cancelSprintingOnActivation = false;
            skillDef.canceledFromSprinting = false;
            skillDef.isCombatSkill = true;
            skillDef.keywordTokens = new string[] { "KEYWORD_AGILE" };
            skillDef.icon = CreateSpriteBadWay(Skillsfromthedeepend.Properties.Resources.Fusillade128_2);
            //Message #charybdis
            CharacterAltSkill(character.GetComponent<SkillLocator>().primary, skillDef, "Splitter", "3 shot lmao");
            LoadoutAPI.AddSkill(typeof(Splitter));
            LoadoutAPI.AddSkill(typeof(Charge));
        }

        public void TacticsBuff()
        {
             Tactics = ScriptableObject.CreateInstance<BuffDef>();

            
            Tactics.isDebuff = false;
            Tactics.canStack = false;
            Tactics.iconSprite = Resources.Load<Sprite>("textures/bodyicons/lemurianbody");
            

            BuffAPI.Add(new CustomBuff(Tactics));

            GameObject character = Resources.Load<GameObject>("prefabs/characterbodies/CommandoBody");
            character.AddComponent(typeof(Assinpassive));
        
        
        }

        public void Engieeffect()
        {
            effect = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/effects/omnieffect/OmniExplosionVFXEngiTurretDeath"), "engiedetonate", false);
            {

                EffectComponent effectComponent = effect.GetComponent<EffectComponent>();
                effectComponent.applyScale = true;

            }
            EffectAPI.AddEffect(effect);

        }



        public static BuffDef Tactics;
        public static GameObject effect;
        public static GameObject pew;
        public void Awake()
        {
            Untitled();
            Fusillade();
            OOOOOYEAHBROTHERRRRR();
            Thej();
            Engieeffect();
            Splitshot();
            TacticsBuff();













        }
    }
}