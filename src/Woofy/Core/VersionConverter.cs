using System;
using Newtonsoft.Json;

namespace Woofy.Core
{
	public class VersionConverter : JsonConverter
	{
		private static readonly Type SupportedType = typeof(Version);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(((Version)value).ToString(4));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.Value == null)
				return null;

			var tokens = ((string)reader.Value).Split('.');
			if (tokens.Length != 4)
				return new Version();

			return new Version(tokens[0].ParseAs<int>(), tokens[1].ParseAs<int>(), tokens[2].ParseAs<int>(), tokens[3].ParseAs<int>());
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == SupportedType;
		}
	}
}