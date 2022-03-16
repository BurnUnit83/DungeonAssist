using Buddy.Coroutines;
using Clio.Utilities;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.Managers;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using ff14bot.Helpers;
using System.Windows.Media;


namespace DungeonAssist
{
    public class EdensGateInundationLevi
    {
        public static async Task<bool> Run()
        {
            // NOT TESTED

            #region Spell Filters
     
           HashSet<uint> Spells = new HashSet<uint>()
            {                		
				  16333,16334 			//Temporary Current
				 ,16337,16335,16336 		//Undersea Quake
				 ,16338,16339 			//TidalWave (Preview and Execute)
				 //,16328 				//Drenching Pulse (Needs a way to avoid others) && Freak Wave
				 //Needs Mechanic for Blue Circle with Red Squares over head to seperate
				 ,16344 			//Spinning Dive (Dive Bombs after Maelstrom)
				 ,16376				//Surging Tsunami (Pushback Mechanic)
				 //,16341				//Swirling Tsunami (Donut AOE) need way to detect if me or other player
				 ,16332				//Killer Wave (Stacking Mechanic)
				 //,16326			//Rip Current (Tank Buster to be coded later)
				            };
            #endregion
			
            #region Custom Mechanics

           
				
			HashSet<uint> SurgingTsunami = new HashSet<uint>() { 16376 };
             if (SurgingTsunami.IsCasting() && Core.Me.CurrentHealthPercent > 0)
             {
				Logging.Write(Colors.Aquamarine, $"Dungeon Assist Eden Mechanic - Swirling Tsunami Handling - Triggered - To Center");
						
				Vector3 _loc = new Vector3(99, 0, 99);

                while (Core.Me.Distance(_loc) > 2f)
                {
                    await CommonTasks.MoveTo(_loc);
                    await Coroutine.Yield();
                }
				Logging.Write(Colors.Aquamarine, $"Dungeon Assist Eden Mechanic - Swirling Tsunami Handling - Sleeping");
                await Coroutine.Sleep(3000);

                if (ActionManager.IsSprintReady)
                {
                    ActionManager.Sprint();
                    await Coroutine.Wait(1000, () => !ActionManager.IsSprintReady);
                }
                 Stopwatch sw = new Stopwatch();
                 sw.Start();
				 
                 Logging.Write(Colors.Aquamarine, $"Dungeon Assist Eden Mechanic - Swirling Tsunami Handling - Sleeping 2");
				 await Coroutine.Sleep(1000);
                 await Coroutine.Yield();
             
                 sw.Stop();
				
				Logging.Write(Colors.Aquamarine, $"Dungeon Assist Eden Mechanic - Swirling Tsunami Handling -  - Complete");
             }
			 
			
            #endregion

            /// Default (缺省)
            if (Spells.IsCasting() && Core.Me.CurrentHealthPercent > 0) { 
			
					Logging.Write(Colors.Aquamarine, $"Dungeon Assist Eden Mechanic - Generic Default Handling - Triggered");
					//Core.Me.ClearTarget();						//Clears Target
						
					await Coroutine.Sleep(700);
					while (Core.Me.Location.Distance2D(PartyManager.VisibleMembers.Where(x => !x.IsMe && x.BattleCharacter.IsAlive).FirstOrDefault().BattleCharacter.Location) > 1)
					{
						if (ActionManager.IsSprintReady)
							{
							ActionManager.Sprint();
							await Coroutine.Wait(1000, () => !ActionManager.IsSprintReady);
							}
						//if (AvoidanceManager.IsRunningOutOfAvoid) 	//&& in that zone  (Mechanics file is loaded only for this zone
						//{
							AvoidanceManager.RemoveAllAvoids((info => true));
							AvoidanceManager.ResetNavigation();
						//}
						MovementManager.SetFacing(PartyManager.VisibleMembers.Where(x => !x.IsMe && x.BattleCharacter.IsAlive).FirstOrDefault().BattleCharacter.Location);
						Logging.Write(Colors.Aquamarine, $"Dungeon Assist Eden Mechanic - Generic Default Handling - Moving to Nearest Alive Party Character");
						MovementManager.MoveForwardStart();
							if(Core.Me.CurrentHealthPercent <= 0)
							{break;}
						Logging.Write(Colors.Aquamarine, $"Dungeon Assist Eden Mechanic - Generic Default Handling - Waiting");
						await Coroutine.Sleep(100);
						MovementManager.MoveStop();
						
					
						
					}
					Logging.Write(Colors.Aquamarine, $"Dungeon Assist Eden Mechanic - Generic Default Handling - Complete");
					//await Coroutine.Sleep(1000);
                    await Coroutine.Yield(); 
			}
			return false;
        }
    }
}