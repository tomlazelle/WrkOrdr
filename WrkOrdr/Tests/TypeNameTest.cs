using System;
using WrkOrdr.TestObjects.Events;

namespace WrkOrdr.Tests
{
    public class TypeNameTest
    {
        public void can_show_type_name()
        {

            Console.WriteLine($"{typeof(CreateWorkOrderEvent).FullName}, {typeof(CreateWorkOrderEvent).Assembly.GetName().Name}");
        }
    }
}