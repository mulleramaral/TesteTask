using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TesteTask
{
    class Program
    {
        static Random r = new Random();

        public static void Main(String[] args)
        {
            Console.WriteLine("INICIADO SEM TASK");

            Class1 obj = new Class1();
            var dtInicial = DateTime.Now;

            for (int i = 0; i < 200; i++)
            {
                obj.Consume(new Akshay(i));
            }

            var dtFinal = DateTime.Now;
            var tempo = dtFinal - dtInicial;
            Console.Write("SEM TASK - Finalizado {0} segundos", tempo.TotalSeconds.ToString("0.000000"));
            Console.ReadLine();

            Console.WriteLine("INICIADO COM TASK");
            dtInicial = DateTime.Now;
            List<Task> task = new List<Task>();
            for (int i = 0; i < 200; i++)
            {
                task.Add(obj.Produce(new Akshay(i)));
            }
            Task.WaitAll(task.ToArray());

            dtFinal = DateTime.Now;
            tempo = dtFinal - dtInicial;
            Console.Write("COM TASK - Finalizado {0} segundos", tempo.TotalSeconds.ToString("0.000000"));
            Console.ReadLine();
        }
    }

    public class Akshay
    {
        public int id;
        public Akshay(int _id)
        {
            id = _id;
        }
    }
    class Class1
    {
        public int QueueLength;
        public Class1()
        {
            QueueLength = 0;
        }

        public Task Produce(Akshay ware)
        {
            Task task = Task.Factory.StartNew(() => Consume(ware));
            QueueLength++;
            return task;
        }

        public void Consume(Object obj)
        {
            Console.WriteLine("Thread {0} consumes {1}", Task.CurrentId, ((Akshay)obj).id);
            Thread.Sleep(100);
            QueueLength--;
        }
    }
}
