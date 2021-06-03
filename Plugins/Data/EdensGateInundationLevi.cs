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

           
						
			HashSet<uint> TempCurrentRight = new HashSet<uint>() { 16333 };
             if (TempCurrentRight.IsCasting())
             {
				//SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == true) { _plugin.Enabled = false; }
						}
				//SIDESTEP TOGGLE				
                Vector3 _loc = new Vector3(81, 0, 112);

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
				 //SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == false) { _plugin.Enabled = true; }
						}
				//SIDESTEP TOGGLE	
             }
			
			 HashSet<uint> TempCurrentLeft = new HashSet<uint>() { 16334 };
             if (TempCurrentLeft.IsCasting())
             {
				//SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == true) { _plugin.Enabled = false; }
						}
				//SIDESTEP TOGGLE	              
                Vector3 _loc = new Vector3(87, 0, 82);

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
				 //SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == false) { _plugin.Enabled = true; }
						}
				//SIDESTEP TOGGLE	
             }
			 HashSet<uint> TidalWaveQuake = new HashSet<uint>() { 16337,16338,16339 };
             if (TidalWaveQuake.IsCasting())
             {
				//SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == true) { _plugin.Enabled = false; }
						}
				//SIDESTEP TOGGLE	                
                Vector3 _loc = new Vector3(99, 0, 99);

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
				//SIDESTEP TOGGLE
					 if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
						{
							var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
							if (_plugin.Enabled == false) { _plugin.Enabled = true; }
						}
				//SIDESTEP TOGGLE	
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