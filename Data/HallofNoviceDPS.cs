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
    public class HallofNoviceDPS
    {
        static PluginContainer sidestepPlugin = PluginHelpers.GetSideStepPlugin();

        static HashSet<uint> Spells = new HashSet<uint>()
        {
        };

        public static async Task<bool> Run()
        {
            IEnumerable<BattleCharacter> axer = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                r => !r.IsMe && r.Distance() < 50 && r.NpcId == 4784 && r.IsAlive); // Axe-wielding Training Partner

            // Axe-wielding Training Partner 4784
            if (axer.Any())
            {
                var axeNpc = GameObjectManager.GetObjectsByNPCId<BattleCharacter>(4784)
                    .Where(obj => obj.IsVisible && obj.CurrentHealth > 1);
                foreach (var axer2 in axeNpc)
                {
                    if (axer2.Distance2D(Core.Me.Location) >= 3)

                    {
                        var _target = axer2.Location;
                        Navigator.PlayerMover.MoveTowards(_target);
                        while (_target.Distance2D(Core.Me.Location) >= 3)
                        {
                            Navigator.PlayerMover.MoveTowards(_target);
                            await Coroutine.Sleep(100);
                        }

                        Navigator.PlayerMover.MoveStop();
                    }

                    axer2.Target();
                    ActionManager.DoAction(ff14bot.Enums.ActionType.General, 1, axer2);
                    await Coroutine.Sleep(1500);
                }
            }

            await Coroutine.Yield();
            return false;
        }
    }
}