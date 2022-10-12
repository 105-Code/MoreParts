﻿
using Cysharp.Threading.Tasks;
using HarmonyLib;
using MorePartsMod.Parts;
using SFS;
using SFS.Parts;
using System;
using UnityEngine;

namespace MorePartsMod.Patches
{
    [HarmonyPatch(typeof(CustomAssetsLoader), "LoadAllCustomAssets")]
    class CustomAssetsLoaderPatcher
    {
        [HarmonyPostfix]
        public static void Postfix(ref UniTask __result)
        {
            __result.GetAwaiter().OnCompleted(AddModules);
        }

        private static void AddModules()
        {
            // add components
            Debug.Log("Adding moreparts components");
            MorePartsInjector injector;
            foreach ( Part part in Base.partsLoader.parts.Values)
            {
                injector = part.GetComponent<MorePartsInjector>();
                if (injector)
                {
                    
                    foreach (Modules module in injector.modules)
                    {
                        Debug.Log(part.name + " adding "+module.ToString());
                        if (module == Modules.BallonModule)
                        {
                            part.GetOrAddComponent<BalloonModule>();
                            continue;
                        }

                        if (module == Modules.TelecomunicationModule)
                        {
                            part.GetOrAddComponent<TelecommunicationDishModule>();
                            continue;
                        }

                        if (module == Modules.RotorModule)
                        {
                            part.GetOrAddComponent<RotorModule>();
                            continue;
                        }

                        if (module == Modules.ContinuosTrackModule)
                        {
                            part.GetOrAddComponent<ContinuousTrackModule>();
                            continue;
                        }

                    }
                }

            }
            
        }

  
    }
}
