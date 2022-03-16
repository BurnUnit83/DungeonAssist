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
	public class AurumVale
	{

		public static async Task<bool> Run()
		{
				//Lockhart
				if (GameObjectManager.GetObjectByNPCId(1534) != null && Core.Me.HasAura(302) && Core.Me.GetAuraById(302).Value > 2) //Gold Lung
				{
					var npc = GameObjectManager.GetObjectsByNPCIds<GameObject>(new uint[] {2002647, 2002648, 2002649, 2000778}).OrderBy(i => i.Distance()).FirstOrDefault();

					if (npc != default(GameObject))
					{
						Logging.Write("We have aura, moving to Morbol Fruit.");
						if (await Navigation.OffMeshMoveInteract(npc))
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
				}
				
				//Miser's Mistress 
				if (GameObjectManager.GetObjectByNPCId(1532) != null && Core.Me.HasAura(303) && Core.Me.GetAuraById(303).Value > 2) //Burrs
				{
					var npc = GameObjectManager.GetObjectsByNPCIds<GameObject>(new uint[] {2002654, 2002655, 2002656, 2002657, 2002658, 2002659, 2002660, 2002661, 2002662, 2002663 }).OrderBy(i => i.Distance()).FirstOrDefault();

					if (npc != default(GameObject))
					{
						Logging.Write("We have aura, moving to Morbol Fruit.");
						if (await Navigation.OffMeshMoveInteract(npc))
						{
							npc.Interact();
							await Coroutine.Wait(10000, () => !Core.Me.HasAura(303));
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
				}				
			

			await Coroutine.Yield();
			return false;
		}
	}
}
