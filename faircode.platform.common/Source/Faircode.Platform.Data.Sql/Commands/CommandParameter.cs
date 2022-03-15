using System;

namespace Faircode.Platform.Data.Sql.Commands
{
    public sealed class CommandParameter
    {
        public string Name { get; private set; }
        public object Value { get; private set; }

        public CommandParameter(String name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
