using UnityEngine;

namespace Model
{
    public static class UnitFactory
    {
        public static Unit Create(long playerID,long userID)
        {
            //UnitComponent unitComponent = Game.Scene.GetComponent<UnitComponent>();
            GameObject prefab = ((GameObject) Resources.Load("KV")).Get<GameObject>("Person");
            
	        Unit unit = EntityFactory.CreateWithId<Unit,long>(playerID, userID);
            unit.UserId = userID;
	        unit.GameObject = UnityEngine.Object.Instantiate(prefab);
	        GameObject parent = GameObject.Find($"/Global/Unit");
	        unit.GameObject.transform.SetParent(parent.transform, false);            
			unit.AddComponent<AnimatorComponent>();
	        unit.AddComponent<MoveComponent>();            
            //unitComponent.Add(unit);
            return unit;
        }
    }
}