using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _1
{
    static class ParallelMultiplication
    {
        public static void Run()
        {
            Task initializeTask = new Task(initialize);
            initializeTask.Start();
            initializeTask.Wait();
        }

        static void initialize()
        {
            double[] pMatrix;
            double[] pVector;
            double[] pResult;
            int size;
            int tasks;

           

            Console.WriteLine("Serial matrix-vector multiplication program");
            ProcessInitialization(out pMatrix, out pVector, out pResult, out size, out tasks);

            ResultCalculation (pMatrix, pVector, pResult, size, tasks);

            if (size < 10)
            {
                Console.WriteLine("Вхідна матриця:");
                Matrix.PrintMatrix (pMatrix, size, size);

                Console.WriteLine("Вхідний вектор:");
                Matrix.PrintVector (pVector, size);

                Console.WriteLine("Результуючий вектор:");
                Matrix.PrintVector (pResult, size);
            }

            
        }

        static void ProcessInitialization(
            out double[] matrix,
            out double[] vector,
            out double[] result,
            out int size,
            out int tasks
        )
        {
            size = 0;
            do
            {
                Console.Write("Введіть розмір.Число має бути більшим нуля: ");
            }
            while (!int.TryParse(Console.ReadLine(), out size) || size < 1);

            matrix = new double[size * size];
            vector = new double[size];
            result = new double[size];

            Matrix.RandomDataInitialization (matrix, vector, size);

            tasks = 0;
            do
            {
                Console.Write("Введіть кількість потоків.Число має бути більшим нуля і ділитись без остачі на розмір: ");
            }
            while (!int.TryParse(Console.ReadLine(), out tasks) || tasks < 1 || tasks > size || size%tasks != 0);
        }

        static void ResultCalculation(
            double[] matrix,
            double[] vector,
            double[] result,
            int size,
            int tasks
        )
        {
            int counter = 0;
            Task[] taskList = new Task[tasks];
            Stopwatch watch = new Stopwatch();

            watch.Start();
            for (int i = 0; i < tasks; i++)
            {
                taskList[i] = Task.Run(()=>CalculatePart(matrix,vector,result,size,tasks,counter++));
            }
             
            Task.WaitAll(taskList);
            watch.Stop();
            Console.WriteLine($"Час обчислення: {watch.ElapsedMilliseconds} мс");
        }

        static void CalculatePart(double[] matrix, double[] vector, double[] result,int size, int tasks, int taskNumber){

            // int elements_matrix_per_task = size*size/tasks;
            // int start_matrix_index = taskNumber*size;
            // int end_matrix_index = (taskNumber+1)*size;

            int elements_vector_per_task = size/tasks;
            int start_vector_index = taskNumber*elements_vector_per_task;
            int end_vector_index = (taskNumber+1)*elements_vector_per_task;

            //Console.WriteLine($"Task #{taskNumber}, matrix chunk:[{start_matrix_index},{end_matrix_index-1}], vector chunk:[{start_vector_index},{end_vector_index-1}]");

            for (int i = start_vector_index; i < end_vector_index; i++)
            {
                result[i] = 0;
                for (int j = start_vector_index; j < end_vector_index; j++)
                {
                    result[i] += matrix[i * size + j] * vector[j];
                }
            }
            // Console.Write("\tchunk result:[");
            // for (int i = start_vector_index; i < end_vector_index; i++)
            // {
            //     Console.Write($"{i}: {result[i]},");
            // }
            // Console.WriteLine("]");
        }
    }
}
