﻿using System.Linq;
using NetModular.Lib.Data.Abstractions.Pagination;

namespace NetModular.Lib.Data.Query
{
    public static class QueryModelExtensions
    {
        /// <summary>
        /// 获取Paging分页类
        /// </summary>
        public static Paging Paging(this QueryModel model)
        {
            var paging = new Paging(model.Page.Index, model.Page.Size);
            paging.QueryCount = model.QueryCount;

            if (model.Page.Sort != null && model.Page.Sort.Any())
            {
                foreach (var sort in model.Page.Sort)
                {
                    paging.OrderBy.Add(new Sort(sort.Field, sort.Type));
                }
            }

            return paging;
        }
    }
}
