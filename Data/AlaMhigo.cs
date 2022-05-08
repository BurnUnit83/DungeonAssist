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

namespace DungeonAssist
{
    public class AlaMhigo
    {
        
        static PluginContainer sidestepPlugin = PluginHelpers.GetSideStepPlugin();
        public static async Task<bool> Run()
        {
            //Lockhart
            if (GameObjectManager.GetObjectByNPCId(6038) != null && Core.Me.HasAura(779)) //Out of body
            {
                var npc = GameObjectManager
                    .GetObjectsByNPCIds<GameObject>(new uint[] {6666})
                    .OrderBy(i => i.Distance()).FirstOrDefault();

                if (npc != default(GameObject))
                {
                    
                    AvoidanceManager.RemoveAllAvoids(i=> i.CanRun);
                    var ids = GameObjectManager.GetObjectsByNPCId(6390).Select(i => i.ObjectId).ToArray();
                    AvoidanceManager.AddAvoidObject<GameObject>(()=> true, 6f, ids);
                    
                    Logging.Write("We have aura, moving to our body.");
                    if (await Navigation.FlightorMove(npc.Location))
                    {
                        npc.Interact();
                        await Coroutine.Wait(10000, () => !Core.Me.HasAura(302));
                    }
                    else
                    {
                        //Couldn't get to the npc
                    }
                }
                else
                {
                    //Can't find npc
                }
                
                if (!Core.Me.HasAura(779))
                {
                    AvoidanceManager.RemoveAllAvoids(i=> i.CanRun);
                }
            }
            

            
            await Coroutine.Yield();
            return false;
        }
    }
}