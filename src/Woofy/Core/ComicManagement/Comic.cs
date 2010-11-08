using System;
using Newtonsoft.Json;
using Woofy.Core.Engine;

namespace Woofy.Core.ComicManagement
{
    public class Comic: IEquatable<Comic>
    {
		/// <summary>
		/// The definition's filename. It uniquely identifies a comic/definition.
		/// </summary>
		public string Id { get; set; }

    	public string Name { get; set; }
    	public int DownloadedStrips { get; set; }
		[Obsolete("No longer used")]
    	public string DownloadFolder { get; set; }
    	public Status Status { get; set; }
    	public Uri CurrentPage { get; set; }

		[JsonIgnore]
		public bool HasFinished { get; set; }
		[JsonIgnore]
    	public Definition Definition { get; private set; }

        public void SetDefinition(Definition definition)
        {
            Definition = definition;
            definition.ComicInstance = this;
        }

        public override string ToString()
        {
            return Name;
        }

        #region Equality
        public bool Equals(Comic other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Definition, Definition);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Comic)) return false;
            return Equals((Comic)obj);
        }

        public override int GetHashCode()
        {
            return (Definition != null ? Definition.GetHashCode() : 0);
        }

        public static bool operator ==(Comic left, Comic right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Comic left, Comic right)
        {
            return !Equals(left, right);
        } 
        #endregion
    }
}
