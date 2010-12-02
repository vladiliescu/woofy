using System.Collections.Generic;
using System.Text;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public class WriteMetaToTextExpression : BaseExpression
    {
        private readonly IFileProxy file;
        private readonly IPathRepository pathRepository;

        public WriteMetaToTextExpression(IAppLog appLog, IFileProxy file, IPathRepository pathRepository) : base(appLog)
        {
            this.file = file;
            this.pathRepository = pathRepository;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            var metadataBuilder = new StringBuilder();

            metadataBuilder.AppendLine("=====================");
            foreach (var entry in context.Metadata)
                metadataBuilder.AppendFormat("{0}:{1}\n", entry.Key, entry.Value);

            var path = pathRepository.DownloadPathFor(context.ComicId, context.ComicId + ".txt");
            file.AppendAllText(path, metadataBuilder.ToString());

            return null;
        }

        protected override string ExpressionName
        {
            get { return Expressions.WriteMetaToText; }
        }
    }
}