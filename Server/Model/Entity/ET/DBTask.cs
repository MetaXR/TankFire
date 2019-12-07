using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Model
{
	public abstract class DBTask : Entity
	{
		protected DBTask()
		{
		}

		protected DBTask(long id): base(id)
		{
		}

		public abstract Task Run();
	}

	public sealed class DBSaveTask : DBTask
	{
		public Disposer Entity;

		public string CollectionName { get; }

		public TaskCompletionSource<bool> Tcs;

		public DBSaveTask(Disposer entity, string collectionName, TaskCompletionSource<bool> tcs) : base(entity.Id)
		{
			this.Entity = entity;
			this.CollectionName = collectionName;
			this.Tcs = tcs;
		}

		public override async Task Run()
		{
			DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();

			try
			{
                // 执行保存数据库任务
                Log.Info(this.CollectionName + "DBTask----保存数据任务");
				await dbComponent.GetCollection(this.CollectionName).ReplaceOneAsync(s => s.Id == this.Entity.Id, this.Entity, new UpdateOptions {IsUpsert = true});
				this.Tcs.SetResult(true);
			}
			catch (Exception e)
			{
				this.Tcs.SetException(new Exception($"保存数据失败!  {CollectionName} {Id}", e));
			}
		}
	}

	public sealed class DBSaveBatchTask : DBTask
	{
		public string CollectionName { get; }

		public List<Disposer> Entitys;

		public TaskCompletionSource<bool> Tcs;
	
		public DBSaveBatchTask(List<Disposer> entitys, string collectionName, TaskCompletionSource<bool> tcs)
		{
			this.Entitys = entitys;
			this.CollectionName = collectionName;
			this.Tcs = tcs;
		}
	
		public override async Task Run()
		{
			DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();

			foreach (Entity entity in this.Entitys)
			{
				if (entity == null)
				{
					continue;
				}

				try
				{
					// 执行保存数据库任务
					await dbComponent.GetCollection(this.CollectionName).ReplaceOneAsync(s => s.Id == entity.Id, entity, new UpdateOptions { IsUpsert = true });
				}
				catch (Exception e)
				{
					Log.Debug($"{entity.GetType().Name} {entity.ToJson()}" + e.ToString());
					this.Tcs.SetException(new Exception($"保存数据失败! {CollectionName} {this.Entitys.ListToString()}", e));
				}
			}
			this.Tcs.SetResult(true);
		}
	}

	public sealed class DBQueryTask : DBTask
	{
		public string CollectionName { get; }

		public TaskCompletionSource<Disposer> Tcs { get; }

		public DBQueryTask(long id, string collectionName, TaskCompletionSource<Disposer> tcs) : base(id)
		{
			this.CollectionName = collectionName;
			this.Tcs = tcs;
		}

		public override async Task Run()
		{
			DBCacheComponent dbCacheComponent = Game.Scene.GetComponent<DBCacheComponent>();
			DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
            // 执行查询前先看看cache中是否已经存在
            Disposer entity = dbCacheComponent.GetFromCache(this.CollectionName, this.Id);
			if (entity != null)
			{
				this.Tcs.SetResult(entity);
				return;
			}
			try
			{
				// 执行查询数据库任务
				entity = await dbComponent.GetCollection(this.CollectionName).FindAsync((s) => s.Id == this.Id).Result.FirstOrDefaultAsync();
				if (entity != null)
				{
					dbCacheComponent.AddToCache(entity);
				}
				this.Tcs.SetResult(entity);
			}
			catch (Exception e)
			{
				this.Tcs.SetException(new Exception($"查询数据库异常! {CollectionName} {Id}", e));
			}
		}
	}

	public sealed class DBQueryBatchTask : DBTask
	{
		public string CollectionName { get; }

		public List<long> IdList { get; }

		public TaskCompletionSource<List<Disposer>> Tcs { get; }

		public DBQueryBatchTask(List<long> list, string collectionName, TaskCompletionSource<List<Disposer>> tcs)
		{
			this.IdList = list;
			this.CollectionName = collectionName;
			this.Tcs = tcs;
		}

		public override async Task Run()
		{
			DBCacheComponent dbCacheComponent = Game.Scene.GetComponent<DBCacheComponent>();
			DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
			List<Disposer> result = new List<Disposer>();

			try
			{
				// 执行查询数据库任务
				foreach (long id in IdList)
				{
                    Disposer entity = dbCacheComponent.GetFromCache(this.CollectionName, id);
					if (entity == null)
					{
						entity = await dbComponent.GetCollection(this.CollectionName).FindAsync((s) => s.Id == id).Result.FirstOrDefaultAsync();
						dbCacheComponent.AddToCache(entity);
					}
					
					if (entity == null)
					{
						continue;
					}
					result.Add(entity);
				}
				
				this.Tcs.SetResult(result);
			}
			catch (Exception e)
			{
				this.Tcs.SetException(new Exception($"查询数据库异常! {this.CollectionName} {IdList.ListToString()}", e));
			}
		}
	}

	public sealed class DBQueryJsonTask : DBTask
	{
		public string CollectionName { get; }

		public string Json { get; }

		public TaskCompletionSource<List<Disposer>> Tcs { get; }

		public DBQueryJsonTask(string collectionName, string json, TaskCompletionSource<List<Disposer>> tcs)
		{
			this.CollectionName = collectionName;
			this.Json = json;
			this.Tcs = tcs;
		}

		public override async Task Run()
		{
			DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
			try
			{
				// 执行查询数据库任务
				FilterDefinition<Disposer> filterDefinition = new JsonFilterDefinition<Disposer>(this.Json);
				List<Disposer> entitys = await dbComponent.GetCollection(this.CollectionName).FindAsync(filterDefinition).Result.ToListAsync();
				this.Tcs.SetResult(entitys);
			}
			catch (Exception e)
			{
				this.Tcs.SetException(new Exception($"查询数据库异常! {CollectionName} {this.Json}", e));
			}
		}
	}
}