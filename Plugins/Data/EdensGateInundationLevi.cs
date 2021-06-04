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
				  16333,16334 		//Temporary Current
				 ,16337 			//Undersea Quake
				 ,16338,16339 		//TidalWave (Preview and Execute)
				 //,16328 			//Drenching Pulse
				 //,16344			//Maelstrom (Where it divebombs) Needs avoidance from the two whirlpools
				 //Needs Mechanic for Blue Circle with Red Squares over head to seperate
				            };
            #endregion
			
            #region Custom Mechanics

           
				/*		
			HashSet<uint> TempCurrentRight = new HashSet<uint>() { 16333 };
             if (TempCurrentRight.IsCasting())
             {
				Logging.Write(Colors.Aquamarine, $"Temporary Current Right Handling - Handling");
				//SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == true) 
							{ _plugin.Enabled = false; 
								Logging.Write(Colors.Aquamarine, $"SideStep Off!");
							}
						}
				//SIDESTEP TOGGLE				
                Logging.Write(Colors.Aquamarine, $"Temporary Current Right Handling - Moving to Coordinates");
				Vector3 _loc = new Vector3(113, 0, 83);

                while (Core.Me.Distance(_loc) > 2f)
                {
                    await CommonTasks.MoveTo(_loc);
                    await Coroutine.Yield();
                }
				Logging.Write(Colors.Aquamarine, $"Temporary Current Right Handling - Sleeping");
                await Coroutine.Sleep(3000);

                if (ActionManager.IsSprintReady)
                {
                    ActionManager.Sprint();
                    await Coroutine.Wait(1000, () => !ActionManager.IsSprintReady);
                }
                 Stopwatch sw = new Stopwatch();
                 sw.Start();
				 
                 Logging.Write(Colors.Aquamarine, $"Temporary Current Right Handling - Sleeping 2");
				 await Coroutine.Sleep(1000);
                 await Coroutine.Yield();
             
                 sw.Stop();
				 //SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == false) { _plugin.Enabled = true; Logging.Write(Colors.Aquamarine, $"Temporary Current Right Handling - Sidestep On");}
						}
				//SIDESTEP TOGGLE	
				Logging.Write(Colors.Aquamarine, $"Temporary Current Right Handling - Complete");
             }
			
			 HashSet<uint> TempCurrentLeft = new HashSet<uint>() { 16334 };
             if (TempCurrentLeft.IsCasting())
             {
				 Logging.Write(Colors.Aquamarine, $"Temporary Current Left Handling - Handling");
				//SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == true) { _plugin.Enabled = false; Logging.Write(Colors.Aquamarine, $"Temporary Current Left Handling - Sidestep Off!");}
						}
				//SIDESTEP TOGGLE	        
					Logginwg.Write(Colors.Aquamarine, $"Temporary Current Left Handling - Moving to Coordinates");
                Vector3 _loc = new Vector3(86, 0, 83);

                while (Core.Me.Distance(_loc) > 2f)
                {
                    await CommonTasks.MoveTo(_loc);
                    await Coroutine.Yield();
                }
				Logging.Write(Colors.Aquamarine, $"Temporary Current Left Handling - Sleeping 1");
                await Coroutine.Sleep(3000);

                if (ActionManager.IsSprintReady)
                {
                    ActionManager.Sprint();
                    await Coroutine.Wait(1000, () => !ActionManager.IsSprintReady);
                }
                 Stopwatch sw = new Stopwatch();
                 sw.Start();
				 //Logging.Write(Colors.Aquamarine, $"Ancient Flare Handling");
                 
				 Logging.Write(Colors.Aquamarine, $"Temporary Current Left Handling - Sleeping 2");
				 await Coroutine.Sleep(1000);
                 await Coroutine.Yield();
             
                 sw.Stop();
				 //SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == false) { _plugin.Enabled = true; Logging.Write(Colors.Aquamarine, $"Temporary Current Left Handling - Sidestep On");}
						}
				//SIDESTEP TOGGLE	
				Logging.Write(Colors.Aquamarine, $"Temporary Current Left Handling - Complete");
             }
			 HashSet<uint> TidalWaveQuake = new HashSet<uint>() { 16337,16338,16339 };
             if (TidalWaveQuake.IsCasting())
             {
				 Logging.Write(Colors.Aquamarine, $"Quale/Tidal Handling - Triggerered");
				//SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == true) { _plugin.Enabled = false; Logging.Write(Colors.Aquamarine, $"Quale/Tidal Handling - Sidestep Off");}
						}
				//SIDESTEP TOGGLE	
				Logging.Write(Colors.Aquamarine, $"Quale/Tidal Handling - Moving to Dead Center");
                Vector3 _loc = new Vector3(99, 0, 99);

                while (Core.Me.Distance(_loc) > 2f)
                {
                    await CommonTasks.MoveTo(_loc);
                    await Coroutine.Yield();
                }
				Logging.Write(Colors.Aquamarine, $"Quale/Tidal Handling - Sleeping 1");
                await Coroutine.Sleep(3000);

                if (ActionManager.IsSprintReady)
                {
                    ActionManager.Sprint();
                    await Coroutine.Wait(1000, () => !ActionManager.IsSprintReady);
                }
                 Stopwatch sw = new Stopwatch();
                 sw.Start();
				 //Logging.Write(Colors.Aquamarine, $"Ancient Flare Handling");
                 
				 Logging.Write(Colors.Aquamarine, $"Quale/Tidal Handling - Sleeping 2");
				 await Coroutine.Sleep(1000);
                 await Coroutine.Yield();
             
                 sw.Stop();
				//SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == false) { _plugin.Enabled = true; Logging.Write(Colors.Aquamarine, $"Quale/Tidal Handling - Sidestep On");}
						}
				//SIDESTEP TOGGLE	
				Logging.Write(Colors.Aquamarine, $"Quale/Tidal Handling - Complete");
             }
			 
			*/
            #endregion

            /// Default (缺省)
            if (Spells.IsCasting()) { Core.Me.ClearTarget();			//Logging.Write(Colors.Aquamarine, $"Default Spell Handling");
					Logging.Write(Colors.Aquamarine, $"Generic Default Handling - Triggered");
					while (Core.Me.Location.Distance2D(PartyManager.VisibleMembers.Where(x => !x.IsMe && x.BattleCharacter.IsAlive).FirstOrDefault().BattleCharacter.Location) > 0.5)
					{
						MovementManager.SetFacing(PartyManager.VisibleMembers.Where(x => !x.IsMe && x.BattleCharacter.IsAlive).FirstOrDefault().BattleCharacter.Location);
						Logging.Write(Colors.Aquamarine, $"Generic Default Handling - Moving to Nearest Alive Party Character");
						MovementManager.MoveForwardStart();
						Logging.Write(Colors.Aquamarine, $"Generic Default Handling - Waiting");
						await Coroutine.Sleep(100);
						MovementManager.MoveStop();
					}
					Logging.Write(Colors.Aquamarine, $"Generic Default Handling - Complete");
				//await Coroutine.Sleep(1000);
                    await Coroutine.Yield(); 

            /// SideStep (回避)
            //if (WorldManager.SubZoneId != 2996) { Helpers.BossIds.ToggleSideStep(new uint[] { 8210 }); } else { Helpers.BossIds.ToggleSideStep(); }
			}
            return false;
        }
    }
}