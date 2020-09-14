using System.Collections.Generic;

namespace MatrixCharp
{
    public static class Methods
    {
        /// <summary>
        /// Произведение матрицы на число
        /// </summary>
        /// <returns>
        /// Матрица умноженная на заданное число Lambda
        /// </returns>
        public static Matrix Product(double Lambda, Matrix matrix)
        {
            for(int i = 0; i < matrix.Order().Item1; i++)
            {
                for (int j = 0; j < matrix.Order().Item2; j++)
                {
                    matrix.Elements[i, j] *= Lambda;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Возведение матрицы в заданную степень. Степень должна быть натуральным числом
        /// </summary>
        /// <returns>
        /// Матрица возведенная в заданную степень
        /// </returns>
        public static Matrix Power(Matrix matrix, int power)
        {
            if (power < 0 || matrix.Order().Item1 != matrix.Order().Item2) throw new System.Exception("Invalid arguments.");

            if (power == 0)
            {
                double[,] E = new double[matrix.Order().Item1, matrix.Order().Item1];

                for(int i = 0; i < matrix.Order().Item1; i++)
                {
                    for(int j = 0; j < matrix.Order().Item1; j++)
                    {
                        E[i, j] = i == j ? 1 : 0;
                    }
                }

                return new Matrix(E);
            }
            else
            {
                return matrix * Power(matrix, power - 1);
            }
        }

        /// <summary>
        /// Транспонирование матрицы(смена индексов столбцов на индекцы строк)
        /// </summary>
        /// <returns>
        /// Транспонированная матрица
        /// </returns>
        public static Matrix Transpose(Matrix matrix)
        {
            double[,] TransposeMatrix = new double[matrix.Order().Item2, matrix.Order().Item1];

            for(int i = 0; i < matrix.Order().Item1; i++)
            {
                for(int j = 0; j < matrix.Order().Item2; j++)
                {
                    TransposeMatrix[j, i] = matrix.Elements[i, j];
                }
            }

            return new Matrix(TransposeMatrix);
        }

        /// <summary>
        /// Определение детерминанта квадратной матрицы (неоптимизированный алгоритм)
        /// </summary>
        /// <returns>
        /// Возвращает числовое значение детерминанта
        /// </returns>
        public static double Determinant(Matrix matrix)
        {
            if (matrix.Order().Item1 != matrix.Order().Item2) throw new System.Exception("Invalid matrix, order must be N x N.");
            
            if(matrix.Order() == (1,1))
            {
                return matrix[0, 0];
            }
            else if(matrix.Order() == (2,2))
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            else
            {
                double Det = 0;

                List<Matrix> Algebra = new List<Matrix>();

                for(int j = 0; j < matrix.Order().Item1; j++)
                {
                    double[,] Minor = new double[matrix.Order().Item1 - 1, matrix.Order().Item1 - 1];

                    for(int k = 1; k < matrix.Order().Item1; k++)
                    {
                        for(int h = 0; h < matrix.Order().Item1 - 1; h++)
                        {
                            if(h != j)
                            {
                                Minor[k - 1, h] = matrix[k, h];
                            }
                        }
                    }
                }

                return Det;
            }
        }

        /// <summary>
        /// Решение матричного полинома, общий вид которого L0 * A^0 + L1 * A^1 + ... Ln * A^n.
        /// </summary>
        /// <returns>
        /// Матрица - решение матричного полинома
        /// </returns>
        public static Matrix Polynomial(Matrix matrix, params double[] Lambdas)
        {
            if (matrix.Order().Item1 != matrix.Order().Item2) throw new System.Exception("Invalid matrix, order must be N x N.");

            Matrix PolynomialMatrix = new Matrix(matrix.Order().Item1);

            for (int i = 0; i < Lambdas.Length; i++)
            {
                PolynomialMatrix += Product(Lambdas[i], Power(matrix, i));
            }

            return PolynomialMatrix;
        }
    }
}
