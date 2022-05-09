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

namespace DungeonAssist
{
    public class HolminsterSwitch
    {
        static PluginContainer sidestepPlugin = PluginHelpers.GetSideStepPlugin();

        static HashSet<uint> Spells = new HashSet<uint>()
        {
            // Forgiven Dissonance 8299
                //Thumbscrew
                    15814, 16850,
                // Brazen Bull    
                    15817, 15820,
            // Tesleen, the Forgiven 8300
                // Exorcise - Stack
                    15826, 15827,
                // Fevered Flagellation - Spread
                    15829, 15830, 17440,
            // Philia 8301
                // Right Knout
                    15846,
                // Left Knout    
                    15847,
                // Taphephobia - Spread
                    15842, 16769,
                // Into the Light   
                    15845, 17232,
                // Pendulum
                    15833, 16777, 16790,
                // Fierce Beating
                    15834,15835,15836,15837,15838,15839
        };

        public static async Task<bool> Run()
        {
            // Forgiven Dissonance 8299
            if (GameObjectManager.GetObjectByNPCId(8299) != null)
            {
                HashSet<uint> Thumbscrew = new HashSet<uint>() { 15814, 16850 };
                if (Thumbscrew.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> BrazenBull = new HashSet<uint>() { 15817, 15820 };
                if (BrazenBull.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
            }
            
            // Tesleen, the Forgiven 8300
            if (GameObjectManager.GetObjectByNPCId(8300) != null)
            {
                HashSet<uint> Exorcise = new HashSet<uint>() { 15826, 15827 };
                if (Exorcise.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> FeveredFlagellation = new HashSet<uint>() { 15829, 15830, 17440 };
                if (FeveredFlagellation.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.Spread(10000, 10);
                }

                if (Core.Me.HasAura(320))
                {
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
            } 
            
            // Philia 8301
            if (GameObjectManager.GetObjectByNPCId(8301) != null)
            {
                HashSet<uint> RightKnout = new HashSet<uint>() { 15846 };
                if (RightKnout.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> LeftKnout = new HashSet<uint>() { 15847 };
                if (LeftKnout.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> IntotheLight = new HashSet<uint>() { 15847, 17232 };
                if (IntotheLight.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> Pendulum = new HashSet<uint>() { 15833, 16777, 16790 };
                if (Pendulum.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> Taphephobia = new HashSet<uint>() { 15842, 16769 };
                if (Taphephobia.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.Spread(10000, 10);
                }
                
                HashSet<uint> FierceBeating = new HashSet<uint>() { 15834,15835,15836,15837,15838,15839 };
                if (FierceBeating.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
            } 

            if (!Spells.IsCasting())
            {
                if (!sidestepPlugin.Enabled)
                {
                    sidestepPlugin.Enabled = true;
                }
            }

            await Coroutine.Yield();
            return false;
        }
    }
}