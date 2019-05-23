namespace StudyOOP.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InstanceID : IDBase
    {
        public InstanceID(string id, string className)
            : base(id)
        {
            this.ClassName = className;
        }

        public string ClassName { get; }
    }
}
