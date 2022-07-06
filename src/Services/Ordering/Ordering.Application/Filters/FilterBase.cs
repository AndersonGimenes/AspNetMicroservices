using Ordering.Domain.Common;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ordering.Application.Filters
{
    public class FilterBase<T> where T : EntityBase
    {
        public Expression<Func<T, bool>> Predicate { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }
        public bool DisableTracking { get; set; }
    }
}
