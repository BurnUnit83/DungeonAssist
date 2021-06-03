//harvest
//hell of water
	// move to side
//water ball mopving -> genbu
	//wait 10 seconds and run towards location
//hell of waste
	//spread out
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
    public class EdensGateUnundationLevi
    {
        public static async Task<bool> Run()
        {
            // NOT TESTED

            #region Spell Filters
            /// 532, 1837, 2794, 5445, 7931, 9076, 9338, 9490, 2441
     
           HashSet<uint> Spells = new HashSet<uint>()
            {
                //1st boss 100 tonze swing
				//10176 liquid capace (constant spewing attack, needs to run away)
				//2nd boss Reapders gale
				//10599. 10187 <45.62811, -26, -105.941> or Current XYZ: <50.26293, -26, -111.4103>
				 11541,10192 //Hell of Water by Genbu
				,10193,10194 //Hell of Waste by Genbu
				//,10196,10197 //Sinister Tide (Arrow Mechanic)
				
            };
            #endregion

            #region Custom Mechanics

           
						
			HashSet<uint> HellOfWater = new HashSet<uint>() { 11541,10192 };
             if (HellOfWater.IsCasting())
             {
				 //Avoider(AvoiderType.Spell, 11541); //Sidestep should have this =\
				 //Avoider(AvoiderType.Spell, 10192);
               
                Vector3 _loc = new Vector3(55, -88, -461);

                while (Core.Me.Distance(_loc) > 1f)
                {
                    await CommonTasks.MoveTo(_loc);
                    await Coroutine.Yield();
                }

                await Coroutine.Sleep(3000);

                if (ActionManager.IsSprintReady)
                {
                    ActionManager.Sprint();
                    await Coroutine.Wait(1000, () => !ActionManager.IsSprintReady);
                }
                 Stopwatch sw = new Stopwatch();
                 sw.Start();
				 //Logging.Write(Colors.Aquamarine, $"Ancient Flare Handling");
                 
				 await Coroutine.Sleep(1000);
                 await Coroutine.Yield();
             
                 sw.Stop();
             }
			
			 
			HashSet<uint> HellOfWaste = new HashSet<uint>() { 10193 };
             if (HellOfWaste.IsCasting())
             {
				Vector3 _loc = new Vector3(74, -88, -468);

                while (Core.Me.Distance(_loc) > 1f)
                {
                    await CommonTasks.MoveTo(_loc);
                    await Coroutine.Yield();
                }

                await Coroutine.Sleep(3000);

                if (ActionManager.IsSprintReady)
                {
                    ActionManager.Sprint();
                    await Coroutine.Wait(1000, () => !ActionManager.IsSprintReady);
                }
                 Stopwatch sw = new Stopwatch();
                 sw.Start();
				 //Logging.Write(Colors.Aquamarine, $"Ancient Flare Handling");
                 
				 await Coroutine.Sleep(1000);
                 await Coroutine.Yield();
             
                 sw.Stop();
             }
			 
			 HashSet<uint> HellOfWaste2 = new HashSet<uint>() { 10194 };
             if (HellOfWaste2.IsCasting())
             {
				Vector3 _loc = new Vector3(50, -88, -468);

                while (Core.Me.Distance(_loc) > 1f)
                {
                    await CommonTasks.MoveTo(_loc);
                    await Coroutine.Yield();
                }

                await Coroutine.Sleep(3000);

                if (ActionManager.IsSprintReady)
                {
                    ActionManager.Sprint();
                    await Coroutine.Wait(1000, () => !ActionManager.IsSprintReady);
                }
                 Stopwatch sw = new Stopwatch();
                 sw.Start();
				 //Logging.Write(Colors.Aquamarine, $"Ancient Flare Handling");
                 
				 await Coroutine.Sleep(1000);
                 await Coroutine.Yield();
             
                 sw.Stop();
             }
			 
			
            #endregion

            /// Default (缺省)
            if (Spells.IsCasting()) { Core.Me.ClearTarget();			//Logging.Write(Colors.Aquamarine, $"Default Spell Handling");
					while (Core.Me.Location.Distance2D(PartyManager.VisibleMembers.Where(x => !x.IsMe && x.BattleCharacter.IsAlive).FirstOrDefault().BattleCharacter.Location) > 0.5)
					{
						MovementManager.SetFacing(PartyManager.VisibleMembers.Where(x => !x.IsMe && x.BattleCharacter.IsAlive).FirstOrDefault().BattleCharacter.Location);
						MovementManager.MoveForwardStart();
						await Coroutine.Sleep(100);
						MovementManager.MoveStop();
					}
				//await Coroutine.Sleep(1000);
                    await Coroutine.Yield();; }

            /// SideStep (回避)
            //if (WorldManager.SubZoneId != 2996) { Helpers.BossIds.ToggleSideStep(new uint[] { 8210 }); } else { Helpers.BossIds.ToggleSideStep(); }

            return false;
        }
    }
}