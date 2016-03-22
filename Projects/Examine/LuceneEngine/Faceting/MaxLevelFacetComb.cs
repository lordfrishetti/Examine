﻿using System.Collections.Generic;
using System.Linq;

namespace Examine.LuceneEngine.Faceting
{
    /// <summary>
    /// Keeps the FacetLevel with the highest level if FacetLevels with the same FacetId appears more than once for a document
    /// </summary>
    internal class MaxLevelFacetComb : IFacetComb
    {
        //Reused.
        Dictionary<int, FacetLevel> _levels = new Dictionary<int, FacetLevel>();
        public FacetLevel[] Comb(FacetLevel[] levels)
        {
            _levels.Clear();
            bool changes = false;
            for (int i = 0, n = levels.Length; i < n; i++)
            {
                var id = levels[i].FacetId;
                FacetLevel fl;
                if (_levels.TryGetValue(id, out fl))
                {
                    changes = true;
                    if (fl.Level < levels[i].Level)
                    {
                        _levels[id] = levels[i];
                    }
                }
                else
                {
                    _levels.Add(id, levels[i]);
                }
            }
            //If no dupplicate facets was found return the original array.
            return changes ? _levels.Values.ToArray() : levels;
        }
    }
}