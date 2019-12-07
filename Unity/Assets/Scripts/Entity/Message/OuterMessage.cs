using System.Collections.Generic;
using ProtoBuf;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Model
{
	[BsonKnownTypes(typeof(AActorMessage))]
	public abstract partial class AMessage
	{
	}

	[BsonKnownTypes(typeof(AActorRequest))]
	[ProtoInclude((int)Opcode.Login_RT, typeof(Login_RT))]
	[ProtoInclude((int)Opcode.LoginGate_RT, typeof(LoginGate_RT))]
	[ProtoInclude((int)Opcode.C2G_EnterMap, typeof(C2G_EnterMap))]
	[ProtoInclude((int)Opcode.C2R_Ping, typeof(C2R_Ping))]
	public abstract partial class ARequest : AMessage
	{
	}

	[BsonKnownTypes(typeof(AActorResponse))]
	[ProtoInclude((int)Opcode.Login_RE, typeof(Login_RE))]
	[ProtoInclude((int)Opcode.LoginGate_RE, typeof(LoginGate_RE))]
	[ProtoInclude((int)Opcode.G2C_EnterMap, typeof(G2C_EnterMap))]
	[ProtoInclude((int)Opcode.R2C_Ping, typeof(R2C_Ping))]
	public abstract partial class AResponse : AMessage
	{
	}
	
    [BsonKnownTypes(typeof(Actor_UnitPos))]
	[BsonKnownTypes(typeof(AFrameMessage))]
	[BsonKnownTypes(typeof(Actor_CreateUnits))]
	[BsonKnownTypes(typeof(FrameMessage))]
    [BsonKnownTypes(typeof(GamerEnter))]
    [BsonKnownTypes(typeof(PlayerReady))]
    [BsonKnownTypes(typeof(Actor_CreateFireTank))]
    [BsonKnownTypes(typeof(Actor_FrieEquipStart))]
    [BsonKnownTypes(typeof(Actor_FireTankLife))]
    [BsonKnownTypes(typeof(GameStart))]
    [BsonKnownTypes(typeof(Gameover))]
    [BsonKnownTypes(typeof(GamerReenter))]
    [BsonKnownTypes(typeof(GamerReconnect))]
    [BsonKnownTypes(typeof(GamerOut))]    
    [ProtoInclude((int)Opcode.FrameMessage, typeof(FrameMessage))]
	[ProtoInclude((int)Opcode.AFrameMessage, typeof(AFrameMessage))]
	[ProtoInclude((int)Opcode.Actor_CreateUnits, typeof(Actor_CreateUnits))]
    [ProtoInclude((int)Opcode.Actor_UnitPos, typeof(Actor_UnitPos))]
    [ProtoInclude((int)Opcode.GamerEnter, typeof(GamerEnter))]
    [ProtoInclude((int)Opcode.PlayerReady, typeof(PlayerReady))]
    [ProtoInclude((int)Opcode.Actor_CreateFireTank, typeof(Actor_CreateFireTank))]
    [ProtoInclude((int)Opcode.Actor_FrieEquipStart, typeof(Actor_FrieEquipStart))]
    [ProtoInclude((int)Opcode.Actor_FireTankLife, typeof(Actor_FireTankLife))]
    [ProtoInclude((int)Opcode.GameStart, typeof(GameStart))]
    [ProtoInclude((int)Opcode.Gameover, typeof(Gameover))]
    [ProtoInclude((int)Opcode.GamerReenter, typeof(GamerReenter))]
    [ProtoInclude((int)Opcode.GamerReconnect, typeof(GamerReconnect))]
    [ProtoInclude((int)Opcode.GamerOut, typeof(GamerOut))]
    public abstract partial class AActorMessage : AMessage
	{
	}
    [BsonKnownTypes(typeof(PlayerJoinRoom_RT))]
    public abstract partial class AActorRequest : ARequest
	{
	}
    [BsonKnownTypes(typeof(PlayerJoinRoom_RE))]
    public abstract partial class AActorResponse : AResponse
	{
	}

	/// <summary>
	/// 帧消息，继承这个类的消息会经过服务端转发
	/// </summary>
	[ProtoInclude((int)Opcode.Frame_ClickMap, typeof(Frame_ClickMap))]
	[BsonKnownTypes(typeof(Frame_ClickMap))]
	public abstract partial class AFrameMessage : AActorMessage
	{
	}

    [ProtoContract]
    [Message(Opcode.C2R_HeartMessage)]
    public class C2R_HeartMessage : ARequest
    {
        [ProtoMember(1)]
        public string heartMessage;
    }

    [ProtoContract]
    [Message(Opcode.R2C_HeartMessage)]
    public class R2C_HeartMessage : AResponse
    {
        [ProtoMember(1)]
        public string heartMessage;
    }

    [ProtoContract]
    [Message(Opcode.C2G_HeatKick)]
    public class C2G_HeatKick : ARequest
    {
        [ProtoMember(1)]
        public string heartMessage;
    }

    [ProtoContract]
    [Message(Opcode.G2C_HeatKick)]
    public class G2C_HeatKick : AResponse
    {
        [ProtoMember(1)]
        public string heartMessage;
    }

    [ProtoContract]
    [Message(Opcode.Login_RT)]
    public class Login_RT : ARequest
    {
        [ProtoMember(1)]
        public string Account;
        [ProtoMember(2)]
        public string Password;
    }
    [ProtoContract]
    [Message(Opcode.Login_RE)]
    public class Login_RE : AResponse
    {
        [ProtoMember(1)]
        public long Key;
        [ProtoMember(2)]
        public string Address;
    }

    [ProtoContract]
    [Message(Opcode.Register_RT)]
    public class Register_RT : ARequest
    {
        [ProtoMember(1)]
        public string Account;
        [ProtoMember(2)]
        public string Password;
    }

    [ProtoContract]
    [Message(Opcode.Register_RE)]
    public class Register_RE : AResponse
    {
       
    }


    [ProtoContract]
	[Message(Opcode.LoginGate_RT)]
	public class LoginGate_RT : ARequest
	{
		[ProtoMember(1)]
		public long Key;
        [ProtoMember(2)]
        public int reconnect;
	}

	[ProtoContract]
	[Message(Opcode.LoginGate_RE)]
	public class LoginGate_RE : AResponse
    {
        [ProtoMember(1)]
        public long PlayerID;
        [ProtoMember(2)]
        public long UserID;
    }
    [ProtoContract]
    [Message(Opcode.StartMatch_RT)]
    public class StartMatch_RT : ARequest
    {
        public long PlayerID;
        public RoomLevel Level;
        public RoomType roomType;
    }
    [ProtoContract]
    [Message(Opcode.StartMatch_RE)]
    public class StartMatch_RE : AResponse
    {

    }
    [ProtoContract]
    [Message(Opcode.GetUserInfo_RT)]
    public class GetUserInfo_RT : ARequest
    {
        public long UserID;
    }
    [ProtoContract]
    [Message(Opcode.GetUserInfo_RE)]
    public class GetUserInfo_RE : AResponse
    {
        public string NickName;
        public int Wins;
        public int Loses;
        public long Money;
    }

    [ProtoContract]
    [Message(Opcode.Quit)]
    public class Quit : AMessage
    {
        public long PlayerID;
    }
    [ProtoContract]
    [Message(Opcode.PlayerJoinRoom_RT)]
    public class PlayerJoinRoom_RT : AActorRequest
    {
        public long Key;
    }
    [ProtoContract]
    [Message(Opcode.PlayerJoinRoom_RE)]
    public class PlayerJoinRoom_RE : AActorResponse
    {

    }
    [ProtoContract]
    [Message(Opcode.PlayerReady)]
    public class PlayerReady : AActorMessage
    {
        public long PlayerID;
    }
[ProtoContract]
    [Message(Opcode.Prompt_RT)]
    public class Prompt_RT : AActorRequest
    {
        public long PlayerID;
    }
[ProtoContract]
    [Message(Opcode.Prompt_RE)]
    public class Prompt_RE : AActorResponse
    {
        //public Card[] Cards;
    }
[ProtoContract]
    [Message(Opcode.ChangeGameMode)]
    public class ChangeGameMode : AActorMessage
    {
        public long PlayerID;
    }  
	
    [ProtoContract]
    [Message(Opcode.C2R_FireEquipStart)]
    public class C2R_FireEquipStart : ARequest
    {
        [ProtoMember(1)]
        public long unitID;
        [ProtoMember(2)]
        public int equipType;
        [ProtoMember(3)]
        public float volume;
        [ProtoMember(4)]
        public float vectory;
    }

    [ProtoContract]
    [Message(Opcode.R2C_FireEquipStart)]
    public class R2C_FireEquipStart : AResponse
    {
       
    }
       
	[ProtoContract]
	[Message(Opcode.C2G_EnterMap)]
	public class C2G_EnterMap: ARequest
	{
	}

	[ProtoContract]
	[Message(Opcode.G2C_EnterMap)]
	public class G2C_EnterMap: AResponse
	{
		[ProtoMember(1)]
		public long UnitId;
		[ProtoMember(2)]
		public int Count;
	}

	[ProtoContract]
	public class UnitInfo
	{
		[ProtoMember(1)]
		public long UnitId;
		[ProtoMember(2)]
		public int X;
		[ProtoMember(3)]
		public int Z;
        [ProtoMember(4)]
        public long PlayerId;
    }

	[ProtoContract]
	[Message(Opcode.Actor_CreateUnits)]
	public class Actor_CreateUnits : AActorMessage
	{
		[ProtoMember(1)]
		public List<UnitInfo> Units = new List<UnitInfo>();
	}
    [ProtoContract]
    public class FireTankInfo
    {
        [ProtoMember(1)]
        public long UnitId;
        [ProtoMember(2)]
        public float Life;
        [ProtoMember(3)]
        public float Loss;
        [ProtoMember(4)]
        public TankState state;
    }
    [ProtoContract]
    [Message(Opcode.Actor_CreateFireTank)]
    public class Actor_CreateFireTank : AActorMessage
    {
        [ProtoMember(1)]
        public FireTankInfo TankInfo;
    }
    [ProtoContract]
    public class EquipInfo
    {
        [ProtoMember(1)]
        public long unitID;
        [ProtoMember(2)]
        public int equipType;
        [ProtoMember(3)]
        public float volume;
        [ProtoMember(4)]
        public float vectory;
    }
    [ProtoContract]
    [Message(Opcode.Actor_FrieEquipStart)]
    public class Actor_FrieEquipStart : AActorMessage
    {
        [ProtoMember(1)]
        public EquipInfo MyEquipInfo;
    }

    [ProtoContract]
    [Message(Opcode.Actor_FireTankLife)]
    public class Actor_FireTankLife : AActorMessage
    {
        [ProtoMember(1)]
        public float tankLife;
        [ProtoMember(2)]
        public float tankLoss;
    }

    public struct FrameMessageInfo
	{
		public long Id;
		public AMessage Message;
	}

	[ProtoContract]
	[Message(Opcode.FrameMessage)]
	public class FrameMessage : AActorMessage
	{
		[ProtoMember(1)]
		public int Frame;
		[ProtoMember(2)]
		public List<AFrameMessage> Messages = new List<AFrameMessage>();
	}

	[ProtoContract]
	[Message(Opcode.Frame_ClickMap)]
	public class Frame_ClickMap: AFrameMessage
	{
		[ProtoMember(1)]
		public int X;
		[ProtoMember(2)]
		public int Z;
	}
    
    [ProtoContract]
    [Message(Opcode.Actor_ClickMap)]
    public class Actor_ClickMap : AActorMessage
    {
        [ProtoMember(1)]
        public int X;
        [ProtoMember(2)]
        public int Z;
        [ProtoMember(3)]
        public long Id;
    }

    #region GateServerOuterMessage
    [ProtoContract]
    [Message(Opcode.RoomKey)]
    public class RoomKey : AActorMessage
    {
        public long Key;
    }
    #endregion

    #region MapServerOuterMessage
    [ProtoContract]
    public class GamerInfo
    {
        public long PlayerID;
        public long UserID;
        public bool IsReady;
        public UnitInfo unitInfo;
    }
    [ProtoContract]
    [Message(Opcode.GamerEnter)]
    public class GamerEnter : AActorMessage
    {
        public long RoomID;
        public GamerInfo[] GamersInfo;
    }
    [ProtoContract]
    [Message(Opcode.GamerOut)]
    public class GamerOut : AActorMessage
    {
        public long PlayerID;
    }
    [ProtoContract]
    [Message(Opcode.GameStart)]
    public class GameStart : AActorMessage
    {
       
    }
    [ProtoContract]
    [Message(Opcode.GamerMoneyLess)]
    public class GamerMoneyLess : AActorMessage
    {
        public long PlayerID;
    }
    [ProtoContract]
    [Message(Opcode.Gameover)]
    public class Gameover : AActorMessage
    {
        //public Identity Winner;
        //public long BasePointPerMatch;
        //public int Multiples;
        //[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        //public Dictionary<long, long> GamersScore;
    }
    [ProtoContract]
    [Message(Opcode.GamerReenter)]
    public class GamerReenter : AActorMessage
    {
        public long PastID;
        public long NewID;
    }
    [ProtoContract]
    [Message(Opcode.GamerReconnect)]
    public class GamerReconnect : AActorMessage
    {
        public long PlayerID;
        public int Multiples;
        //[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        //public Dictionary<long, Identity> GamersIdentity;
        //public Card[] LordCards;
        //public KeyValuePair<long, Card[]> DeskCards;
    }   
   
    [ProtoContract]
    [Message(Opcode.Actor_UnitPos)]
    public class Actor_UnitPos : AActorMessage
    {
        [ProtoMember(1)]
        public long UserId;
        [ProtoMember(2)]
        public long UnitId;
        [ProtoMember(3)]
        public int X;
        [ProtoMember(4)]
        public int Z;
        [ProtoMember(5)]
        public int udtilt;
    }
    #endregion
    [ProtoContract]
    [Message(Opcode.C2M_Reload)]
	public class C2M_Reload: ARequest
	{
		public AppType AppType;
	}
    [ProtoContract]
    [Message(Opcode.M2C_Reload)]
	public class M2C_Reload: AResponse
	{
	}

	[ProtoContract]
	[Message(Opcode.C2R_Ping)]
	public class C2R_Ping: ARequest
	{
	}

	[ProtoContract]
	[Message(Opcode.R2C_Ping)]
	public class R2C_Ping: AResponse
	{
	}
}