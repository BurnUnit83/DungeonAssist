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
            13520, 13844, 15723
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