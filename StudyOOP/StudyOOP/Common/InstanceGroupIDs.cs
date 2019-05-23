namespace StudyOOP.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class InstanceGroupIDs
    {
        public static InstanceGroupID FlatFileToTsvConverters =>
            new InstanceGroupID(
                nameof(FlatFileToTsvConverters),
                new InstanceID[] { InstanceIDs.EmployeeConverter, InstanceIDs.ItemConverter });
    }
}
