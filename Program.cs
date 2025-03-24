using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int[] sizes = { 1000, 5000, 10000, 50000, 100000 }; // Test edilecek dizi boyutları

        foreach (var size in sizes)
        {
            int[] array = GenerateRandomArray(size);

            Console.WriteLine($"Dizi Boyutu: {size}");

            // Bubble Sort Zaman Ölçümü (Optimizasyonlu)
            int[] arrayCopy = (int[])array.Clone();
            MeasureSortingTime("Bubble Sort (Optimized)", arrayCopy, OptimizedBubbleSort);

            // Selection Sort Zaman Ölçümü (Karşılaştırma için)
            arrayCopy = (int[])array.Clone();
            MeasureSortingTime("Selection Sort", arrayCopy, SelectionSort);

            Console.WriteLine("--------------------------------------");
        }
    }

    // Rastgele dizi oluşturma
    static int[] GenerateRandomArray(int size)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(0, 100000);
        }
        return array;
    }

    // Optimizasyonlu Bubble Sort (Swap kontrolü ile erken çıkış)
    static void OptimizedBubbleSort(int[] array)
    {
        int n = array.Length;
        bool swapped;
        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    swapped = true;
                }
            }
            if (!swapped) // Eğer hiç swap yapılmadıysa zaten sıralıdır
                break;
        }
    }

    // Selection Sort algoritması (Karşılaştırma için)
    static void SelectionSort(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (array[j] < array[minIndex])
                    minIndex = j;
            }
            (array[i], array[minIndex]) = (array[minIndex], array[i]);
        }
    }

    // Zaman ölçme fonksiyonu
    static void MeasureSortingTime(string algorithmName, int[] array, Action<int[]> sortingAlgorithm)
    {
        Stopwatch sw = Stopwatch.StartNew();
        sortingAlgorithm(array);
        sw.Stop();
        Console.WriteLine($"{algorithmName} Süre: {sw.ElapsedMilliseconds} ms");
    }
}
