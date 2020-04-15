using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Extension
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public string Name { get; set; }
        public TableAttribute(string tableName)
        {
            this.Name = tableName;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
        public bool IsIdentity { get; set; }
        public KeyAttribute(bool isidentity)
        {
            IsIdentity = isidentity;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ComputedAttribute : Attribute
    {

    }
}
