using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    [SugarTable("Test001")]
    public class Test001
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, IndexGroupNameList = new string[] { "AA", "BB" })]//通过特性设置主键和自增列           
        public int AA { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(255)", IsNullable = true, DefaultValue = "")]//设置长度、是否可为空、默认值       
        public string ABBA { get; set; }

        [SugarColumn(Length = 18, DecimalDigits = 2)]//设置长度、精度
        public decimal CCCC { get; set; }

    }
}
