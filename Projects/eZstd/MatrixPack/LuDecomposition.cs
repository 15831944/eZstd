// ----------------------------------------------------------------------------------------
// Lutz Roeder's .NET MatrixPack, September 2000, adapted from MatrixPack for COM and Jama routines
// http://www.aisto.com/roeder/dotnet
// roeder@aisto.com
// ----------------------------------------------------------------------------------------
namespace eZstd.MatrixPack
{
    using System;

    /// <summary>
    ///   LU decomposition of a rectangular matrix（不限定为方阵）.
    /// </summary>
    /// <remarks>
    ///   For an m-by-n matrix <c>A</c> with m >= n, the LU decomposition is an m-by-n
    ///   unit lower triangular matrix <c>L</c>, an n-by-n upper triangular matrix <c>U</c>,
    ///   and a permutation vector <c>piv</c> of length m so that <c>A(piv)=L*U</c>.
    ///   If m &lt; n, then <c>L</c> is m-by-m and <c>U</c> is m-by-n.
    ///   The LU decompostion with pivoting always exists, even if the matrix is
    ///   singular, so the constructor will never fail.  The primary use of the
    ///   LU decomposition is in the solution of square systems of simultaneous
    ///   linear equations. This will fail if <see cref="IsNonSingular"/> returns <see langword="false"/>.
    /// </remarks>
    public class LuDecomposition
    {
        private Matrix LU;
        private int pivotSign;
        /// <summary>
        /// 矩阵的主元所在矩阵中所对应的下标，一般指主对角线上的元素,左上角到右下角。如果不是方阵就是左上角到最下一行,
        /// </summary>
		private int[] pivotVector;

        /// <summary>Construct a LU decomposition.</summary>	
        public LuDecomposition(Matrix A)
        {
            LU = A.Clone();
            double[][] lu = LU.Array;
            int rows = A.Rows;
            int columns = A.Columns;
            pivotVector = new int[rows];
            for (int i = 0; i < rows; i++)
                pivotVector[i] = i;
            pivotSign = 1;
            double[] LUrowi;
            double[] LUcolj = new double[rows];

            // Outer loop.
            for (int j = 0; j < columns; j++)
            {
                // Make a copy of the j-th column to localize references.
                for (int i = 0; i < rows; i++)
                    LUcolj[i] = lu[i][j];

                // Apply previous transformations.
                for (int i = 0; i < rows; i++)
                {
                    LUrowi = lu[i];

                    // Most of the time is spent in the following dot product.
                    int kmax = Math.Min(i, j);
                    double s = 0.0;
                    for (int k = 0; k < kmax; k++)
                        s += LUrowi[k] * LUcolj[k];
                    LUrowi[j] = LUcolj[i] -= s;
                }

                // Find pivot and exchange if necessary.
                int p = j;
                for (int i = j + 1; i < rows; i++)
                {
                    if (Math.Abs(LUcolj[i]) > Math.Abs(LUcolj[p]))
                    {
                        p = i;
                    }
                }

                if (p != j)
                {
                    for (int k = 0; k < columns; k++)
                    {
                        double t = lu[p][k];
                        lu[p][k] = lu[j][k];
                        lu[j][k] = t;
                    }

                    int v = pivotVector[p];
                    pivotVector[p] = pivotVector[j];
                    pivotVector[j] = v;

                    pivotSign = -pivotSign;
                }

                // Compute multipliers.

                if (j < rows & lu[j][j] != 0.0)
                {
                    for (int i = j + 1; i < rows; i++)
                    {
                        lu[i][j] /= lu[j][j];
                    }
                }
            }
        }

        /// <summary>Returns if the matrix is non-singular（非奇异的）.</summary>
        public bool IsNonSingular
        {
            get
            {
                for (int j = 0; j < LU.Columns; j++)
                {
                    if (Math.Abs(LU[j, j]) < 0.0000001)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>Returns the determinant（行列式，方阵才有行列式） of the matrix.</summary>
        public double Determinant
        {
            get
            {
                if (LU.Rows != LU.Columns) throw new ArgumentException("Matrix must be square.");
                double determinant = (double)pivotSign;
                for (int j = 0; j < LU.Columns; j++)
                    determinant *= LU[j, j];
                return determinant;
            }
        }

        #region ---   上下三角矩阵

        /// <summary>Returns the lower triangular factor <c>L</c> with <c>A=LU</c>.</summary>
        public Matrix LowerTriangularFactor
        {
            get
            {
                int rows = LU.Rows;
                int columns = LU.Columns;
                Matrix X = new Matrix(rows, columns);
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++)
                        if (i > j)
                            X[i, j] = LU[i, j];
                        else if (i == j)
                            X[i, j] = 1.0;
                        else
                            X[i, j] = 0.0;
                return X;
            }
        }

        /// <summary>Returns the lower triangular factor <c>L</c> with <c>A=LU</c>.</summary>
        public Matrix UpperTriangularFactor
        {
            get
            {
                int rows = LU.Rows;
                int columns = LU.Columns;
                Matrix X = new Matrix(rows, columns);
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++)
                        if (i <= j)
                            X[i, j] = LU[i, j];
                        else
                            X[i, j] = 0.0;
                return X;
            }
        }

        #endregion

        /// <summary>Returns the pivot permuation vector.</summary>
        public double[] PivotPermutationVector
        {
            get
            {
                int rows = LU.Rows;
                double[] p = new double[rows];
                for (int i = 0; i < rows; i++)
                    p[i] = (double)pivotVector[i];
                return p;
            }
        }

        /// <summary>Solves a set of equation systems of type <c>A * X = B</c>.
        /// 比如求解最简单的线性方程组 A * x = b . </summary>
        /// <param name="B">Right hand side matrix with as many rows as <c>A</c> and any number of columns.</param>
        /// <returns>Matrix <c>X</c> so that <c>L * U * X = B</c>.</returns>
        public Matrix Solve(Matrix B)
        {
            /* 调用举例
        public static void Main()
        {
            double[][] dataA = new double[][]
            {
                new double[] {8.1,2.3,-1.5},
                new double[] {0.5,-6.23,0.87},
                new double[] {2.5,1.5,10.2},
            };

            double[][] dataB = new double[][]
            {
                new double[] {6.1},
                new double[] {2.3},
                new double[] {1.8},
            };

            Matrix A = new Matrix(dataA);
            Matrix b = new Matrix(dataB);

            // 通过LU上下三角分解法 求解线性方程组 Ax = b
            LuDecomposition d = new LuDecomposition(A);
            Matrix x = d.Solve(b);
        } */

            if (B.Rows != LU.Rows) throw new ArgumentException("Invalid matrix dimensions.");
            if (!IsNonSingular) throw new InvalidOperationException("Matrix is singular");

            // Copy right hand side with pivoting
            int count = B.Columns;
            Matrix X = B.Submatrix(pivotVector, 0, count - 1);

            int rows = LU.Rows;
            int columns = LU.Columns;
            double[][] lu = LU.Array;

            // Solve L*Y = B(piv,:)
            for (int k = 0; k < columns; k++)
            {
                for (int i = k + 1; i < columns; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        X[i, j] -= X[k, j] * lu[i][k];
                    }
                }
            }

            // Solve U*X = Y;
            for (int k = columns - 1; k >= 0; k--)
            {
                for (int j = 0; j < count; j++)
                {
                    X[k, j] /= lu[k][k];
                }

                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        X[i, j] -= X[k, j] * lu[i][k];
                    }
                }
            }

            return X;
        }
    }
}
