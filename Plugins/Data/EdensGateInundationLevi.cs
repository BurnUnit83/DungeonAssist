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
				 16333,16334 //Temporary Current
				            };
            #endregion

            #region Custom Mechanics

           
						
			HashSet<uint> TempCurrentRight = new HashSet<uint>() { 16333 };
             if (TempCurrentRight.IsCasting())
             {
				Helpers.BossIds.ToggleSideStep();                
                Vector3 _loc = new Vector3(87, 0, 91);

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
				 Helpers.BossIds.ToggleSideStep();
             }
			
			 HashSet<uint> TempCurrentLeft = new HashSet<uint>() { 16334 };
             if (TempCurrentLeft.IsCasting())
             {
				Helpers.BossIds.ToggleSideStep();                
                Vector3 _loc = new Vector3(98, 0, 98);

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
				 Helpers.BossIds.ToggleSideStep();
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