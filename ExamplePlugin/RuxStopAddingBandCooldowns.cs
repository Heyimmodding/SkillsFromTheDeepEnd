using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using UnityEngine;


namespace ExamplePlugin
{
    class Assinpassive : MonoBehaviour, IOnDamageDealtServerReceiver, IOnTakeDamageServerReceiver, IOnIncomingDamageServerReceiver
    {   

        public void Start()
        {
            cb = base.GetComponent<CharacterBody>();
            

        }
        public void OnDamageDealtServer(DamageReport damageReport)
        {   if (cb && !cb.HasBuff(Deepend.Tactics))
            {   
                DamageDealt += damageReport.damageDealt / cb.damage;
                Debug.Log(DamageDealt);
                if (DamageDealt >= 20f)
                {
                    cb.AddBuff(Deepend.Tactics);
                    DamageDealt = 0;
                }
            }   
           
     
        }
        public void OnTakeDamageServer(DamageReport damageReport)
        {
            DamageDealt = 0;
        }
        public void OnIncomingDamageServer(DamageInfo damageInfo)
        {
            if (cb && cb.HasBuff(Deepend.Tactics))
            {
                damageInfo.rejected = true;
                Util.PlaySound("Play_bandit2_shift_end", base.gameObject);
                EffectManager.SpawnEffect(Resources.Load<GameObject>("Prefabs/Effects/ProcStealthkit"), new EffectData
                {
                    origin = base.transform.position,
                    rotation = Quaternion.identity
                }, true);
                cb.AddTimedBuff(RoR2Content.Buffs.Cloak, 5f);
                cb.AddTimedBuff(RoR2Content.Buffs.CloakSpeed, 5f);
                cb.RemoveBuff(Deepend.Tactics);

            }


        }

        public CharacterBody cb;
        public float DamageDealt;
       
    }

}
