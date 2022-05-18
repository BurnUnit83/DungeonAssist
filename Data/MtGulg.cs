using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buddy.Coroutines;
using ff14bot.Managers;
using ff14bot.Objects;


namespace DungeonAssist
{
    public class MtGulg
    {
        static PluginContainer sidestepPlugin = PluginHelpers.GetSideStepPlugin();


        // Forgiven Cruelty 8260
        // 16818                                        :: Lumen Infinitum  
        // 15614, 15615, 15616, 15617, 15618, 17153     :: Typhoon Wing - Handled by SideStep    

        // Forgiven Whimsy 8261
        // 15622, 15623, 16987, 16988, 16989            :: Exegesis

        // Forgiven Obscenity 8262
        // 15638, 15640, 15641, 15649, 18025            :: Divine Diminuendo        
        // 15642, 15643, 15648                          :: Conviction Marcato     


        // 15644                                        :: Penance Pianissimo
        // 15645                                        :: Feather Marionette

        // 16247, 16248                                 :: Right Palm
        // 16249, 16250                                 :: Left Palm

        // 16521                                        :: Glittering Emerald


        static HashSet<uint> Spells = new HashSet<uint>()
        {
            15614, 15615, 15616, 15617, 15618, 15622, 15623, 15638,
            15640, 15641, 15642, 15643, 15644, 15645, 15648, 15649,
            16247, 16248, 16249, 16250, 16521, 16818, 16987, 16988,
            16989, 17153, 18025,
        };

        public static async Task<bool> Run()
        {
            IEnumerable<BattleCharacter> forgivenCruelty = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                r => !r.IsMe && r.Distance() < 50 && r.NpcId == 8260); // Forgiven Cruelty
            
            IEnumerable<BattleCharacter> forgivenWhimsy = GameObjectManager.GetObjectsOfType<BattleCharacter>().Where(
                r => !r.IsMe && r.Distance() < 50 && r.NpcId == 8261); // Forgiven Whimsy
            
            IEnumerable<BattleCharacter> forgivenRevelry = GameObjectManager.GetObjectsOfType<BattleCharacter>()
                .Where(
                    r => !r.IsMe && r.Distance() < 50 && r.NpcId == 8270); // Forgiven Revelry
            
            IEnumerable<BattleCharacter> forgivenObscenity = GameObjectManager.GetObjectsOfType<BattleCharacter>()
                .Where(
                    r => !r.IsMe && r.Distance() < 50 && r.NpcId == 8262); // Forgiven Obscenity

            // Forgiven Cruelty 8260
            if (forgivenCruelty.Any())
            {
                HashSet<uint> LumenInfinitum = new HashSet<uint>() {16818};
                if (LumenInfinitum.IsCasting())
                {
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
            }

            // Forgiven Whimsy 8261
            if (forgivenWhimsy.Any())
            {
                sidestepPlugin.Enabled = false;

                HashSet<uint> Exegesis = new HashSet<uint>() {15622, 15623, 16987, 16988, 16989};
                if (Exegesis.IsCasting())
                {
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
            }

            if (!forgivenWhimsy.Any())
            {
                if (!sidestepPlugin.Enabled)
                {
                    sidestepPlugin.Enabled = true;
                }
            }
            
            // Forgiven Revelry 8270
            if (forgivenRevelry.Any())
            {

                HashSet<uint> RightPalm = new HashSet<uint>() { 16247, 16248 };
                if (RightPalm.IsCasting())
                {
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
                
                HashSet<uint> LeftPalm = new HashSet<uint>() { 16249, 16250 };
                if (LeftPalm.IsCasting())
                {
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }
            }
   

            // Forgiven Obscenity 8262
            if (forgivenObscenity.Any())
            {
                HashSet<uint> DivineDiminuendo = new HashSet<uint>() {15638, 15640, 15641, 15649, 18025};
                if (DivineDiminuendo.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }

                HashSet<uint> ConvictionMarcato = new HashSet<uint>() {15642, 15643, 15648};
                if (ConvictionMarcato.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }

                // 15652                                        :: Ringsmith
                // 15653                                        :: Gold Chaser
                // 17066                                        :: Solitaire Ring

                HashSet<uint> RingsmithGoldChaserSolitaireRing = new HashSet<uint>() {15652, 15653, 17066};
                if (RingsmithGoldChaserSolitaireRing.IsCasting())
                {
                    sidestepPlugin.Enabled = false;
                    AvoidanceManager.RemoveAllAvoids(i => i.CanRun);
                    await MovementHelpers.GetClosestDps.Follow();
                }

                if (!Spells.IsCasting())
                {
                    if (!sidestepPlugin.Enabled)
                    {
                        await Coroutine.Sleep(500);
                        sidestepPlugin.Enabled = true;
                    }
                }
            }


            await Coroutine.Yield();
            return false;
        }
    }
}