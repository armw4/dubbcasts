using System;
using System.Diagnostics;
using NHibernate;

namespace NHibernatePerformanceTip
{
    public static class Extensions
    {
        public static void ExecuteTransaction(this ISession session, Action work)
        {
            using (var transaction = session.BeginTransaction())
            {
                work();
                transaction.Commit();
            }
        }
    }

    public static class IntExtensions
    {
        public static void Times(this int count, Action action)
        {
            for (var i = 0; i < count; i++)
                action();
        }
    }

    public static class ActionExtensions
    {
        public static void Time(this Action action)
        {
            var stopwatch = new Stopwatch();

            Console.WriteLine("Starting action...");

            stopwatch.Start();

            action();

            stopwatch.Stop();

            Console.WriteLine("Action completed...");
            Console.WriteLine("Elapsed time: {0}", stopwatch.Elapsed);
            Console.WriteLine();
        }
    }
}
