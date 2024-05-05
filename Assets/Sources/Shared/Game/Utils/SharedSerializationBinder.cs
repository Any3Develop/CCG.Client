using System;
using Newtonsoft.Json.Serialization;

namespace Shared.Game.Utils
{
	public interface ISharedSerializationBinder : ISerializationBinder
	{
		string DefaultAssemblyName { get; }
		string TargetAssemblyName { get; }
	}

	public class SharedSerializationBinder : ISharedSerializationBinder
	{
		public string DefaultAssemblyName => "mscorlib";
		public string TargetAssemblyName => "Shared";

		public Type BindToType(string assemblyName, string typeName)
		{
			if (assemblyName != TargetAssemblyName)
				assemblyName = DefaultAssemblyName;

			return Type.GetType($"{typeName}, {assemblyName}", true);
		}

		public void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = serializedType.Assembly.FullName;
			typeName = serializedType.FullName;
		}
	}
}