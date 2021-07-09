using System;

namespace _1
{
    static class Matrix
    {
        public static void DummyDataInitialization(
            double[] matrix,
            double[] vector,
            int size
        )
        {
            for (int i = 0; i < size; i++)
            {
                vector[i] = 1;
                for (int j = 0; j < size; j++)
                {
                    matrix[i * size + j] = i;
                }
            }
        }

        public static void RandomDataInitialization(
            double[] matrix,
            double[] vector,
            int size
        )
        {
            for (int i = 0; i < size; i++)
            {
                double min = -100;
                double max = 100;

                vector[i] = new Random().NextDouble() * (max - min) + min;
                for (int j = 0; j < size; j++)
                {
                    matrix[i * size + j] =
                        new Random().NextDouble() * (max - min) + min; //random from min to max
                }
            }
        }

        public static void PrintMatrix(
            double[] matrix,
            int rowCount,
            int colCount
        )
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    Console.Write($"{matrix[i * colCount + j]:F3} ");
                }
                Console.WriteLine();
            }
        }

        public static void PrintVector(double[] vector, int size)
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{vector[i]:F3} ");
            }
            Console.WriteLine();
        }
    }
}
