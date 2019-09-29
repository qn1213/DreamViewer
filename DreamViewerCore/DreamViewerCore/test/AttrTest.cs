using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CommandLine;

namespace DreamViewerCore.test.AttrTest
{
    class AttrTest
    {
        public static void Parse<T1>(string arg)
        {
            //T1 cTest = T1();
            Type t1 = typeof(T1);
            PropertyInfo[] t2 = t1.GetProperties();
            foreach (PropertyInfo field in t2)
            {
                var att = Attribute.GetCustomAttribute(field, typeof(Attribute));
                Console.WriteLine("====");

                if (att.GetType() == typeof(OptionAttribute))
                {
                    Console.Write(((OptionAttribute)att).ShortName + "  ");
                    Console.WriteLine(((OptionAttribute)att).LongName);
                }
                else if (att.GetType() == typeof(ValueAttribute))
                {
                    Console.Write(((ValueAttribute)att).Index + "  ");
                    Console.WriteLine(((ValueAttribute)att).MetaName);
                }

                Console.WriteLine(att.GetType());
            }
            
        }

    }
}
