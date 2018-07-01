using Infratructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_LambadaDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> pA = new List<Person>();
            pA.Add(new Person() { PId = 1, Name = "张三", Age = 16, JobId = 1 });
            pA.Add(new Person() { PId = 2, Name = "小红", Age = 18, JobId = 2 });
            pA.Add(new Person() { PId = 3, Name = "王武", Age = 20, JobId = 3 });
            pA.Add(new Person() { PId = 4, Name = "小梅", Age = 17, JobId = 4 });
            pA.Add(new Person() { PId = 5, Name = "小李", Age = 24, JobId = 3 });

            List<Job> jB = new List<Job>();
            jB.Add(new Job() { JobId = 1, JobName = "制造业", WorkAge = 3 });
            jB.Add(new Job() { JobId = 2, JobName = "IT行业", WorkAge = 5 });
            jB.Add(new Job() { JobId = 3, JobName = "建筑业", WorkAge = 2 });
            jB.Add(new Job() { JobId = 4, JobName = "金融业", WorkAge = 1 });

            //返回Name字段列表
            var result = pA.Select(k => k.Name).ToList();
            //加上index，并且返回一个匿名函数
            var result0 = pA.Select((k,index) => new { index = index,person = k}).ToList();

            //筛选出Age大于18的记录
            var result1 = pA.Where(k => k.Age > 18).ToList();
            //r参数代表元素在集合中的索引
            var result112 = pA.Where((k,r) =>
            {
                if (r <= 3)
                    return false;
                return k.Age > 18;
            }
            ).ToList();

            //筛选出Age大于18并且JobId等于3的记录
            var result2 = pA.Where(k => k.Age > 18 && k.JobId==3 ).ToList();

            //筛选出Age大于18或者JobId等于3的记录
            var result3 = pA.Where(k => k.Age > 18 || k.JobId == 3).ToList();

            //先筛选出Age大于10的记录，再根据JobId分组，再选择分组后的键输出
            var result4 = pA.Where(k => k.Age > 10).GroupBy(j => j.JobId).Select(l => l.Key).ToList();

            //一般情况下Group和ToDictionary合起来用 用来分组
            var result41 = pA.Where(k => k.Age > 10).GroupBy(j => j.JobId);
            var result42 = result41.ToDictionary(r => r.Key);
            var result43 = result41.ToDictionary(r => r.Key,rr=>rr.Select(r=>r.Name));
            //var result44 = pA.Where(k => k.Age > 10).ToDictionary(r => r,
            //    new CustomEqualityComparer<Person>());
            

            //IGrouping实现了IEnumerable，也是集合，是用来分组的集合

            //先筛选出Age大于10的记录，再根据Person对象分组，再选择分组后的键输出
            var result5 = pA.Where(k => k.Age > 10)
                .GroupBy(a => new Person { PId = a.PId, Name = a.Name, Age = a.Age, JobId = a.JobId })
                .Select(a => a.Key).ToList();

            //单列去重
            var result131 = pA.Select(k => k.JobId).Distinct();
            var result132 = pA.Select(k => k.JobId).Distinct();

            //先筛选出Age大于10的记录，再根据Age列升序/正序输出
            var result6 = pA.Where(k => k.Age > 10)
                .OrderBy(k => k.Age).ToList();
            var result61 = pA.Where(k => k.Age > 10)
                .OrderBy(k => k,new CustomCompare<Person>()).ToList();

            //先筛选出Age大于10的记录，再根据Age列降序输出
            var result7 = pA.Where(k => k.Age > 10)
                .OrderByDescending(k => k.Age).ToList();

            //先筛选出Age大于10的记录，再按Age倒序，再按JobId正序，再按名称倒序。
            var result8 = pA.Where(k => k.Age > 10).OrderByDescending(k => k.Age)
                .ThenBy(k => k.JobId).ThenByDescending(k => k.Name).ToList();

            //动态排序
            var result81 = pA.OrderBy(k => GetPropertyValue(k, "Age")).ToList();
            var result82 = pA.OrderBy(k => GetPropertyValue(k, "JobId")).ToList();

            //筛选出Age大于10的记录数量
            var result9 = pA.Where(k => k.Age > 10).Count();

            //获取Age的平均值
            var result10 = pA.Average(k => k.Age);

            //获取Age的平均值
            var result101 = pA.Sum(k => k.Age);

            //筛选出Name的记录包含'小'字符的记录
            var result11 = pA.Where(k => k.Name.Contains("小")).ToList();


            var result12 = pA.Join(jB, j => j.JobId, k => k.JobId, (j, k) => new { j.PId, j.Name, j.Age, k.JobName }).ToList();
            var result13 = pA.Join(jB, j => j.JobId, k => k.JobId, (j, k) => new { j.PId, j.Name, j.Age, k.JobName }).DefaultIfEmpty().ToList();

            //先筛选出Age大于10的记录，再取前三条记录
            var result154 = pA.Where(o => o.Age > 10).Take(3).ToList();
            //从index=0开始，若符合条件，则取出并继续下一个，否则。停止。(与Where不同)
            var result155 = pA.TakeWhile(k => k.Age > 18).ToList();
            var result156 = pA.TakeWhile((k, index) => index < 2).ToList();

            var result161 = pA.Skip(2);
            //从index=0开始，若符合条件，则跳过该继续下一个，否则。停止。
            var result162 = pA.SkipWhile(r => r.JobId == 3);
            var result163 = pA.SkipWhile((r,index) => index <= 3);

            //返回满足指定条件的对象，若为空或返回数量不止一个，则抛异常
            var result171 = pA.Single(r => r.JobId == 2);
            //返回满足指定条件的对象，若为空，则返回空；若返回数量不止一个，则抛异常
            var result172 = pA.SingleOrDefault(r => r.JobId == 2);

            //返回满足指定条件的对象列表中的第一个对象，若为空，则抛异常
            var result173 = pA.First(r => r.JobId == 2);
            //返回满足指定条件的对象列表中的第一个对象，若为空，则返回空
            var result174 = pA.FirstOrDefault(r => r.JobId == 2);

            //返回满足指定条件的对象列表中的最后一个对象，若为空，则抛异常
            var result175 = pA.Last(r => r.JobId == 2);
            //返回满足指定条件的对象列表中的最后一个对象，若为空，则返回空
            var result176 = pA.LastOrDefault(r => r.JobId == 2);

            //返回的是IEnumerabele<string>类型
            var result181 = pA.SelectMany(r => new List<string>()
            {
               r.Name,
               r.Age.ToString()
            });
            var result182 = pA.SelectMany((r,index) => new List<string>()
            {
               r.Name,
               r.Age.ToString(),
               index.ToString()
            });
            //返回结果为List<object>，其中r为Person,rr为第一步返回的 List<string>
            var result183 = pA.SelectMany(r => new List<string>()
            {
               r.Name,
               r.Age.ToString()
            },(r,rr) => new List<object>
            {
                r.JobId,
                rr
            });

            var result191 = Enumerable.Repeat(pA.First(), 10);

            var result201 = pA.Min(r => r.Age);
            var result202 = pA.Max(r => r.Age);

            var result211 = pA.All(r => r.Age > 18);
            var result212 = pA.Any(r => r.Age > 18);
            var result213 = pA.Any();

            var result221 = pA.Cast<object>();
            var result222 = pA.OfType<Job>();


        }

        private static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property); return propertyInfo.GetValue(obj, null);
        }
    }
}
