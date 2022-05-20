using System;
using System.Collections.Generic;
using System.Text;
using R2API.Utils;
using UnityEngine;
using RoR2;
using RoR2.ContentManagement;
using UnityEngine.Networking;
using System.Collections;
using RoR2.Skills;

namespace Skillsfromthedeepend
{
    class SFTDEContentProvider : IContentPackProvider

    {
        public string identifier => "SFTDE";

        public static void AddCustomContent(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(new SFTDEContentProvider());
        }

        public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }

        public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(contentPack, args.output);
            args.ReportProgress(1f);
            yield break;
        }

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }
        public static ContentPack contentPack = new ContentPack();

        public static void SFTDEContent()
        {
            ContentManager.collectContentPackProviders += AddCustomContent;

            contentPack.skillDefs.Add(new SkillDef[]{Deepend.FusilladeDef, Deepend.SalvoDef, Deepend.EngieExplode, Deepend.EngieJump, Deepend.DetsDef, Deepend.OUAGGG});
            
        }
    }
}