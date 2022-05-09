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
    public class DohnMheg
    {
        static PluginContainer sidestepPlugin = PluginHelpers.GetSideStepPlugin();

        static HashSet<uint> Spells = new HashSet<uint>()
        {
            // Aenc Thon, Lord of the Lingering Gaze 8141
            // Candy Cane - Casted on tank, nothing to do
            8857,
            // Laughing Leap - SideStep handles
            8852, 8840,
            // Hydrofall
            8871, 8893,
            // Landsblood
            7822, 7899, 8800, 15598,

            // Griaule 8143        
            // 

            // Aenc Thon, Lord of the Lengthsome Gait 8146
            //  Corrosive Bile
            5174, 5175, 13547, 13548,
            // Toad Choir
            13551,
            // Imp Choir
            13552,
            // Finale
            13520, 13844, 15723,
            // Corrosive Bile
            13547, 13548
        };

        public static async Task<bool> Run()
        {
            IEnumerable<BattleCharacter> aencThon = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                r => !r.IsMe && r.Distance() < 26 && r.NpcId == 8141 && r.CurrentHealthPercent < 100);

            // Aenc Thon, Lord of the Lingering Gaze 8299
            if (aencThon.Any())
            {
                sidestepPlugin.Enabled = false;
                AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                await MovementHelpers.GetClosestDps.Follow();

                if (!aencThon.Any())
                {
                    if (!sidestepPlugin.Enabled)
                    {
                        sidestepPlugin.Enabled = true;
                    }
                }
            }

            // Griaule 8143
            /*
            if (GameObjectManager.GetObjectByNPCId(8143) != null)
            {
                IEnumerable<BattleCharacter> paintedSapling = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                    r => !r.IsMe && r.Distance() < 26 && r.NpcId == 8144);

                if (paintedSapling.Any())
                {
                    
                    BattleCharacter sapling = GameObjectManager.GetObjectsOfType<BattleCharacter>(true)
                        .Where(r =>
                            r.IsAlive &&
                            (r.NpcId == 8144 ))
                        .OrderBy(r => r.Distance())
                        .First();
                    
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    Navigator.PlayerMover.MoveTowards(sapling.Location);
                    await Coroutine.Sleep(50);
                }               
            }*/

            // Aenc Thon, Lord of the Lengthsome Gait 8146
            IEnumerable<BattleCharacter> aencThon2 = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                r => !r.IsMe && r.Distance() < 26 && r.NpcId == 8146 && r.CurrentHealthPercent < 100);
            
            if (GameObjectManager.GetObjectByNPCId(8146) != null)
            {
                HashSet<uint> ImpChoir = new HashSet<uint>() {13552};
                if (ImpChoir.IsCasting())
                {
                    
                    Vector3 location1 = new Vector3("-128.474,-144.5366,-243.2417");
                    while (location1.Distance2D(Core.Me.Location) > 1)
                    {
                        Navigator.PlayerMover.MoveTowards(location1);
                        await Coroutine.Sleep(100);
                    }

                    Navigator.PlayerMover.MoveStop();
                    await Coroutine.Sleep(3000);
                }

                HashSet<uint> ToadChoir = new HashSet<uint>() {13551};
                if (ToadChoir.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }

                
                
                HashSet<uint> Finale = new HashSet<uint>() {13520, 13844, 15723};
                if (Finale.IsCasting() && aencThon2.First().Location.Distance2D(Core.Me.Location) >= 10)
                {
                    if (Core.Me.IsCasting)
                    {
                        ActionManager.StopCasting();
                    }

                    await Coroutine.Sleep(3000);
                    Logging.Write(Colors.Aquamarine, $"Minding the gap");
                    
                    Vector3 location = new Vector3("-142.8355,-144.5264,-232.6624");
                    while (location.Distance2D(Core.Me.Location) > 0.2)
                    {
                        Navigator.PlayerMover.MoveTowards(location);
                        await Coroutine.Sleep(30);
                    }

                    location = new Vector3("-140.8284,-144.5366,-246.1443");
                    while (location.Distance2D(Core.Me.Location) > 0.2)
                    {
                        Navigator.PlayerMover.MoveTowards(location);
                        await Coroutine.Sleep(30);
                    }

                    location = new Vector3("-130.1889,-144.5366,-242.384");
                    while (location.Distance2D(Core.Me.Location) > 0.2)
                    {
                        Navigator.PlayerMover.MoveTowards(location);
                        await Coroutine.Sleep(30);
                    }

                    location = new Vector3("-114.455,-144.5366,-244.2632");
                    while (location.Distance2D(Core.Me.Location) > 0.2)
                    {
                        Navigator.PlayerMover.MoveTowards(location);
                        await Coroutine.Sleep(30);
                    }

                    location = new Vector3("-125.6857,-144.5238,-249.264");
                    while (location.Distance2D(Core.Me.Location) > 0.2)
                    {
                        Navigator.PlayerMover.MoveTowards(location);
                        await Coroutine.Sleep(30);
                    }

                    location = new Vector3("-122.5055,-144.5192,-258.3726");
                    while (location.Distance2D(Core.Me.Location) > 0.2)
                    {
                        Navigator.PlayerMover.MoveTowards(location);
                        await Coroutine.Sleep(30);
                    }

                    location = new Vector3("-128.1084,-144.5226,-258.0896");
                    while (location.Distance2D(Core.Me.Location) > 1)
                    {
                        Navigator.PlayerMover.MoveTowards(location);
                        await Coroutine.Sleep(100);
                    }
                }

                HashSet<uint> Corrosivebile = new HashSet<uint>() {13547, 13548};
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