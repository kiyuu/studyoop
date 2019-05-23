namespace StudyOOP.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InstanceGroupID : IDBase
    {
        public InstanceGroupID(string id, InstanceID[] instanceIds)
            : base(id)
        {
            this.InstanceIds = instanceIds;
        }

        public InstanceID[] InstanceIds { get; }
    }
}
