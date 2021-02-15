﻿using Buddy.Coroutines;
using ff14bot;
using ff14bot.AClasses;
using ff14bot.Behavior;
using ff14bot.Helpers;
using ff14bot.Managers;
using ff14bot.NeoProfiles;
using ff14bot.Enums;
using ff14bot.Helpers;
using ff14bot.RemoteWindows;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using TreeSharp;

namespace SycrusAssist
{
    public class SycrusAssist : BotPlugin
    {
        private Composite _coroutine;
        private SycrusAssistSettings _settingsForm;
		
        //private static NamedPipeClientStream pipe;

        private Composite deathCoroutine2;
        private static uint _buff = 48;

        public override string Author { get { return "NeonNeo, Kayla (For Death Logic)"; } }
#if RB_CN
        public override string Name => "亲信战友";
#else
        public override string Name => "SycrusAssist";
	
	
#endif

		//public override string NameKAY { get; } = name;
        public override Version Version { get { return new Version(1, 2, 0); } }

        private bool CanSycrusAssist() => Array.IndexOf(new int[] { 102, 372 }, WorldManager.ZoneId) >= 0;
        public override void OnInitialize()
        {
            if (PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any())
            {
                var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").FirstOrDefault();
                if (_plugin.Enabled == false) { _plugin.Enabled = true; }
            }
			
			if (PluginManager.Plugins.Where(p => p.Plugin.Name == "Osiris" ).Any())
            {
                var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "Osiris").FirstOrDefault();
                if (_plugin.Enabled == false) { _plugin.Enabled = true; }
            }

            _coroutine = new Decorator(c => CanSycrusAssist(), new ActionRunCoroutine(r => RunSycrusAssist()));
			//deathCoroutine2 = new ActionRunCoroutine(ctx => HandleDeath2());
        }

        public override void OnEnabled()
        {
            TreeRoot.OnStart += OnBotStart;
            TreeRoot.OnStop += OnBotStop;
            TreeHooks.Instance.OnHooksCleared += OnHooksCleared;

            if (TreeRoot.IsRunning) { AddHooks(); }
        }

        public override void OnDisabled()
        {
            TreeRoot.OnStart -= OnBotStart;
            TreeRoot.OnStop -= OnBotStop;
            RemoveHooks();
        }

        public override void OnShutdown() { OnDisabled(); }

        public override bool WantButton { get { return false; } }


        private void AddHooks()
        {
            Logging.Write(Colors.Aquamarine, "Adding SycrusAssist Hook");
            TreeHooks.Instance.AddHook("TreeStart", _coroutine);
        }

        private void RemoveHooks()
        {
            Logging.Write(Colors.Aquamarine, "Removing SycrusAssist Hook");
            TreeHooks.Instance.RemoveHook("TreeStart", _coroutine);
        }

        private void OnBotStop(BotBase bot) { RemoveHooks(); }

        private void OnBotStart(BotBase bot) { AddHooks(); }

        private void OnHooksCleared(object sender, EventArgs e) { RemoveHooks(); }

        
        private static async Task<bool> PlayerCheck()
        {
			//This code makes sure you're alive before running
          if (Core.Me.CurrentHealthPercent <= 0)
            {
                Logging.Write(Colors.Aquamarine, $"Player has died.");
                
				await Coroutine.Sleep(5000);
				Logging.Write(Colors.Aquamarine, $"Handling Death in Sycrus Assist.");

				await Coroutine.Wait(20000, () => ClientGameUiRevive.ReviveState == ReviveState.Dead);


            await Coroutine.Wait(-1, () => Core.Me.HasAura(148));
            await Coroutine.Sleep(500);
            Logging.Write(Colors.Aquamarine, $"We wave Raise Aura.");
            if (NotificationRevive.IsOpen)
            {
            Logging.Write(Colors.Aquamarine, $"Clicking Rause.");
                ClientGameUiRevive.Revive();
            }

            await Coroutine.Wait(20000, () => ClientGameUiRevive.ReviveState == ReviveState.None);

            Poi.Clear("We live!");
			Logging.Write(Colors.Aquamarine, $"Yes, We Live");
			
			 NeoProfileManager.Load(NeoProfileManager.CurrentProfile.Path, true);
                
				NeoProfileManager.UpdateCurrentProfileBehavior();
            return false;


               NeoProfileManager.Load(NeoProfileManager.CurrentProfile.Path, true);
                
				NeoProfileManager.UpdateCurrentProfileBehavior();
                await Coroutine.Sleep(5000);
                return true;
            }

            return false;
        }
        private async Task<bool> RunSycrusAssist()
        {
            if (!Core.Me.InCombat && ActionManager.IsSprintReady && MovementManager.IsMoving)
            {
                ActionManager.Sprint();
                await Coroutine.Wait(1000, () => !ActionManager.IsSprintReady);
            }

            //if (!Core.Player.HasAura(_buff)) { await EatFood(); }

            switch (WorldManager.ZoneId)
            {
                
				case 372: //80本 国际服 5.1
                    if (await PlayerCheck())  {  return true; }
                    return await SyrcusTower.Run();
                default:
                    if (await PlayerCheck())  {  return true; }
                    return await SyrcusTower.Run();
            }
        }
		private async Task<bool> HandleDeath2()
        {
            Logging.Write(Colors.Aquamarine, $"Dead, Attempting Profile Reload");
            
				NeoProfileManager.Load(NeoProfileManager.CurrentProfile.Path, true);
                
				NeoProfileManager.UpdateCurrentProfileBehavior();
            return false;
        }
    }

    public class Settings : JsonSettings
    {
        private static Settings _instance;
        public static Settings Instance { get { return _instance ?? (_instance = new Settings()); ; } }

        public Settings() : base(Path.Combine(CharacterSettingsDirectory, "SycrusAssist.json")) { }

        [Setting]
        public uint Id { get; set; }
    }
}
