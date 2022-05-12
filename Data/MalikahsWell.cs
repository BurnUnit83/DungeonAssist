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
    public class MalikahsWell
    {
        static PluginContainer sidestepPlugin = PluginHelpers.GetSideStepPlugin();

        static HashSet<uint> Spells = new HashSet<uint>()
        {
            // Greater Armadillo 8252
            // Falling Rock
            15594,
            // Head Toss
            15590,
            // Right Round
            15591, 15592,
            // FLail Smash
            15593,
            // Earthshake
            15929,
            
            // Amphibious Talos
            //Swift Spill
            15599,15600,
            
            // Storge 8249
            // Heretic's Fork
            15602, 15609,
            // Breaking Wheel
            15605, 15610,
            // Crystal Nail
            15606, 15607
            

        };

        public static async Task<bool> Run()
        {

            IEnumerable<BattleCharacter> greaterArmadillo = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                r => !r.IsMe && r.Distance() < 50 && r.NpcId == 8252);  // Greater Armadillo
            IEnumerable<BattleCharacter> amphibiousTalos = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                r => !r.IsMe && r.Distance() < 50 && r.NpcId == 8250);  // Amphibious Talos
            IEnumerable<BattleCharacter> storge = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                r => !r.IsMe && r.Distance() < 50 && r.NpcId == 8249);  // Storge
            
            // Greater Armadillo 8252
            if (greaterArmadillo.Any())
            {
                
                if (sidestepPlugin.Enabled)
                {
                    sidestepPlugin.Enabled = false;
                }
                
                HashSet<uint> FallingRock = new HashSet<uint>() { 15594 };
                if (FallingRock.IsCasting())
                {
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }                

                HashSet<uint> HeadToss = new HashSet<uint>() { 15590 };
                if (HeadToss.IsCasting())
                {
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> RightRound = new HashSet<uint>() { 15591, 15592 };
                if (RightRound.IsCasting())
                {
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> FLailSmash = new HashSet<uint>() { 15593 };
                if (FLailSmash.IsCasting())
                {
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                    
                }
                
                HashSet<uint> Earthshake = new HashSet<uint>() { 15929 };
                if (Earthshake.IsCasting())
                {
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }

                if (!greaterArmadillo.Any())
                {
                    if (!sidestepPlugin.Enabled)
                    {
                        sidestepPlugin.Enabled = true;
                    }                  
                }


            }

            // Batsquatch 8232
            if (amphibiousTalos.Any())
            {

                HashSet<uint> Soundwave = new HashSet<uint>() { 15506 };
                if (Soundwave.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }            
            }
            
            // Storge 8249
            if (storge.Any())
            {
                
                if (sidestepPlugin.Enabled)
                {
                    sidestepPlugin.Enabled = false;
                }

                HashSet<uint> HereticsFork = new HashSet<uint>() { 15602, 15609 };
                if (HereticsFork.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                } 
                
                HashSet<uint> BreakingWheel = new HashSet<uint>() { 15605, 15610 };
                if (BreakingWheel.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                } 
                
                HashSet<uint> CrystalNail = new HashSet<uint>() { 15606, 15607 };
                if (CrystalNail.IsCasting())
                {
                    //sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                } 
                
            }

            sidestepPlugin.Enabled = true;
            await Coroutine.Yield();
            return false;
        }
    }
}