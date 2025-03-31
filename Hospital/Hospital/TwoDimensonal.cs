using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class TwoDimensonal
    {
        public string ColumnName { get; set; }
        public string Extra { get; set; }

        public TwoDimensonal(string columnName, string extra)
        {
            ColumnName = columnName;
            Extra = extra;
        }
    }
}
