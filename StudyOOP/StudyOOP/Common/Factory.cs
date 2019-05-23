namespace StudyOOP.Common
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using StudyOOP.Convert;

    internal static class Factory
    {
        public static FlatFileToTsvConverterBase[] Createinstances(InstanceGroupID instanceGroupID)
        {
            FlatFileToTsvConverterBase[] listret = new FlatFileToTsvConverterBase[instanceGroupID.InstanceIds.Length];
            var count = 0;
            foreach (var id in instanceGroupID.InstanceIds)
            {
                listret[count] = (FlatFileToTsvConverterBase)Activator.CreateInstance(Type.GetType(id.ClassName));
                count += 1;
            }

            return listret;
        }
    }
}
