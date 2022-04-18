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
    public class TheStoneVigil
    {
        static PluginContainer sidestepPlugin = PluginHelpers.GetSideStepPlugin();

        static HashSet<uint> Spells = new HashSet<uint>()
        {
            // Koshchei 1678
            // Mealstrom
            903, 4367, 8906, 3455, 2727, 2295, 11725,

            // Isgebind 1680
            // Cauterize
            1026, 2047, 2048, 2049, 2342, 2597, 2598, 2599, 4260, 4648, 5770, 6039, 6241, 9932, 9931, 9933,
            9934, 9935, 12617, 12616, 12730, 12864, 12865, 19764, 20231, 20261, 24674, 26904, 26905
        };

        public static async Task<bool> Run()
        {
            // Chudo-Yudo  1677
            if (GameObjectManager.GetObjectByNPCId(1677) != null)
            {
                HashSet<uint> Swinge = new HashSet<uint>() {903, 4367, 8906, 3455, 2727, 2295, 11725};
                if (Swinge.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
            }

            /* Koshchei 1678
            if (GameObjectManager.GetObjectByNPCId(1678) != null &&
                (WorldManager.ZoneId == 1042 && WorldManager.SubZoneId == 878))
            {
                AvoidanceManager.RemoveAllAvoids(i=> i.CanRun);
                var ids = GameObjectManager.GetObjectsByNPCId(9910).Select(i => i.ObjectId).ToArray();
                AvoidanceManager.AddAvoidObject<GameObject>(()=> true, 3f, ids);
            }*/

            // Isgebind 1680
            if (GameObjectManager.GetObjectByNPCId(1680) != null)
            {
                HashSet<uint> Cauterize = new HashSet<uint>()
                {
                    1026, 2047, 2048, 2049, 2342, 2597, 2598, 2599, 4260, 4648, 5770, 6039, 6241, 9932, 9931, 9933,
                    9934, 9935, 12617, 12616, 12730, 12864, 12865, 19764, 20231, 20261, 24674, 26904, 26905
                };
                if (Cauterize.IsCasting())
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