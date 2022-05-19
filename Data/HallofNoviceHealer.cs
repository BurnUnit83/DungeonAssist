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
    public class HallofNoviceHealer
    {
        static PluginContainer sidestepPlugin = PluginHelpers.GetSideStepPlugin();

        static HashSet<uint> Spells = new HashSet<uint>()
        {
        };

        public static async Task<bool> Run()
        {
            IEnumerable<BattleCharacter> gladiator = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                r => !r.IsMe && r.Distance() < 50 && r.NpcId == 4781 && r.IsAlive); // Guild Gladiator 
            IEnumerable<BattleCharacter> archer = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                r => !r.IsMe && r.Distance() < 50 && r.NpcId == 4783 && r.IsAlive); // Guild Archer 

            // Guild Gladiator  4781
            if (gladiator.Any())
            {
                var gladiatorNpc = GameObjectManager.GetObjectsByNPCId<BattleCharacter>(4781)
                    .Where(obj => obj.IsVisible && obj.CurrentHealth > 1);
                foreach (var gladiator2 in gladiatorNpc)
                {
                    if (gladiator2.CurrentHealthPercent <= 75)
                    {
                        if (gladiator2.Distance2D(Core.Me.Location) >= 15)

                        {
                            
                            if (Core.Me.IsCasting)
                            {
                                ActionManager.StopCasting();
                            }
                            
                            var _target = gladiator2.Location;
                            Navigator.PlayerMover.MoveTowards(_target);
                            while (_target.Distance2D(Core.Me.Location) >= 15)
                            {
                                Navigator.PlayerMover.MoveTowards(_target);
                                await Coroutine.Sleep(100);
                            }

                            Navigator.PlayerMover.MoveStop();
                        }

                        ActionManager.DoAction(120, gladiator2);
                        await Coroutine.Sleep(2000);                        
                    }

                }
            }
            
            // Guild Archer  4783
            if (archer.Any())
            {
                var archerNpc = GameObjectManager.GetObjectsByNPCId<BattleCharacter>(4783)
                    .Where(obj => obj.IsVisible && obj.CurrentHealth > 1);
                foreach (var archer2 in archerNpc)
                {
                    if (archer2.CurrentHealthPercent <= 75)
                    {
                        if (Core.Me.IsCasting)
                        {
                            ActionManager.StopCasting();
                        }
                        
                        if (archer2.Distance2D(Core.Me.Location) >= 15)

                        {
                            var _target = archer2.Location;
                            Navigator.PlayerMover.MoveTowards(_target);
                            while (_target.Distance2D(Core.Me.Location) >= 15)
                            {
                                Navigator.PlayerMover.MoveTowards(_target);
                                await Coroutine.Sleep(100);
                            }

                            Navigator.PlayerMover.MoveStop();
                        }

                        ActionManager.DoAction(120, archer2);
                        await Coroutine.Sleep(2500);                        
                    }

                }
            }

            await Coroutine.Yield();
            return false;
        }
    }
}