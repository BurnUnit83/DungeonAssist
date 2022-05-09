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
                    
                    sidestepPlugin.Enabled = false;
                    
                    
                        BattleCharacter obj = GameObjectManager.GetObjectsOfType<BattleCharacter>(true)
                            .Where(r =>
                              r.IsAlive &&
                              (r.NpcId == 729 || r.NpcId == 8378 || // "雅·修特拉"
                              r.NpcId == 1492 || // "于里昂热"
                              r.NpcId == 4130 || // "阿尔菲诺"
                              r.NpcId == 5239 || // "阿莉塞"
                              r.NpcId == 8889 || // 琳
                              r.NpcId == 11264 || // Alphinaud's avatar
                              r.NpcId == 11265 || // Alisaie's avatar
                              r.NpcId == 11267 || // Urianger's avatar
                              r.NpcId == 11268 || // Y'shtola's avatar
                              r.NpcId == 11269 || // Ryne's avatar
                              r.NpcId == 11270 || // Estinien's avatar
                              r.Name == "阿莉塞" ||
                              r.Name == "琳" ||
                              r.Name == "水晶公" ||
                              r.Name == "敏菲利亚" ||
                              r.Name == "桑克瑞德"))
                            .OrderBy(r => r.Distance())
                            .First();

                        // 当距离大于跟随距离 再处理跟随
                        if (obj.Location.Distance2D(Core.Me.Location) >= 0.2)
                        {
                            // 读条中断
                            if (Core.Me.IsCasting)
                            {
                                ActionManager.StopCasting();
                            }

                            // 选中跟随最近的队友
                            obj.Target();

                            Logging.Write(Colors.Aquamarine, $"Following {obj.Name} Distance:{obj.Location.Distance2D(Core.Me.Location)}");

                            while (obj.Location.Distance2D(Core.Me.Location) >= 0.2)
                            {
                                Navigator.PlayerMover.MoveTowards(obj.Location);
                                await Coroutine.Sleep(50);
                            }

                            Navigator.PlayerMover.MoveStop();
                            await Coroutine.Sleep(50);
                            return true;
                        }
                    
                    /*
                    Vector3[] navPoints =
                    {
                        new Vector3(-128.5326f, -144.5212f, -228.8046f),
                    };

                    foreach (var pos in navPoints)
                    {
                        while (Core.Me.Location.Distance2D(pos) > 1.5f)
                        {
                            await Coroutine.Yield();
                            Navigator.PlayerMover.MoveTowards(pos);
                        }
                    }
                    Navigator.PlayerMover.MoveStop();
                    await Coroutine.Wait(20000, () => !Finale.IsCasting());*/
                }

                HashSet<uint> Corrosivebile = new HashSet<uint>() { 13547, 13548 };
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