namespace AspNetExample.Models
{
    public class Constants
    {
        public const string EXAMPLE_TASK_DI_ATTRIBUTES_ID = "a714aa26-87c4-4b42-bfbc-acdb2b184d53";
        public readonly static Guid EXAMPLE_DI_TASK_GUID = Guid.Parse(EXAMPLE_TASK_DI_ATTRIBUTES_ID);
        public readonly static Guid EXAMPLE_TASK_WITH_METADATA = Guid.Parse("a714aa36-87c4-4b42-bfbc-acdb2b184d53");
        public readonly static Guid EXAMPLE_TASK_WITHOUT_METADATA = Guid.Parse("b714aa36-87c4-4b42-bfbc-acdb2b184d53");
    }
}
