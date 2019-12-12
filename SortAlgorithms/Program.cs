using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace SortAlgorithms
{
    class Program
    {
        public delegate void SortDelegate(int[] num);

        public delegate int[] CreateArrayDelegate(int size);

        static int[] CreateTable(int size) // Satunnaistaulukko
        {
            int[] taulukko = new int[size];
            Random generaattori = new Random();

            for (int i = 0; i < taulukko.Length; i++)
            {
                taulukko[i] = generaattori.Next(size);
            }

            return taulukko;
        }

        static int[] CreateAscendingTable(int size) // Nouseva taulukko
        {
            int[] nousevaTaulukko = new int[size];
            for (int i = 0; i < nousevaTaulukko.Length; i++)
            {
                nousevaTaulukko[i] = i;
            }
            return nousevaTaulukko;

        }

        static int[] CreateDescendingTable(int size) // Laskeva taulukko
        {
            int[] laskevaTaulukko = new int[size];
            for (int i = 0; i < laskevaTaulukko.Length; i++)
            {
                laskevaTaulukko[i] = size--;
            }
            return laskevaTaulukko;

        }

        private static void InsertionSort(int[] arr)
        {
            int i, key, j;

            int n = arr.Length;

            for (i = 1; i < n; i++)
            {
                key = arr[i];
                j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }

        private static void SelectionSort(int[] num)
        {
            int i, j, first, temp;
            for (i = num.Length - 1; i > 0; i--)
            {
                first = 0;
                for (j = 1; j <= i; j++)
                {
                    if (num[j] > num[first])
                        first = j;
                }
                temp = num[first];
                num[first] = num[i];
                num[i] = temp;
            }
        }

        private static void BubbleSort(int[] luku)
        {
            int temp;

            for (int j = 0; j <= luku.Length - 2; j++)
            {
                for (int i = 0; i <= luku.Length - 2; i++)
                {
                    if (luku[i] > luku[i + 1])
                    {
                        temp = luku[i + 1];
                        luku[i + 1] = luku[i];
                        luku[i] = temp;
                    }
                }
            }

        }

        private static void QuickSort(int[] a, int lo, int hi)
        {
            //lo is the lower index, hi is the upper index of the region of array a that is to be sorted
            int i = lo, j = hi, h;

            // comparison element x
            int x = a[(lo + hi) / 2];

            // partition
            do
            {
                while (a[i] < x) i++;
                while (a[j] > x) j--;
                if (i <= j)
                {
                    h = a[i];
                    a[i] = a[j];
                    a[j] = h;
                    i++; j--;
                }
            } while (i <= j);

            // recursion
            if (lo < j) QuickSort(a, lo, j);
            if (i < hi) QuickSort(a, i, hi);
        }

        private static void QuickSortMain(int[] input)
        {
            QuickSort(input, 0, input.Length - 1);
        }

        private static void ShellSort(int[] shell)
        {
            int n = shell.Length;
            int gap = n / 2;
            int temp;

            while (gap > 0)
            {
                for (int i = 0; i + gap < n; i++)
                {
                    int j = i + gap;
                    temp = shell[j];

                    while (j - gap >= 0 && temp < shell[j - gap])
                    {
                        shell[j] = shell[j - gap];
                        j = j - gap;
                    }
                    shell[j] = temp;
                }
                gap = gap / 2;
            }
        }

        private static void Merge(int[] input, int left, int middle, int right)
        {
            int[] leftArray = new int[middle - left + 1];
            int[] rightArray = new int[right - middle];

            Array.Copy(input, left, leftArray, 0, middle - left + 1);
            Array.Copy(input, middle + 1, rightArray, 0, right - middle);

            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    input[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else
                {
                    input[k] = rightArray[j];
                    j++;
                }
            }
        }


        private static void MergeSort(int[] input, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSort(input, left, middle);
                MergeSort(input, middle + 1, right);

                Merge(input, left, middle, right);
            }
        }

        private static void MergeSortMain(int[] input)
        {
            MergeSort(input, 0, input.Length - 1);
        }

        static void Metodi(SortDelegate del)
        {
            int[] taulukko = CreateTable(10000);
            int[] nousevaTaulukko = CreateAscendingTable(10000);
            int[] laskevaTaulukko = CreateDescendingTable(10000);
            Stopwatch kello = Stopwatch.StartNew();
            del(taulukko);
            var elapsedTime = kello.Elapsed;
            Console.WriteLine(del.Method.Name.ToString() + " Satunnaistaulukko: " + elapsedTime);

            kello = Stopwatch.StartNew();
            del(nousevaTaulukko);
            elapsedTime = kello.Elapsed;
            Console.WriteLine(del.Method.Name.ToString() + " Nousevataulukko: " + elapsedTime);

            kello = Stopwatch.StartNew();
            del(laskevaTaulukko);
            elapsedTime = kello.Elapsed;
            Console.WriteLine(del.Method.Name.ToString() + " Laskevataulukko: " + elapsedTime);

        }

        static void Main(string[] args)
        {
            SortDelegate insertionSort = new SortDelegate(InsertionSort);
            SortDelegate selectionSort = new SortDelegate(SelectionSort);
            SortDelegate bubbleSort = new SortDelegate(BubbleSort);
            SortDelegate quickSort = new SortDelegate(QuickSortMain);
            SortDelegate shellSort = new SortDelegate(ShellSort);
            SortDelegate mergeSort = new SortDelegate(MergeSortMain);


            Metodi(insertionSort);
            Metodi(selectionSort);
            Metodi(bubbleSort);
            Metodi(Array.Sort);
            Metodi(quickSort);
            Metodi(shellSort);
            Metodi(mergeSort);

            Console.ReadLine();
        }
    }
}
