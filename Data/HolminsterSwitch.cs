using System;
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
    public class HolminsterSwitch
    {
        static PluginContainer sidestepPlugin = PluginHelpers.GetSideStepPlugin();
        
        private static DateTime fierceBeatingTimestamp = DateTime.MinValue;
        private static readonly int FierceBeatingDuration = 32_000;
        
        private static readonly Vector3 ExorciseStackLoc = new Vector3("79.35034, 0, -81.01664");
        private static readonly int ExorciseDuration = 25_000;

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
                    if (Core.Me.Distance(ExorciseStackLoc) > 1f && Core.Me.IsCasting)
                    {
                        ActionManager.StopCasting();
                    }

                    while (Core.Me.Distance(ExorciseStackLoc) > 1f)
                    {
                        await CommonTasks.MoveTo(ExorciseStackLoc);
                        await Coroutine.Yield();
                    }

                    // Wait in-place for stack marker to go off
                    Navigator.PlayerMover.MoveStop();
                    await Coroutine.Sleep(5000);

                    Stopwatch exorciseTimer = new Stopwatch();
                    exorciseTimer.Restart();

                    // Create an AOE avoid for the ice puddle where the stack marker went off
                    AvoidanceManager.AddAvoidLocation(
                        () => exorciseTimer.IsRunning && exorciseTimer.ElapsedMilliseconds < ExorciseDuration,
                        radius: 6.5f * 1.5f, // Expand to account for stack target maybe standing to the side
                        () => ExorciseStackLoc);

                    // Non-targetable but technically .IsVisible copies of Tesleen with the same .NpcId are used to place the outer ice puddles
                    // Create AOE avoids on top of them since SideStep doesn't do this automatically
                    IEnumerable<GameObject> fakeTesleens = GameObjectManager.GetObjectsByNPCId(8300).Where(obj => !obj.IsTargetable);
                    foreach (GameObject fake in fakeTesleens)
                    {
                        Vector3 location = fake.Location;

                        ff14bot.Pathing.Avoidance.AvoidInfo a = AvoidanceManager.AddAvoidLocation(
                            () => exorciseTimer.IsRunning && exorciseTimer.ElapsedMilliseconds < ExorciseDuration,
                            radius: 6.5f,
                            () => location);
                    }
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
                GameObject philia = GameObjectManager.GetObjectsByNPCId(8301).FirstOrDefault(obj => obj.IsTargetable);

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
                    await MovementHelpers.Spread(10000, 9);
                }
                
                HashSet<uint> FierceBeating = new HashSet<uint>() { 15834, 15835, 15836, 15837, 15838, 15839 };
                if (FierceBeating.IsCasting() && fierceBeatingTimestamp.AddMilliseconds(FierceBeatingDuration) < DateTime.Now)
                {
                    Vector3 location = philia.Location;
                    uint objectId = philia.ObjectId;

                    fierceBeatingTimestamp = DateTime.Now;
                    Stopwatch fierceBeatingTimer = new Stopwatch();
                    fierceBeatingTimer.Restart();

                    // Create an AOE avoid for the orange swirly under the boss
                    AvoidanceManager.AddAvoidObject<GameObject>(
                        canRun: () => fierceBeatingTimer.IsRunning && fierceBeatingTimer.ElapsedMilliseconds < FierceBeatingDuration,
                        radius: 11f,
                        unitIds: objectId);

                    // Attach very wide cone avoid pointing out the boss's right, forcing bot to left side
                    // Boss spins clockwise and front cleave comes quickly, so disallow less-safe right side
                    // Position + rotation will auto-update as the boss moves + turns!
                    AvoidanceManager.AddAvoidUnitCone<GameObject>(
                        canRun: () => fierceBeatingTimer.IsRunning && fierceBeatingTimer.ElapsedMilliseconds < FierceBeatingDuration,
                        objectSelector: (obj) => obj.ObjectId == objectId,
                        leashPointProducer: () => location,
                        leashRadius: 40f,
                        rotationDegrees: -90f,
                        radius: 25f,
                        arcDegrees: 345f);
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