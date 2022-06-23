using Buddy.Coroutines;
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
using ff14bot.Navigation;
using LlamaLibrary.Helpers;
using TreeSharp;

namespace DungeonAssist
{
    public class DungeonAssist : BotPlugin
    {
        private Composite _coroutine;
        private DungeonAssistSettings _settingsForm;

        //private static NamedPipeClientStream pipe;

        private Composite deathCoroutine2;
        private static uint _buff = 48;

        public override string Author
        {
            get { return "NeonNeo, Kayla, DomesticWarlord, Antony "; }
        }
#if RB_CN
        public override string Name => "亲信战友";
#else
        public override string Name => "DungeonAssist";


#endif

        //public override string NameKAY { get; } = name;
        public override Version Version
        {
            get { return new Version(1, 1, 6); }
        }
        //changelog
        //V 1.1.1 = Disabling plugin on bot shutdown
        //V 1.1.2 = Corrected var plugin to var plugin2 for OSIRIS to run. Removing Death Logic and allowing Osiris to run
        //V 1.1.3 = Selects Yes to Auto Teleport to Battle
        //Loot was added to Orderbot Tags by Kayla, not needed here
        //V 1.1.4 = Exits Duty if instance is complete after a small delay.  Revives if necessary
        //V 1.1.5 = Supports Alexander Water Thingy Guy, Eden's Gate Innundation (Leviathan) and Hell's Lid zones
        //V 1.1.6 = Revive Death Logic Split up by Run Type
        //Todo


        private bool CanDungeonAssist() =>
            Array.IndexOf(new int[] {102, 138, 162, 172, 372, 444, 543, 544, 549, 550, 689, 742, 821, 822, 823, 836, 837, 851, 1042, 1043, 1044, 1046, 1047, 1048},
                WorldManager.ZoneId) >= 0;

        private bool TurnOffSideStep() => Array.IndexOf(new int[] {851, 1111}, WorldManager.ZoneId) >= 0;
        private bool ReviveRaid() => Array.IndexOf(new int[] {372, 742, 851}, WorldManager.ZoneId) >= 0;
        private bool ReviveDungeon() => Array.IndexOf(new int[] {111, 1111}, WorldManager.ZoneId) >= 0;

        public override void OnInitialize()
        {
            if ((PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any()) &&
                (!TurnOffSideStep()))
            {
                var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避")
                    .FirstOrDefault();
                if (_plugin.Enabled == false)
                {
                    _plugin.Enabled = true;
                }
            }
            else if ((PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避").Any()) &&
                     (TurnOffSideStep()))
            {
                var _plugin = PluginManager.Plugins.Where(p => p.Plugin.Name == "SideStep" || p.Plugin.Name == "回避")
                    .FirstOrDefault();
                if (_plugin.Enabled == true)
                {
                    _plugin.Enabled = false;
                }
            }

            if (PluginManager.Plugins.Where(p => p.Plugin.Name == "Osiris").Any())
            {
                var _plugin2 = PluginManager.Plugins.Where(p => p.Plugin.Name == "Osiris").FirstOrDefault();
                if (_plugin2.Enabled == false)
                {
                    _plugin2.Enabled = true;
                }
            }

            _coroutine = new Decorator(c => CanDungeonAssist(), new ActionRunCoroutine(r => RunDungeonAssist()));
        }

        public override void OnEnabled()
        {
            TreeRoot.OnStart += OnBotStart;
            TreeRoot.OnStop += OnBotStop;
            TreeHooks.Instance.OnHooksCleared += OnHooksCleared;

            if (TreeRoot.IsRunning)
            {
                AddHooks();
            }

            PluginContainer plugin = PluginHelpers.GetSideStepPlugin();
            if (plugin != null)
            {
                plugin.Enabled = true;
            }
        }

        public override void OnDisabled()
        {
            TreeRoot.OnStart -= OnBotStart;
            TreeRoot.OnStop -= OnBotStop;
            RemoveHooks();
        }

        public override void OnShutdown()
        {
            OnDisabled();
        }

        public override bool WantButton
        {
            get { return false; }
        }


        private void AddHooks()
        {
            Logging.Write(Colors.Aquamarine, "Adding DungeonAssist Hook");
            TreeHooks.Instance.AddHook("TreeStart", _coroutine);
        }

        private void RemoveHooks()
        {
            Logging.Write(Colors.Aquamarine, "Removing DungeonAssist Hook");
            TreeHooks.Instance.RemoveHook("TreeStart", _coroutine);
        }

        private void OnBotStop(BotBase bot)
        {
            
            RemoveHooks();
            if (PluginManager.Plugins.Where(p => p.Plugin.Name == "DungeonAssist" || p.Plugin.Name == "回避").Any())
            {
                var _plugin3 = PluginManager.Plugins.Where(p => p.Plugin.Name == "DungeonAssist").FirstOrDefault();
                if (_plugin3.Enabled == true)
                {
                    _plugin3.Enabled = false;
                }
            }
        }

        private void OnBotStart(BotBase bot)
        {
            AddHooks();
        }

        private void OnHooksCleared(object sender, EventArgs e)
        {
            RemoveHooks();
        }


        private static async Task<bool> PlayerCheck()
        {
            //This code makes sure you're alive before running
            if (Core.Me.CurrentHealthPercent <= 0)
            {
                return false;


                return true;
            }

            return false;
        }

        private async Task<bool> RunDungeonAssist()
        {
            
            if (!Core.Me.InCombat && ActionManager.IsSprintReady && MovementManager.IsMoving)
            {
                ActionManager.Sprint();
                await Coroutine.Wait(1000, () => !ActionManager.IsSprintReady);
            }

            if (SelectYesno.IsOpen && Core.Me.CurrentHealthPercent > 0)
            {
                Logging.Write(Colors.Aquamarine, "Clicking Yes to Teleport to Battle");
                SelectYesno.ClickYes();
            }

            if (DutyManager.InInstance) // && Core.Me.CurrentHealthPercent <= 0)
            {
                if (DirectorManager.ActiveDirector != null) //director isn't null
                {
                    //Checks Duty State
                    var activeAsInstance = (ff14bot.Directors.InstanceContentDirector) DirectorManager.ActiveDirector;
                    if (activeAsInstance.InstanceEnded) //Duty ended
                    {
                        if (Core.Me.CurrentHealthPercent <= 0)
                        {
                            await Coroutine.Sleep(500);
                            //Checks Dead State and Revives
                            await Coroutine.Wait(3000, () => ClientGameUiRevive.ReviveState == ReviveState.Dead);
                            Logging.Write(Colors.Aquamarine, "No one is in combat, releasing...");
                            await Coroutine.Sleep(500);
                            if (ReviveRaid())
                            {
                                SelectYesno.ClickYes();
                            }
                            else if (ReviveDungeon())
                            {
                                ff14bot.RemoteWindows.NotificationRevive.Click();
                            }
                            else
                            {
                                ff14bot.RemoteWindows.SelectOk.ClickOk();
                            }

                            while (CommonBehaviors.IsLoading)
                            {
                                Logging.Write(Colors.Aquamarine, "Waiting for zoning to finish...");
                                await Coroutine.Wait(-1, () => (!CommonBehaviors.IsLoading));
                            }

                            while (!Core.Me.IsAlive)
                            {
                                Logging.Write(Colors.Aquamarine, "Zoning finsihed, waiting to become alive...");
                                await Coroutine.Wait(-1, () => (Core.Me.IsAlive));
                            }
                        }
                    }
                }
            }
            //if (!Core.Player.HasAura(_buff)) { await EatFood(); }

            switch (WorldManager.ZoneId)
            {
                case 138: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await HallofNovice.Run();
                
                case 162: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await Halatali.Run();
                
                case 172: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await AurumVale.Run();
                
                case 372: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await SyrcusTower.Run();
                
                case 543: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await HallofNovice.Run();
                
                case 544: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await HallofNoviceDPS.Run();
                
                case 549: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await HallofNoviceHealer.Run();
                
                case 550: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await HallofNoviceHealer.Run();
                
                case 689: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await AlaMhigo.Run();
                
                case 742: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await HellsLid.Run();
                
                case 821:
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await DohnMheg.Run();
                
                case 822:
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await MtGulg.Run();
                
                case 823: 
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await QitanaRavel.Run();
                
                case 836: 
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await MalikahsWell.Run();
                
                case 837: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await HolminsterSwitch.Run();
                
                case 851: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await EdensGateInundationLevi.Run();
                
                case 1042: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await TheStoneVigil.Run();
                
                case 1043: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await CastrumMeridianum.Run();
                
                case 1044: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await ThePraetorium.Run();
                
                case 1046: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await TheNavel.Run();
                
                case 1047: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await TheHowlingEye.Run();
                
                case 1048: //80本 国际服 5.1
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await ThePortaDecumana.Run();
                
                default:
                    if (await PlayerCheck())
                    {
                        return true;
                    }
                    return await SyrcusTower.Run();
            }
        }
    }

    public class Settings : JsonSettings
    {
        private static Settings _instance;

        public static Settings Instance
        {
            get
            {
                return _instance ?? (_instance = new Settings());
                ;
            }
        }

        public Settings() : base(Path.Combine(CharacterSettingsDirectory, "DungeonAssist.json"))
        {
        }

        [Setting] public uint Id { get; set; }
    }
}