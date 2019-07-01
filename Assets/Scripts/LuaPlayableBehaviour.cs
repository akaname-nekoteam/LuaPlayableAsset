/*****************************************************************************
*	$Id: $
*****************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LuaPlayableBehaviour : PlayableBehaviour
{
	public TextAsset textAsset{ get; set; }
	public GameObject owner{ get; set; }

	private XLua.LuaTable scriptEnv = null;
	private Action luaUpdate;
	private Action luaOnDestroy;
	//----------------------------------------------------------------------------
	// PlayState.Pausedに変更されたときに呼び出される
	public override void OnBehaviourPause(
		UnityEngine.Playables.Playable playable,
		UnityEngine.Playables.FrameData info
	)
	{
		luaOnDestroy?.Invoke();
		luaOnDestroy = null;

		scriptEnv?.Dispose();
		scriptEnv = null;
		luaUpdate = null;
	}
	//----------------------------------------------------------------------------
	// PlayState.Playingに変更されたときに呼び出される
	public override void OnBehaviourPlay(
		UnityEngine.Playables.Playable playable,
		UnityEngine.Playables.FrameData info
	)
	{
		if( textAsset != null)
		{
			scriptEnv = LuaEngine.LuaEnv.NewTable();
			// スクリプトごとに別々の環境を設定すると、スクリプト間でのグローバル変数と関数の競合をある程度防ぐことができます。
			XLua.LuaTable meta = LuaEngine.LuaEnv.NewTable();
			meta.Set( "__index", LuaEngine.LuaEnv.Global);
			scriptEnv.SetMetaTable( meta);
			meta.Dispose();

			scriptEnv.Set( "self", owner);
			LuaEngine.LuaEnv.DoString( textAsset.text, textAsset.GetHashCode().ToString(), scriptEnv);

			Action luaAwake = scriptEnv.Get<Action>( "awake");
			scriptEnv.Get( "update", out luaUpdate);
			scriptEnv.Get( "ondestroy", out luaOnDestroy);

			luaAwake?.Invoke();
		}
	}
	//----------------------------------------------------------------------------
	// PlayableGraphのProcessFrameフェーズの間に呼び出される
	// ProcessFrameはあなたのPlayableがその仕事をする段階です。
	// PlayableがPlayPlayableOutputを直接または間接的に再生して接続するときに、各フレームに対して呼び出されます。
	public override void ProcessFrame(
		UnityEngine.Playables.Playable playable,
		UnityEngine.Playables.FrameData info,
		object playerData
	)
	{
		if( Application.isPlaying)
		{
			luaUpdate?.Invoke();
		}
	}
} // class LuaPlayableBehaviour

