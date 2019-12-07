namespace Model
{
	// 客户端 100 - 999, 服务端内部1000以上
	public enum Opcode: ushort
	{
		ARequest =1,
		AResponse= 2,
		AActorMessage =3,
		AActorRequest =4,
		AActorResponse =5,
		ActorRequest =6,
		ActorResponse =7,
		ActorRpcRequest =8,
		ActorRpcResponse= 9,
		AFrameMessage =10,
        C2R_HeartMessage=11,
        R2C_HeartMessage=12,
        C2G_HeatKick=13,
        G2C_HeatKick=14,
#region ClientOuterMessage
        FrameMessage= 100,
        Login_RT,
        Login_RE,
        LoginGate_RT,
        LoginGate_RE,
        Register_RT,
        StartMatch_RT,
        StartMatch_RE,
        GetUserInfo_RT,
        GetUserInfo_RE,
        Quit,
        PlayerJoinRoom_RT,
        PlayerJoinRoom_RE,
        PlayerReady,
        Prompt_RT,
        Prompt_RE,
        ChangeGameMode,
        Register_RE,
        R2C_ServerLog,        
		C2G_EnterMap,
		G2C_EnterMap,
		C2M_Reload,
		M2C_Reload,
		C2R_Ping,
		R2C_Ping,
		Frame_ClickMap,		
        Actor_FireTankLife,
        Actor_CreateFireTank,
        C2R_FireEquipStart,
        R2C_FireEquipStart,
        Actor_FrieEquipStart,
        Actor_ClickMap,
#endregion

#region GateServerOuterMessage
        RoomKey= 150,
 #endregion

#region MapServerOuterMessage
        GamerEnter,
        GamerOut,
        GameStart,
        GamerMoneyLess,
        Gameover,
        GamerReconnect,
        GamerReenter,
        Actor_CreateUnits,
        Actor_UnitPos,
#endregion

 #region ETInnerMessage
        // 服务端Opcode, 从1000开始
        G2G_LockRequest= 1000,
		G2G_LockResponse,
		G2G_LockReleaseRequest,
		G2G_LockReleaseResponse,

		M2A_Reload,
		A2M_Reload,

		DBSaveRequest,
		DBSaveResponse,
		DBQueryRequest,
		DBQueryResponse,
		DBSaveBatchResponse,
		DBSaveBatchRequest,
		DBQueryBatchRequest,
		DBQueryBatchResponse,
		DBQueryJsonRequest,
		DBQueryJsonResponse,

		ObjectAddRequest,
		ObjectAddResponse,
		ObjectRemoveRequest,
		ObjectRemoveResponse,
		ObjectLockRequest,
		ObjectLockResponse,
		ObjectUnLockRequest,
		ObjectUnLockResponse,
		ObjectGetRequest,
		ObjectGetResponse,

		R2G_GetLoginKey,
		G2R_GetLoginKey,

		G2M_CreateUnit,
		M2G_CreateUnit,

		M2M_TrasferUnitRequest,
		M2M_TrasferUnitResponse,


 #endregion

        #region RealmServerInnerMessage
        GetLoginKey_RT,
        GetLoginKey_RE,
        #endregion

        #region GateServerInnerMessage
        JoinMatch_RT,
        JoinMatch_RE,
        PlayerDisconnect,
        PlayerQuit,
        #endregion

        #region MatchServerInnerMessage
        CreateRoom_RT,
        CreateRoom_RE,
        GetJoinRoomKey_RT,
        GetJoinRoomKey_RE,
        MatchSuccess,
        PlayerReconnect,
        #endregion

        #region MapServerInnerMessage
        GamerQuitRoom,
        RoomState,
        #endregion

    }
}
