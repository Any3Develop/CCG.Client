using System;

namespace Client.Common.Services.SceneService
{
	[Flags]
	public enum SceneId
	{
		Startup = 0,
		Server = 1,
		Client = 2,
	}
}