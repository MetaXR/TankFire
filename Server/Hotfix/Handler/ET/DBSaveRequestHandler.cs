using System;
using Model;

namespace Hotfix
{
	[MessageHandler(AppType.DB)]
	public class DBSaveRequestHandler : AMRpcHandler<DBSaveRequest, DBSaveResponse>
	{
		protected override async void Run(Session session, DBSaveRequest message, Action<DBSaveResponse> reply)
		{
			DBSaveResponse response = new DBSaveResponse();
			try
			{
                Log.Info(message.CollectionName+ "------DBSaveRequestHandler");
				DBCacheComponent dbCacheComponent = Game.Scene.GetComponent<DBCacheComponent>();
				if (message.CollectionName == "")
				{
					message.CollectionName = message.Disposer.GetType().Name;
                    Log.Info(message.CollectionName + "------DBSaveRequestHandler");
                }

				if (message.NeedCache)
				{
					dbCacheComponent.AddToCache(message.Disposer, message.CollectionName);
				}
				await dbCacheComponent.Add(message.Disposer, message.CollectionName);
                Log.Info(response.Message + "------DBSaveRequestHandler");
                reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}