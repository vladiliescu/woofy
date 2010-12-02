using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public class WriteMetaToXmpExpression : BaseExpression
    {
        private readonly IAppSettings appSettings;

        public WriteMetaToXmpExpression(IAppLog appLog, IAppSettings appSettings) : base(appLog)
        {
            this.appSettings = appSettings;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            var metaBuilder = new StringBuilder();
            AddIfPossible("title", metaBuilder, context);
            AddIfPossible("description", metaBuilder, context);
            AddIfPossible("source", metaBuilder, context.CurrentAddress.AbsoluteUri);

            if (metaBuilder.Length == 0)
            {
                Warn(context, "haven't found neither the title nor the description metadata");
                return null;
            }

            foreach (var downloadedFile in context.DownloadedFiles)
            {
                var arguments = @"{0} ""{1}""".FormatTo(metaBuilder.ToString(), downloadedFile);
                Log(context, "running exiftool with {0}", arguments);

                var run = new ProcessStartInfo(appSettings.ExifToolPath, arguments)
                              { CreateNoWindow = true, RedirectStandardOutput = true, UseShellExecute = false };

                var process = Process.Start(run);
                process.WaitForExit();
                Log(context, process.StandardOutput.ReadToEnd());
            }

            return null;
        }

        private void AddIfPossible(string tag, StringBuilder metaBuilder, Context context)
        {
            if (!context.Metadata.ContainsKey(tag))
                return;

            metaBuilder.AppendFormat(@" -xmp:{0}=""{1}""", tag, context.Metadata[tag]);
        }

        private void AddIfPossible(string tag, StringBuilder metaBuilder, string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            metaBuilder.AppendFormat(@" -xmp:{0}=""{1}""", tag, value);
        }

        protected override string ExpressionName
        {
            get { return Expressions.WriteMetaToXmp; }
        }
    }
}