/*****************************************************************************
*	$Id: $
*****************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LuaPlayableAsset : PlayableAsset
{
	[SerializeField]
	public TextAsset textAsset;

	//----------------------------------------------------------------------------
	public override Playable CreatePlayable(
		UnityEngine.Playables.PlayableGraph graph,
		GameObject owner
	)
	{
		var behaviour = new LuaPlayableBehaviour();
		behaviour.textAsset = textAsset;
		behaviour.owner = owner;
		return ScriptPlayable<LuaPlayableBehaviour>.Create( graph, behaviour);
	}
} // class LuaPlayableAsset


