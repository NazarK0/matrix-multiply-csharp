using System;
using System.Diagnostics;

namespace _1
{
    static class PlainMultiplication
    {
        public static void Run()
        {
            double[] pMatrix;
            double[] pVector;
            double[] pResult;
            int size;

            Stopwatch watch = new Stopwatch();

            Console.WriteLine("Serial matrix-vector multiplication program");
            ProcessInitialization(out pMatrix,
            out pVector,
            out pResult,
            out size);

            watch.Start();
            ResultCalculation (pMatrix, pVector, pResult, size);
            watch.Stop();

            if (size < 10)
            {
                Console.WriteLine("Вхідна матриця:");
                Matrix.PrintMatrix (pMatrix, size, size);

                Console.WriteLine("Вхідний вектор:");
                Matrix.PrintVector (pVector, size);

                Console.WriteLine("Результуючий вектор:");
                Matrix.PrintVector (pResult, size);
            }

            Console
                .WriteLine($"Час обчислення: {watch.ElapsedMilliseconds} мс");
        }

        static void ProcessInitialization(
            out double[] matrix,
            out double[] vector,
            out double[] result,
            out int size
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
        }

        static void ResultCalculation(
            double[] matrix,
            double[] vector,
            double[] result,
            int size
        )
        {
            for (int i = 0; i < size; i++)
            {
                result[i] = 0;
                for (int j = 0; j < size; j++)
                {
                    result[i] += matrix[i * size + j] * vector[j];
                }
            }
        }
    }
}
