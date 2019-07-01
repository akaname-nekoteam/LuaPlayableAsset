/*****************************************************************************
*	$Id: $
*****************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public sealed class LuaEngine : UnityEngine.MonoBehaviour
{
	static public XLua.LuaEnv LuaEnv
	{
		get
		{
			if( luaEnv == null)
			{
				luaEnv = new XLua.LuaEnv();
			}
			return luaEnv;
		}
	}
	static protected XLua.LuaEnv luaEnv = null;
	//----------------------------------------------------------------------------
	private void Update()
	{
		luaEnv?.Tick();
	}
} // class LuaEngine


