using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeTests
{

    /// <summary> 现在需要遍历School下面的所有的学生。 </summary>
    /// <remarks>说明：类School下面有一个Class的集合，每个Class下面有有一个Student的集合。每个学生有Name和Sex两个属性。 现在需要遍历School下面的所有的学生。
    /// 当然我们可以用两个嵌套的foreach语句类遍历School下面的所有的Class，然后再用foreach来遍历Class下面的所有的Student，把他们添加到一个List里去。
    /// 这个场景也是实际编程中经常遇到的。有了Linq我们就可以大大的简化我们的代码：</remarks>
    internal class LinqTest_SelectMany
    {
        public static void Main(string[] args)
        {
            //初始化数据  
            School s = new School();
            for (int i = 0; i < 5; i++)
            {
                s.Classes.Add(new Class());
            }
            s.Classes[0].Students.Add(new Student(1, "a0"));
            s.Classes[1].Students.Add(new Student(1, "b0"));
            s.Classes[2].Students.Add(new Student(0, "c0"));
            s.Classes[3].Students.Add(new Student(0, "d0"));
            s.Classes[4].Students.Add(new Student(0, "e0"));
            s.Classes[0].Students.Add(new Student(0, "a1"));
            s.Classes[0].Students.Add(new Student(1, "a1"));
            s.Classes[0].Students.Add(new Student(1, "a2"));
            s.Classes[0].Students.Add(new Student(1, "a3"));
            s.Classes[1].Students.Add(new Student(0, "b1"));
            s.Classes[2].Students.Add(new Student(0, "c1"));
            s.Classes[3].Students.Add(new Student(0, "d1"));

            //取出school下的所有性别是0的student  
            // 我们只需要用一下SelectMany就可以了，不用去用foreach进行两次遍历。SelectMany在MSDN中的解释：将序列的每个元素投影到 IEnumerable(T) 并将结果序列合并为一个序列。
            var stds = s.Classes.SelectMany(b => b.Students).Where(i => i.Sex == 0);

            // 比较 Select 与 SelectMany 的区别
            IEnumerable<IList<Student>> test1 = s.Classes.Select(r => r.Students);
            IEnumerable<Student> test2 = s.Classes.SelectMany(r => r.Students);

            foreach (var b in stds)
            {
                Console.WriteLine(b.Name);
            }
            Console.ReadKey();
        }

        private class School
        {
            internal IList<Class> Classes { get; set; }

            internal School()
            {
                Classes = new List<Class>();
            }
        }

        private class Class
        {
            internal IList<Student> Students { get; set; }

            internal Class()
            {
                Students = new List<Student>();
            }
        }

        private class Student
        {
            public Student(int i, string name)
            {
                Sex = i;
                Name = name;
            }

            public string Name { get; set; }

            public int Sex { get; set; }
        }
    }
}
