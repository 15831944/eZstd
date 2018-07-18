using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using eZstd.Enumerable;
using eZstd.Mathematics;
using eZstd.Miscellaneous;

namespace ExeTests
{
    class Program
    {
        static void Main(string[] args)
        {
            return;
            var s1 = new myStruct(1);
            var s2 = s1;
            s2.Age = 30;
            Debug.Print(s1.Age.ToString());
            return;
            var s = new List<SegmentData<double, double>>();
            s.Add(new SegmentData<double, double>(1, 2, 100));
            s.Add(new SegmentData<double, double>(3, 4, 200));

            var arr = SegmentData<double, double>.ConvertToArr(s);

            var src = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
            var ins1 = new[] { 9 as object, 10 };
            var ins2 = new[] { "a" as object, "b" };
            var ins3 = new[] { "c" as object, 20 };
            var ins4 = new[] { "d" as object, 10 };
            var ins5 = new[] { 8 as object, 30 };

            var sss = arr.InsertVector<object, object, object>(false, new[] { ins1, ins2, ins3, ins4, ins5 }, new[] { -1f, 1, 1, 1, 2.999f });
            Debug.Print(DebugUtils.PrintArray(sss).ToString());
        }

        private static string RegexMatchEvaluator(Match m)
        {
            var c = m.Index;
            return "好";
        }

        public class myStruct
        {
            public double Age { get; set; }

            public myStruct(double age)
            {
                Age = age;
            }
        }
    }
}