namespace StudyOOP.Common
{
    using StudyOOP.Convert;

    public static class InstanceGroupIDs
    {
        public static InstanceGroupID<FlatFileToTsvConverterBase> FlatFileToTsvConverters =>
            new InstanceGroupID<FlatFileToTsvConverterBase>(nameof(FlatFileToTsvConverters));
    }
}
