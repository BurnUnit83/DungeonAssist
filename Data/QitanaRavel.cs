using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buddy.Coroutines;
using ff14bot;
using ff14bot.Managers;
using ff14bot.Objects;
using LlamaLibrary.Helpers;
using Buddy.Coroutines;
using Clio.Utilities;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.Managers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using ff14bot.Helpers;
using System.Windows.Media;
using ff14bot.Navigation;

namespace DungeonAssist
{
    public class QitanaRavel
    {
        static PluginContainer sidestepPlugin = PluginHelpers.GetSideStepPlugin();

        static HashSet<uint> Spells = new HashSet<uint>()
        {
            // Lozatl 8231
            // Heat Up
            15502, 15501,
            // Lozatl's Scorn
            15499,
            // Lozatl's Fury
            15503, 15504,

            // Batsquatch 8232        
            // Soundwave
            15506,
            // 

            // Aenc Thon, Lord of the Lengthsome Gait 8146
            //  Corrosive Bile
            5174, 5175, 13547, 13548,
            // Toad Choir
            13551,
            // Imp Choir
            13552,
            // Finale
            13520, 13844, 15723
        };

        public static async Task<bool> Run()
        {
            // Lozatl 8231
            if (GameObjectManager.GetObjectByNPCId(8231) != null)
            {
                HashSet<uint> HeatUp = new HashSet<uint>() { 15502, 15501 };
                if (HeatUp.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> LozatlsScorn = new HashSet<uint>() { 15499 };
                if (LozatlsScorn.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> LozatlsFury = new HashSet<uint>() { 15503, 15504 };
                if (LozatlsFury.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
            }

            // Batsquatch 8232
            if (GameObjectManager.GetObjectByNPCId(8232) != null)
            {
                HashSet<uint> Soundwave = new HashSet<uint>() { 15506 };
                if (Soundwave.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }            
            }

            // Aenc Thon, Lord of the Lengthsome Gait 8146
            if (GameObjectManager.GetObjectByNPCId(8146) != null)
            {
                HashSet<uint> ImpChoir = new HashSet<uint>() {13552};
                if (ImpChoir.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }

                HashSet<uint> ToadChoir = new HashSet<uint>() {13551};
                if (ToadChoir.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }

                HashSet<uint> Finale = new HashSet<uint>() {13520, 13844, 15723};
                if (Finale.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }

                HashSet<uint> Corrosivebile = new HashSet<uint>() {13520, 13844, 15723};
                if (Corrosivebile.IsCasting())
                {
                    // sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }

                if (!Spells.IsCasting())
                {
                    if (!sidestepPlugin.Enabled)
                    {
                        sidestepPlugin.Enabled = true;
                    }
                }
            }


            await Coroutine.Yield();
            return false;
        }
    }
}