using BepInEx;
using EntityStates;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace Skillsfromthedeepend
{
    [BepInDependency(R2API.R2API.PluginGUID)]


    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]


    [R2APISubmoduleDependency(nameof(LanguageAPI), nameof(LoadoutAPI), nameof(PrefabAPI), nameof(DamageAPI))]


    public class Deepend : BaseUnityPlugin
    {

        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Heyimnoob";
        public const string PluginName = "SkillsFromTheDeepEnd";
        public const string PluginVersion = "1.9.1";


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


        }
        public void Fusillade()
        {
            GameObject character = LegacyResourcesAPI.Load<GameObject>("prefabs/characterbodies/HuntressBody");

            FusilladeDef = ScriptableObject.CreateInstance<RoR2.Skills.SkillDef>();
            //Did you know that the critically acclaimed MMORPG Final Fantasy XIV has a free trial,
            //and includes the entirety of A Realm Reborn AND the award-winning Heavensward expansion up to level 60 with no restrictions on playtime?
            //Sign up, and enjoy Eorzea today!

            FusilladeDef.activationState = new SerializableEntityStateType(typeof(NoAim));
            FusilladeDef.interruptPriority = InterruptPriority.Any;
            FusilladeDef.activationStateMachineName = ("Weapon");
            FusilladeDef.baseRechargeInterval = 0;
            FusilladeDef.cancelSprintingOnActivation = false;
            FusilladeDef.canceledFromSprinting = false;
            FusilladeDef.isCombatSkill = true;
            FusilladeDef.keywordTokens = new string[] { "KEYWORD_AGILE" };
            FusilladeDef.icon = this.assetBundle.LoadAsset<Sprite>("fusilladeIcon.png");
            //Message #charybdis
            CharacterAltSkill(character.GetComponent<SkillLocator>().primary, FusilladeDef, "Fusillade", "Shoot a modified arrow from your bow that does <style=cIsDamage>375% damage</style> and has <style=cDeath>NO AUTOAIM</style>. <style=cKeywordName>Pierces.</style>");


        }

        public void Untitled()
        {
            GameObject character = LegacyResourcesAPI.Load<GameObject>("prefabs/characterbodies/EngiBody");


            EngieExplode = ScriptableObject.CreateInstance<SkillDef>();

            EngieExplode.activationState = new SerializableEntityStateType(typeof(EngiDisrupt));
            EngieExplode.interruptPriority = InterruptPriority.Any;
            EngieExplode.activationStateMachineName = ("Weapon");
            EngieExplode.baseRechargeInterval = 30;
            //change this later ^
            EngieExplode.cancelSprintingOnActivation = true;
            EngieExplode.canceledFromSprinting = false;
            EngieExplode.isCombatSkill = true;
            EngieExplode.icon = this.assetBundle.LoadAsset<Sprite>("juryIcon.png");

            CharacterAltSkill(character.GetComponent<SkillLocator>().utility, EngieExplode, "Jury-rigged Turrets", "Weaponize your crappy turrets with this one simple trick! On activation, explodes all currently alive turrets for <style=cIsDamage>250% of their</style> <style=cDeath>Max HP</style> in a large radius. Refunds turrets on use, but has a long cooldown");



        }

        public void OOOOOYEAHBROTHERRRRR()
        {
            GameObject character = LegacyResourcesAPI.Load<GameObject>("prefabs/characterbodies/CommandoBody");


            DetsDef = ScriptableObject.CreateInstance<SteppedSkillDef>();
            DetsDef.activationState = new SerializableEntityStateType(typeof(Embeddeddets));
            DetsDef.interruptPriority = InterruptPriority.Skill;
            DetsDef.activationStateMachineName = ("Weapon");
            DetsDef.canceledFromSprinting = true;
            DetsDef.cancelSprintingOnActivation = true;
            DetsDef.isCombatSkill = true;
            DetsDef.baseRechargeInterval = 2;
            DetsDef.baseMaxStock = 10;
            DetsDef.stockToConsume = 1;
            DetsDef.dontAllowPastMaxStocks = true;
            DetsDef.requiredStock = 1;
            DetsDef.rechargeStock = 10;
            DetsDef.resetCooldownTimerOnUse = true;
            DetsDef.stepCount = 2;
            DetsDef.icon = this.assetBundle.LoadAsset<Sprite>("detsIcon.png");

            CharacterAltSkill(character.GetComponent<SkillLocator>().primary, DetsDef, "Embedded Detonators", "Fire <style=cDeath>10 bullets</style>, which Embed into enemies and do <style=cIsDamage>75% damage</style> on impact, before having to reload, causing all bullets in enemies to explode for <style=cIsDamage>200% damage</style>.");




            Dets = PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("prefabs/effects/omnieffect/OmniExplosionVFX"), "CommandoExplode", false);
            {

                EffectComponent effectComponent = Dets.GetComponent<EffectComponent>();
                effectComponent.soundName = "Play_captain_shift_impact";
                effectComponent.applyScale = true;

            }
            R2API.ContentAddition.AddEffect(Dets);

            character.AddComponent<EmbeddedDetsTracker>();
            On.RoR2.GenericSkill.RestockSteplike += (orig, self) =>
            {
                {
                    int previousStock = self.stock;
                    orig(self);
                    if (self.stock > previousStock && self.skillDef == (SkillDef)DetsDef && NetworkServer.active)
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
                                    EffectManager.SpawnEffect(Dets, new EffectData
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
                                    blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
                                    blastAttack.Fire();
                                }
                            }
                            tracker.victims = new List<HealthComponent>();




                        }

                    }


                }


            };

        }



        public void Thej()
        {
            GameObject character = LegacyResourcesAPI.Load<GameObject>("prefabs/characterbodies/EngiBody");


            EngieJump = ScriptableObject.CreateInstance<SkillDef>();

            EngieJump.activationState = new SerializableEntityStateType(typeof(Engiburst));
            EngieJump.interruptPriority = InterruptPriority.Any;
            EngieJump.activationStateMachineName = ("Weapon");
            EngieJump.baseRechargeInterval = 10;
            //change this later ^
            EngieJump.cancelSprintingOnActivation = true;
            EngieJump.canceledFromSprinting = false;
            EngieJump.isCombatSkill = true;
            EngieJump.icon = this.assetBundle.LoadAsset<Sprite>("unstableIcon.png");

            CharacterAltSkill(character.GetComponent<SkillLocator>().utility, EngieJump, "Unstable Turrets", "Trade the exploding blast of Jury rigged turrets for an extremely powerful blast that will send you flying. <style=cDeath>Has no damage</style>");



        }

        public void Splitshot()
        {
            GameObject character = LegacyResourcesAPI.Load<GameObject>("prefabs/characterbodies/HuntressBody");

            SalvoDef = ScriptableObject.CreateInstance<RoR2.Skills.SkillDef>();


            SalvoDef.activationState = new SerializableEntityStateType(typeof(Splitter));
            SalvoDef.interruptPriority = InterruptPriority.Any;
            SalvoDef.activationStateMachineName = ("Weapon");
            SalvoDef.baseRechargeInterval = 0;
            SalvoDef.cancelSprintingOnActivation = false;
            SalvoDef.canceledFromSprinting = false;
            SalvoDef.isCombatSkill = true;
            SalvoDef.keywordTokens = new string[] { "KEYWORD_AGILE" };
            SalvoDef.icon = this.assetBundle.LoadAsset<Sprite>("salvoIcon.png");
            //Message #charybdis
            CharacterAltSkill(character.GetComponent<SkillLocator>().primary, SalvoDef, "Salvo", "Shoot a volley of 3 close range arrows with high, uncontrollable spread for <style=cIsDamage>200%x3 damage</style>. Critical strikes cause the arrows to split in two");


        }

        public void Death()
        {
            Agony = ScriptableObject.CreateInstance<BuffDef>();

            Agony.isDebuff = true;
            Agony.canStack = false;
            Agony.iconSprite = this.assetBundle.LoadAsset<Sprite>("agonyBuff");
            Agony.buffColor = new Color32(147, 112, 219, 225);

            R2API.ContentAddition.AddBuffDef(Agony);

            AcridComponent = base.GetComponent<CrocoDamageTypeController>();


            On.RoR2.GlobalEventManager.OnHitEnemy += GlobalEventManager_OnHitEnemy;

            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            GameObject character = LegacyResourcesAPI.Load<GameObject>("prefabs/characterbodies/crocobody");

            OUAGGG = ScriptableObject.CreateInstance<SkillDef>();

            OUAGGG.activationState = new SerializableEntityStateType(typeof(Misery));
            OUAGGG.interruptPriority = InterruptPriority.Skill;
            OUAGGG.activationStateMachineName = ("Weapon");
            OUAGGG.baseRechargeInterval = 10;
            OUAGGG.cancelSprintingOnActivation = false;
            OUAGGG.canceledFromSprinting = true;
            OUAGGG.isCombatSkill = true;
            OUAGGG.icon = this.assetBundle.LoadAsset<Sprite>("agonyIcon.png");
            OUAGGG.keywordTokens = new string[] { "KEYWORD_POISON", "KEYWORD_TORMENTED", "KEYWORD_RAPID_REGEN" };

            CharacterAltSkill(character.GetComponent<SkillLocator>().special, OUAGGG, "Scourge", "<style=cIsHealing>Poisonous.</style> <style=cDeath>Tormenting.</style> Release a large explosion of bile from yourself, doing <style=cIsDamage>200% damage</style>.");

        }


        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            if (!NetworkServer.active)
            {
                return;
            }

            orig(self, damageReport);
            CharacterBody characterBody = damageReport.victimBody;
            CharacterBody attackerBody = damageReport.attackerBody;
            float Radius = 15f;
            Vector3 CorePosition = characterBody.corePosition;
            if (NetworkServer.active)
            {
                if (characterBody)
                {
                   
                    if (characterBody.HasBuff(Agony))
                    {
                        Util.PlaySound("Play_acrid_shift_land", base.gameObject);



                        EffectManager.SpawnEffect(Misery.Effect, new EffectData
                        {
                            scale = Radius,
                            origin = CorePosition

                        }, true);
                        BlastAttack blastAttack = new BlastAttack
                        {
                            radius = Radius,
                            position = characterBody.transform.position,
                            baseDamage = attackerBody.damage * 1.5f,
                            procCoefficient = 0.5f,
                            attacker = attackerBody.gameObject,
                            crit = Util.CheckRoll(attackerBody.crit, attackerBody.master),
                            falloffModel = BlastAttack.FalloffModel.None,
                            baseForce = 500f,
                            attackerFiltering = AttackerFiltering.NeverHitSelf,
                            damageColorIndex = DamageColorIndex.WeakPoint


                        };
                        DamageAPI.AddModdedDamageType(blastAttack, Deepend.acridBuffHandler);
                        blastAttack.Fire();

                        attackerBody.AddTimedBuff(RoR2Content.Buffs.CrocoRegen, 0.5f);
                    }

                }
            }
        }
        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            

            orig(self, damageInfo, victim);
            if (damageInfo.procCoefficient == 0f || damageInfo.rejected)
            {
                return;
            }
            if (!NetworkServer.active)
            {
                return;
            }
            if (DamageAPI.HasModdedDamageType(damageInfo, acridBuffHandler))
            {
                CharacterBody victimBody = victim ?
                victim.GetComponent<CharacterBody>() : null;

                if(victimBody)
                {
                    victimBody.AddTimedBuff(Agony, 20f);
                }

            }
        }

        public void LanguageHandler()
        {
            LanguageAPI.Add("KEYWORD_TORMENTED", "<style=cKeywordName>Tormented</style> <style=cSub>Mark an enemy, causing them to explode for <style=cIsDamage>150% damage</style> in a 15 meter radius on death. Also applies <style=cIsHealing>Regenerative</style></style>");
          
        }
        public void Huntresscriteffect()
        {
            pew = PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("prefabs/effects/tracers/tracerhuntresssnipe"), "huntercrit", false);
            { 
                GameObject beamObject = pew.transform.Find("BeamObject").gameObject;
                GameObject critOrb = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/orbeffects/FlurryArrowCritOrbEffect");

                Material matCritTrail = critOrb.transform.Find("TrailParent/Trail").GetComponent<TrailRenderer>().material;
                Material[] newMaterials = new Material[] {null, matCritTrail};
                beamObject.GetComponent<ParticleSystemRenderer>().SetSharedMaterials(newMaterials, 2);
            }
            R2API.ContentAddition.AddEffect(pew);

        }

        public void Engieeffect()
        {
            effect = PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("prefabs/effects/omnieffect/OmniExplosionVFXEngiTurretDeath"), "engiedetonate", false);
            {

                EffectComponent effectComponent = effect.GetComponent<EffectComponent>();
                effectComponent.applyScale = true;

            }
            R2API.ContentAddition.AddEffect(effect);

        }

        public void LoadAssets()
        {
            this.assetBundle = AssetBundle.LoadFromFile(Assembly.GetExecutingAssembly().Location.Replace("Skillsfromthedeepend.dll", "sftde"));
        }
        
        public static DamageAPI.ModdedDamageType acridBuffHandler = DamageAPI.ReserveDamageType();
        public static BuffDef Agony;
        public static BuffDef Tactics;
        public static GameObject effect;
        public static GameObject pew;
        public static GameObject Dets;
        public CrocoDamageTypeController AcridComponent;
        public DamageType MiseryC;
        public static SteppedSkillDef DetsDef;
        public static SkillDef FusilladeDef;
        public static SkillDef SalvoDef;
        public static SkillDef EngieExplode;
        public static SkillDef EngieJump;
        public static SkillDef OUAGGG;
        public static SkillDef Powermdoe;
        public static SkillDef Dill;
        public static SkillDef Insane;

        public AssetBundle assetBundle;
        public GameObject Effect = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/impacteffects/CrocoLeapExplosion");
        public void Awake()
        {

            LoadAssets();
            Untitled();
            Fusillade();
            OOOOOYEAHBROTHERRRRR();
            Thej();
            Engieeffect();
            Splitshot();
            Huntresscriteffect();
            Death();
            LanguageHandler();
           










        }
    }
}