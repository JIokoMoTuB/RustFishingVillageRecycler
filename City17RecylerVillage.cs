using UnityEngine;
using System.Linq;

namespace Oxide.Plugins
{
    [Info("City17RecylerVillage", "City 17 Server Recycler Village Plugin", "1.0.0")]
    [Description("Recycler Village Plugin")]

    class City17RecylerVillage : RustPlugin
    {
        #region Vars
        private ulong objectIDRecycler = 10056698;
        private string prefabRecycler = "assets/bundled/prefabs/static/recycler_static.prefab";
        private string[] prefabVillageList = new string[] {
            "assets/bundled/prefabs/autospawn/monument/fishing_village/fishing_village_a.prefab",
            "assets/bundled/prefabs/autospawn/monument/fishing_village/fishing_village_b.prefab",
            "assets/bundled/prefabs/autospawn/monument/fishing_village/fishing_village_c.prefab"
        };
        #endregion
		
        #region Hooks
        private void OnServerInitialized()
        {
            NextTick(() => {
				this.RemoveRecycler();
                foreach (MonumentInfo Recycler in UnityEngine.Object.FindObjectsOfType<MonumentInfo>())
                {
					if(prefabVillageList.Contains(Recycler.name)){
						if(Recycler.name == this.prefabVillageList[0]){
							var NewPisitionRecycler = Recycler.transform.position + Recycler.transform.rotation * new Vector3(-19.045f, 2.059f, -20.737f);
							CrateRecycler(NewPisitionRecycler, Recycler.transform.rotation);
						}
						if(Recycler.name == this.prefabVillageList[1]){
							var NewPisitionRecycler = Recycler.transform.position + Recycler.transform.rotation * new Vector3(-10.197f, 2.087f, 18.888f);
							CrateRecycler(NewPisitionRecycler, Recycler.transform.rotation * new Quaternion(0, 2.0f, 0, 2));
						}
						if(Recycler.name == this.prefabVillageList[2]){
							var NewPisitionRecycler = Recycler.transform.position + Recycler.transform.rotation * new Vector3(-8.854f, 1.918f, 12.474f);
							CrateRecycler(NewPisitionRecycler, Recycler.transform.rotation * new Quaternion(0, 2.0f, 0, 2));
						}
					}
				}
            });
        }
        #endregion

        #region Metods
        private void CrateRecycler(Vector3 position, Quaternion quaternion)
        {
            var Recycler = GameManager.server.CreateEntity(this.prefabRecycler, position, quaternion);
            Recycler.Spawn();
            Recycler.OwnerID = this.objectIDRecycler;
        }

		private void RemoveRecycler(){
			foreach (var SelectRecycler in UnityEngine.Object.FindObjectsOfType<Recycler>().Where(CurrentRecycler => CurrentRecycler.OwnerID == this.objectIDRecycler))
                    SelectRecycler.KillMessage();
		}
        #endregion
    }
}