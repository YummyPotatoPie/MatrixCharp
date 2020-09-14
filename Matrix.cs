namespace MatrixCharp
{
    public class Matrix
    {
        /// <summary>
        /// Элементы матрицы
        /// </summary>
        public double[,] Elements { get; private set; }

        /// <summary>
        /// Функция определение порядка матрицы
        /// </summary>
        /// <returns>
        /// Кортеж: первый элемент которого количество строк в матрице, а второй количество столбцов
        /// </returns>
        public (int, int) Order()
        {
            return (Elements.GetLength(0), Elements.GetLength(1));
        }

        /// <summary>
        /// Проверка равенства объектов
        /// </summary>
        /// <returns>
        /// Булевое значение: true если объекты равны, иначе false
        /// </returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Хэш-функция по умолчанию
        /// </summary>
        /// <returns>
        /// Хэш-код объекта
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Индексатор для получения элементов матрицы по индексу
        /// </summary>
        /// <returns>
        /// Элемент матрицы 
        /// </returns>
        public double this[int i, int j]
        {
            get
            {
                if(i < Elements.GetLength(0) && j < Elements.GetLength(1))
                {
                    return Elements[i, j];
                }
                throw new System.IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Сумма матриц
        /// </summary>
        /// <returns>
        /// Матрица C, равная сумме матриц A, B
        /// </returns>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A.Order() != B.Order()) throw new System.Exception("Orders of matrices are not equal.");

            double[,] C = new double[A.Order().Item1, A.Order().Item2];

            for(int i = 0; i < A.Order().Item1; i++)
            {
                for(int j = 0; j < A.Order().Item2; j++)
                {
                    C[i, j] = A.Elements[i, j] + B.Elements[i, j];
                }
            }

            return new Matrix(C);
        }

        /// <summary>
        /// Разница матриц
        /// </summary>
        /// <returns>
        /// Матрица C, равная разнице матриц A, B
        /// </returns>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A.Order() != B.Order()) throw new System.Exception("Orders of matrices are not equal.");

            double[,] C = new double[A.Order().Item1, A.Order().Item2];

            for (int i = 0; i < A.Order().Item1; i++)
            {
                for (int j = 0; j < A.Order().Item2; j++)
                {
                    C[i, j] = A.Elements[i, j] - B.Elements[i, j];
                }
            }

            return new Matrix(C);
        }

        /// <summary>
        /// Произведение матриц
        /// </summary>
        /// <returns>
        /// Матрица C, равная произведению матриц A, B
        /// </returns>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            if(A.Order().Item2 != B.Order().Item1) throw new System.Exception("Matrices are incompatible.");

            double[,] C = new double[A.Order().Item1, B.Order().Item2];

            for(int i = 0; i < A.Order().Item1; i++) 
            {
                for(int j = 0; j < B.Order().Item2; j++)
                {
                    C[i, j] = 0;

                    for (int k = 0; k < A.Order().Item2; k++)
                    {
                        C[i, j] += A.Elements[i, k] * B.Elements[k, j];
                    }
                }
            }

            return new Matrix(C);
        }

        /// <summary>
        /// Проверка равенства матриц
        /// </summary>
        /// <returns>
        /// Булевое значение: true если матрицы равны, иначе false
        /// </returns>
        public static bool operator ==(Matrix A, Matrix B)
        {
            if (A.Order() != B.Order())
            {
                return false;
            }
            else
            {
                for (int i = 0; i < A.Order().Item1; i++)
                {
                    for (int j = 0; j < A.Order().Item2; j++)
                    {
                        if (A.Elements[i, j] != B.Elements[i, j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Проверка неравенства мартриц
        /// </summary>
        /// <returns>
        /// Булевое значение: false если матрицы равны, иначе true
        /// </returns>
        public static bool operator !=(Matrix A, Matrix B)
        {
            if (A.Order() != B.Order())
            {
                return true;
            }
            else
            {
                for (int i = 0; i < A.Order().Item1; i++)
                {
                    for (int j = 0; j < A.Order().Item2; j++)
                    {
                        if (A.Elements[i, j] != B.Elements[i, j])
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Метод представления матрицы как строки
        /// </summary>
        /// <returns>
        /// Строковое представление матрицы
        /// </returns>
        public override string ToString()
        {
            string Result = "";

            for (int i = 0; i < Elements.GetLength(0); i++)
            {
                for (int j = 0; j < Elements.GetLength(1); j++)
                {
                    Result += Elements[i, j] + " ";
                }
                Result += "\n";
            }
            return Result;
        }

        /// <summary>
        /// Конструктор создающий квадратную нулевую матрицу
        /// </summary>
        public Matrix(int n)
        {
            double[,] Elements = new double[n, n];

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    Elements[i, j] = 0;
                }
            }

            this.Elements = Elements;
        }

        /// <summary>
        /// Конструктор создающий нулевую N x M матрицу
        /// </summary>
        public Matrix(int n, int m)
        {
            double[,] Elements = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Elements[i, j] = 0;
                }
            }

            this.Elements = Elements;
        }

        /// <summary>
        /// Инициализация матрицы напрямую через двумерный массив
        /// </summary>
        public Matrix(double[,] Elements)
        {
            this.Elements = Elements;
        }
    }
}
