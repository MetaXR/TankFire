using System.Linq;
using System.Collections.Generic;

namespace Model
{
    public class MatcherComponent : Component
    {
        private readonly Dictionary<long, Matcher> matchers = new Dictionary<long, Matcher>();
        //private readonly Dictionary<long, Matcher> wdxmatchers = new Dictionary<long, Matcher>();
        //private readonly Dictionary<long, Matcher> baozhamatchers = new Dictionary<long, Matcher>();
        //private readonly Dictionary<long, Matcher> dimianmatchers = new Dictionary<long, Matcher>();
        //private readonly Dictionary<long, Matcher> feiyimatchers = new Dictionary<long, Matcher>();
        //private readonly Dictionary<long, Matcher> cksmatchers = new Dictionary<long, Matcher>();
        public int Count { get { return matchers.Count; } }

        public void Add(Matcher matcher)
        {
            this.matchers.Add(matcher.PlayerID, matcher);
            //switch (matcher.roomType)
            //{
            //    case RoomType.WENDING:
            //        this.wdxmatchers.Add(matcher.PlayerID, matcher);
            //        break;
            //    case RoomType.BAOZHA:
            //        this.baozhamatchers.Add(matcher.PlayerID, matcher);
            //        break;
            //    case RoomType.DIMIAN:
            //        this.dimianmatchers.Add(matcher.PlayerID, matcher);
            //        break;
            //    case RoomType.FEIYI:
            //        this.feiyimatchers.Add(matcher.PlayerID, matcher);
            //        break;
            //    case RoomType.CHANGKAISHI:
            //        this.cksmatchers.Add(matcher.PlayerID, matcher);
            //        break;
            //}
        }

        public Matcher Get(long id)
        {
            Matcher matcher;
            this.matchers.TryGetValue(id, out matcher);
            return matcher;
        }

        public Matcher[] GetAll()
        {
            return this.matchers.Values.ToArray();
        }

        //public Matcher[] GetListByType(RoomType type)
        //{
        //    switch (type)
        //    {
        //        case RoomType.WENDING:
        //            return  this.wdxmatchers.Values.ToArray();   
                    
        //        case RoomType.BAOZHA:
        //            return this.baozhamatchers.Values.ToArray();
                
        //        case RoomType.DIMIAN:
        //            return this.dimianmatchers.Values.ToArray();
                   
        //        case RoomType.FEIYI:
        //            return this.feiyimatchers.Values.ToArray();
                    
        //        case RoomType.CHANGKAISHI:
        //            return this.cksmatchers.Values.ToArray();                 
        //    }
        //    return this.matchers.Values.ToArray();
        //}

        public void Remove(long id)
        {
            Matcher matcher = Get(id);
            this.matchers.Remove(id);           
          
            //switch (matcher.roomType)
            //{
            //    case RoomType.WENDING:
            //        this.wdxmatchers.Remove(id);
            //        break;
            //    case RoomType.BAOZHA:
            //        this.baozhamatchers.Remove(id);
            //        break;
            //    case RoomType.DIMIAN:
            //        this.dimianmatchers.Remove(id);
            //        break;
            //    case RoomType.FEIYI:
            //        this.feiyimatchers.Remove(id);
            //        break;
            //    case RoomType.CHANGKAISHI:
            //        this.cksmatchers.Remove(id);
            //        break;
            //}

            matcher?.Dispose();

        }
       
        public override void Dispose()
        {
            if(this.Id == 0)
            {
                return;
            }

            base.Dispose();

            foreach (var matcher in this.matchers.Values)
            {
                matcher.Dispose();
            }

            //foreach (var matcher in this.wdxmatchers.Values)
            //{
            //    matcher.Dispose();
            //}

            //foreach (var matcher in this.baozhamatchers.Values)
            //{
            //    matcher.Dispose();
            //}

            //foreach (var matcher in this.dimianmatchers.Values)
            //{
            //    matcher.Dispose();
            //}

            //foreach (var matcher in this.feiyimatchers.Values)
            //{
            //    matcher.Dispose();
            //}

            //foreach (var matcher in this.cksmatchers.Values)
            //{
            //    matcher.Dispose();
            //}
        }
    }
}
