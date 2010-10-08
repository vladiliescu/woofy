namespace Woofy.Core.Engine
{
	public abstract class Definition
	{
        public abstract string Comic { get; }
        public abstract string StartAt { get; }

		protected abstract void RunImpl(Context context);

		/// <summary>
		/// Basically the definition's filename without the extension.
		/// </summary>
		public string Id { get; set; }

		protected Definition()
		{
			Id = GetType().Name.Substring(1);
		}

		public void Run()
		{
			var context = new Context();
			RunImpl(context);
		}
	}
}