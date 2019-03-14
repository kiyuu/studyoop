namespace StudyOOP.Common
{
    public static class InstanceGroupIDs
    {
        public static InstanceGroupID FlatFileToTsvConverters =>
            new InstanceGroupID(
                nameof(FlatFileToTsvConverters),
                new InstanceID[] { InstanceIDs.EmployeeConverter, InstanceIDs.ItemConverter });
    }
}
