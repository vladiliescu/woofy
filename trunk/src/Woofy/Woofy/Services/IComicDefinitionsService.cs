using System;
using System.Collections.Generic;
using System.Text;

using Woofy.Entities;

namespace Woofy.Services
{
    public interface IComicDefinitionsService
    {
        /// <summary>
        /// Parses the comic definitions, building comic entities from them.
        /// </summary>
        /// <returns>A collection of comic entities extracted from the comic definitions.</returns>
        ComicCollection BuildComicsFromDefinitions();        
    }
}
