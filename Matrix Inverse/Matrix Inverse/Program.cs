using System.Drawing;
internal class Program
{
    private static void Main(string[] args)
    {
    start:
        Console.Clear();
        textcolorReset();

        Console.Write("Enter sive of Matrix: ");
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine();
        
        if (n <= 0 || n > 6)
        {
            settextcolor(12);
            Console.WriteLine("Error");
            textcolorReset();
            goto end;
        }

        double[,] matrix = new double[n, n];
        
            
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter {n} values separated by spaces for row {i+1}");
            string input = Console.ReadLine();
            string[] values = input.Split(' ');
        
            for (int j = 0; j < n; j++)
            {
                // Parse the string value at the current index to an integer
                int value = int.Parse(values[j]);
                // Assign the value to the element at the current row and column
                matrix[i, j] = value;
            }
        }

        
        settextcolor(10);
        Console.WriteLine("\n----The Matrix----");
        textcolorReset();
        printMatrix(matrix, n);

        if (n == 2)
        { inverse_2(matrix, n); }
        else if (n > 2)
        { inverse_n(matrix,n); }
        else if (n == 1)
        { inverse_n(matrix, n); }


        settextcolor(10);
        Console.WriteLine("\n\tThe End");
        textcolorReset();
    end:
        Console.WriteLine("\nPrees 'Enter' to reload the program");
        while (Console.ReadKey().Key != ConsoleKey.Enter)
        {
        }
        goto start;
    }

       static void inverse_2(double[,] matrix, int n)
        {
            change_2(matrix);

            double ad = matrix[0, 0] * matrix[1, 1];    // ----The Matrix----
            double bc = matrix[0, 1] * matrix[1, 0];     //    a      b
            double det = ad - bc;                       //    c       d
            if (det == 0)
            {
                settextcolor(12);
                Console.WriteLine("\nThe result of Det division is zero");
                textcolorReset();
                Console.Write("Type of Matrix: ");
                settextcolor(9);
                Console.WriteLine("Not inverse (Singular)");
                textcolorReset();
                return;
            }                            
            for (int i = 0; i < n; i++)                  
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = matrix[i, j] * (1 / det);
                }
            }
            Console.WriteLine($"\nDet: {det}");
            Console.WriteLine($"\nThe inverse of Matrix");
        printMatrix(matrix, n);

        }
    static void change_2(double[,] matrix)
    {
        //تبديل القطر الرئيسى
        double temp = matrix[0, 0];
        matrix[0, 0] = matrix[1, 1];
        matrix[1, 1] = temp;
        //تغير اشاره القطر الفرعى
        matrix[0, 1] = -matrix[0, 1];
        matrix[1, 0] = -matrix[1, 0];
    }
    static void inverse_n(double[,] matrix,int n)
    {
        Solve_inverse_n(matrix,n);
        

    }

       static void Solve_inverse_n(double[,] augmentedMatrix, int n)
        {

        double[,] New_augmented_Matrix  = AugmentMatrix(augmentedMatrix, n);
        settextcolor(14);
        Console.WriteLine("\n---Matrix After addind Identity matrix---");
        textcolorReset();
        printMatrix_inverese(New_augmented_Matrix, n);

            for (int i = 0; i < n; i++)
            {
                //    التأكد من وجود صف يحتوي على معامل غير صفر في المركز 
                if (New_augmented_Matrix[i, i] == 0)
                {
                    //فى حاله وجود صفر فى اخر الصف الاخير 
                    if (New_augmented_Matrix[n - 1, n - 1] == 0)
                    {
                        break;
                    }

                    cheangeRow(New_augmented_Matrix, n, i);
                }
                


                //    جعل المعامل الرئيسي في الصف يساوي 1 
                double pivot = New_augmented_Matrix[i, i];
                for (int j = i; j < 2*n; j++)
                {
                    New_augmented_Matrix[i, j] /= pivot;
            }

                //     جعل المعامالت األخرى في األعمدة المحددة تساوي صفر 
                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        double factor = New_augmented_Matrix[k, i];
                    for (int j = i; j < 2*n; j++)//***************************************************
                        {
                            New_augmented_Matrix[k, j] -= factor * New_augmented_Matrix[i, j];
                        }
                    }
                }

            settextcolor(12);
            Console.WriteLine($"\nthe matrix after step. {i + 1}:");
            textcolorReset();
            printMatrix_inverese(New_augmented_Matrix, n);
            }
        Console.WriteLine();
        Console.Write("Type of the Matrix:");
        chack_matrix(New_augmented_Matrix, n);
    }

    static double[,] AugmentMatrix(double[,] matrix,int n)
    {
        
        double[,] augmentedMatrix = new double[n, 2 * n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                augmentedMatrix[i, j] = matrix[i, j];
            }

            augmentedMatrix[i, i + n] = 1;
        }

        return augmentedMatrix;
    }
    static void cheangeRow(double[,] array, int n, int i)
        {
        for(int c = i; c < n; c++)
        {
            if (array[c + 1, i] != 0)
            {
                for (int j = 0; j < 2 * n; j++)
                {
                    // استخدام متغير مؤقت لتخزين قيمة الصف الأول
                    double temp = array[i, j];
                    // نسخ قيمة الصف الثانى إلى الصف الأول
                    array[i, j] = array[c + 1, j];
                    // نسخ قيمة المتغير المؤقت إلى الصف الثانى
                    array[c + 1, j] = temp;

                }
                settextcolor(9);
                Console.WriteLine($"\nMatrix After changing row.{i + 1} by row.{c + 2}");
                textcolorReset();
                printMatrix_inverese(array, n);
                return;
            }

        }
        }
       static void chack_matrix(double[,] matrix, int n)
        {
            bool type0 = false;


        for (int i = 0; i < n; i++)
            {
                if (matrix[n - 1, i] == 0)
                {
                    type0 = true;
                }
                else
                {
                    type0 = false;
                }
            }


            if (type0 == true)
            {
            settextcolor(4);
            Console.WriteLine("This Matrix is not invertible\n");
            textcolorReset();
            return;
            }
            else
            {
            settextcolor(10);
            Console.WriteLine("This Matrix is invertible\n");
            textcolorReset();
            Console.WriteLine("---Inverse of Matrix---");
            print_final_Matrix(matrix, n);
            }


        }
       static void printMatrix(double[,] martix, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                if (j == 0)
                {
                    Console.Write("[");
                }
                    Console.Write($"{martix[i, j]:F2} ".PadLeft(6));
                if (j == n-1)
                {
                    Console.Write("]");
                }
                Thread.Sleep(150);
                }
                Console.WriteLine();
            }
        }
    static void printMatrix_inverese(double[,] martix, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 2*n; j++)
            {
                if (j == 0)
                {
                    Console.Write("[");
                }
                if (j == n)
                {
                    Console.Write("|");
                }
                Console.Write($"{martix[i, j]:F2} ".PadLeft(6));
                if (j == 2*n - 1)
                {
                    Console.Write("]");
                }
                if(n == 3) {Thread.Sleep(80); }
                if(n > 3) { Thread.Sleep(40); }
                

            }
            Console.WriteLine();
        }
    }

    static void print_final_Matrix(double[,] martix, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = n; j < 2*n; j++)
            {
                if (j == n)
                {
                    Console.Write("[");
                }
                Console.Write($"{martix[i, j]:F2} ".PadLeft(6));
                if (j == 2*n - 1)
                {
                    Console.Write("]");
                }
                Thread.Sleep(150);
            }
            Console.WriteLine();
        }
    }
    static void textcolorReset()
        {
            Console.ForegroundColor = (ConsoleColor)7;
        }
       static void settextcolor(int color)
        {
            Console.ForegroundColor = (ConsoleColor)color;
        }
    
}
//
//     settextcolor(12);
//     Console.WriteLine("");
//     textcolorReset();
//
