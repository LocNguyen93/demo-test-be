using System;

namespace Ext.Shared.Models
{
    public class PagingParamModel
    {
        private int? page;
        private int? size;

        public int Page
        {
            get => !page.HasValue || page.Value <= 0 ? 1 : page.Value;
            set => page = value;
        }
        public int Size
        {
            get => !size.HasValue || size.Value <= 0 || size.Value > 100 ? 10 : size.Value;
            set => size = value;
        }

        public PagingParamModel()
        {
        }
        public PagingParamModel(int? page = 1, int? size = 10)
        {
            this.page = page;
            this.size = size;
        }
    }


    public class PagingSortingParamModel : PagingParamModel
    {
        private string sortDirection;

        public string SortField { get; set; }
        public string SortDirection
        {
            get {
                if (sortDirection == null)
                    return OrderByDirection.ASC.ToString();
                else
                    return Enum.TryParse(sortDirection.ToUpper(), out OrderByDirection val) ? val.ToString() : OrderByDirection.ASC.ToString();
            }
            set => sortDirection = value;
        }

        public PagingSortingParamModel()
            : base()
        {
        }
        public PagingSortingParamModel(int? pageNumber, int? pageSize, string sortField, string sortDirection)
            : base(pageNumber, pageSize)
        {
            SortField = sortField;
            SortDirection = sortDirection;
        }
    }

    public enum OrderByDirection
    {
        ASC,
        DESC
    }
}
