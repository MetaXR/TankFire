using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model
{
	[ObjectEvent]
	public class DBCacheComponentEvent : ObjectEvent<DBCacheComponent>, IAwake
	{
		public void Awake()
		{
			this.Get().Awake();
		}
	}

	/// <summary>
	/// 用来缓存数据
	/// </summary>
	public class DBCacheComponent : Component
	{
		public Dictionary<string, Dictionary<long, Disposer>> cache = new Dictionary<string, Dictionary<long, Disposer>>();

		public const int taskCount = 32;
		public List<DBTaskQueue> tasks = new List<DBTaskQueue>(taskCount);

		public void Awake()
		{
			for (int i = 0; i < taskCount; ++i)
			{
				DBTaskQueue taskQueue = new DBTaskQueue();
				this.tasks.Add(taskQueue);
				taskQueue.Start();
			}
		}

		public Task<bool> Add(Disposer entity, string collectionName = "")
		{
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

			this.AddToCache(entity, collectionName);

			if (collectionName == "")
			{
				collectionName = entity.GetType().Name;
			}
            Log.Info(collectionName + "------DBCacheComponent");
            DBSaveTask task = new DBSaveTask(entity, collectionName, tcs);
            this.tasks[(int)((ulong)task.Id % taskCount)].Add(task);

            return tcs.Task;
		}

		public Task<bool> AddBatch(List<Disposer> entitys, string collectionName)
		{
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
			DBSaveBatchTask task = new DBSaveBatchTask(entitys, collectionName, tcs);
			this.tasks[(int)((ulong)task.Id % taskCount)].Add(task);
			return tcs.Task;
		}

		public void AddToCache(Disposer entity, string collectionName = "")
		{
			if (collectionName == "")
			{
				collectionName = entity.GetType().Name;
			}
			Dictionary<long, Disposer> collection;
			if (!this.cache.TryGetValue(collectionName, out collection))
			{
				collection = new Dictionary<long, Disposer>();
				this.cache.Add(collectionName, collection);
			}
			collection[entity.Id] = entity;
		}

		public Disposer GetFromCache(string collectionName, long id)
		{
			Dictionary<long, Disposer> d;
			if (!this.cache.TryGetValue(collectionName, out d))
			{
				return null;
			}
            Disposer result;
			if (!d.TryGetValue(id, out result))
			{
				return null;
			}
			return result;
		}

		public void RemoveFromCache(string collectionName, long id)
		{
			Dictionary<long, Disposer> d;
			if (!this.cache.TryGetValue(collectionName, out d))
			{
				return;
			}
			d.Remove(id);
		}

		public Task<Disposer> Get(string collectionName, long id)
		{
            Disposer entity = GetFromCache(collectionName, id);
			if (entity != null)
			{
				return Task.FromResult(entity);
			}

			TaskCompletionSource<Disposer> tcs = new TaskCompletionSource<Disposer>();
			this.tasks[(int)((ulong)id % taskCount)].Add(new DBQueryTask(id, collectionName, tcs));

			return tcs.Task;
		}

		public Task<List<Disposer>> GetBatch(string collectionName, List<long> idList)
		{
			List <Disposer> entitys = new List<Disposer>();
			bool isAllInCache = true;
			foreach (long id in idList)
			{
                Disposer entity = this.GetFromCache(collectionName, id);
				if (entity == null)
				{
					isAllInCache = false;
					break;
				}
				entitys.Add(entity);
			}

			if (isAllInCache)
			{
				return Task.FromResult(entitys);
			}

			TaskCompletionSource<List<Disposer>> tcs = new TaskCompletionSource<List<Disposer>>();
			DBQueryBatchTask dbQueryBatchTask = new DBQueryBatchTask(idList, collectionName, tcs);
			this.tasks[(int)((ulong)dbQueryBatchTask.Id % taskCount)].Add(dbQueryBatchTask);

			return tcs.Task;
		}

		public Task<List<Disposer>> GetJson(string collectionName, string json)
		{
			TaskCompletionSource<List<Disposer>> tcs = new TaskCompletionSource<List<Disposer>>();
			
			DBQueryJsonTask dbQueryJsonTask = new DBQueryJsonTask(collectionName, json, tcs);
			this.tasks[(int)((ulong)dbQueryJsonTask.Id % taskCount)].Add(dbQueryJsonTask);

			return tcs.Task;
		}
	}
}