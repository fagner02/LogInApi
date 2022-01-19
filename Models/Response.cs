using System.Collections.Generic;
using X.PagedList;

namespace LogInApi.Models {
    public class Response<T> {
        public Response(IPagedList page, IEnumerable<T> data) {
            PageNumber = page.PageNumber;
            PageSize = page.PageSize;
            TotalItems = page.TotalItemCount;
            TotalPages = page.PageCount;
            Data = data;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}