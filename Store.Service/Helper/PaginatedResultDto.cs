using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Helper
{
    public class PaginatedResultDto<T>
    {
        public PaginatedResultDto(int? pageIndex, int? pageSize, int? totalCount, IReadOnlyList<T> date)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Date = date;
        }

        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }

        public int? TotalCount { get; set; }

        public IReadOnlyList<T> Date { get; set; }
    }
}
