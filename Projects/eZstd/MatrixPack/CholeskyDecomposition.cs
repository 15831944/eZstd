// ----------------------------------------------------------------------------------------
// Lutz Roeder's .NET MatrixPack, September 2000, adapted from MatrixPack for COM and Jama routines
// http://www.aisto.com/roeder/dotnet
// roeder@aisto.com
// ----------------------------------------------------------------------------------------

namespace eZstd.MatrixPack
{
    using System;
    /// <summary>
    ///		Cholesky Decomposition of a symmetric, positive definite matrix. 
    /// 即对称正定矩阵的 Cholesky 分解，以及求解线性方程组的平方根法。
    ///	</summary>
    /// <remarks>
    ///		For a symmetric, positive definite matrix <c>A</c>, the Cholesky decomposition is a
    ///		lower triangular matrix <c>L</c> so that <c>A = L * L'</c>. 其中 L'是L的共轭转置矩阵。
    ///    只要A满足以上两个条件，L是唯一确定的，而且L的对角元素肯定是正数。
    ///		If the matrix is not symmetric or positive definite, the constructor returns a partial 
    ///		decomposition and sets two internal variables that can be queried using the
    ///		<see cref="IsSymmetric"/> and <see cref="IsPositiveDefinite"/> properties.
    ///	</remarks>
    public class CholeskyDecomposition
    {
        private Matrix L;
        private bool isSymmetric;
        private bool isPositiveDefinite;

        /// <summary>Construct a Cholesky Decomposition.</summary>
        public CholeskyDecomposition(Matrix A)
        {
            if (!A.IsSquare)
            {
                throw new ArgumentNullException("Matrix is not square.");
            }

            int dimension = A.Rows;
            L = new Matrix(dimension, dimension);

            double[][] a = A.Array;
            double[][] l = L.Array;

            isPositiveDefinite = true;
            isSymmetric = true;

            for (int j = 0; j < dimension; j++)
            {
                double[] Lrowj = l[j];
                double d = 0.0;
                for (int k = 0; k < j; k++)
                {
                    double[] Lrowk = l[k];
                    double s = 0.0;
                    for (int i = 0; i < k; i++)
                    {
                        s += Lrowk[i] * Lrowj[i];
                    }
                    Lrowj[k] = s = (a[j][k] - s) / l[k][k];
                    d = d + s * s;
                    isSymmetric = isSymmetric & (a[k][j] == a[j][k]);
                }

                d = a[j][j] - d;
                isPositiveDefinite = isPositiveDefinite & (d > 0.0);
                l[j][j] = Math.Sqrt(Math.Max(d, 0.0));
                for (int k = j + 1; k < dimension; k++)
                    l[j][k] = 0.0;
            }
        }

        /// <summary>Returns <see langword="true"/> if the matrix is symmetric.</summary>
        public bool IsSymmetric
        {
            get
            {
                return this.isSymmetric;
            }
        }

        /// <summary>Returns <see langword="true"/> if the matrix is positive definite（正定矩阵）.
        /// 若 n*n 实对称矩阵A对任意非零n维实向量x恒有xt*A*x > 0 f（其中xt为向量x的转置向量），则称A为正定矩阵。</summary>
        public bool IsPositiveDefinite
        {
            get
            {
                return this.isPositiveDefinite;
            }
        }

        /// <summary>Returns the left triangular factor <c>L</c> so that <c>A = L * L'</c>.</summary>
        public Matrix LeftTriangularFactor
        {
            get
            {
                return this.L;
            }
        }

        /// <summary>Solves a set of equation systems of type <c>A * X = B</c>.</summary>
        /// <param name="rhs">Right hand side matrix with as many rows as <c>A</c> and any number of columns.</param>
        /// <returns>Matrix <c>X</c> so that <c>L * L' * X = B</c>.</returns>
        /// <remarks>
        ///     基于正定矩阵的 Cholesky 分解来求解 A*x=b的方法称为平方根法。
        ///     可以验证，正定矩阵的Cholesky的分解的乘除法总计算量约为n^3/6（由于对称性，计算量约为LU分解的一半）。
        ///     Cholesky 分解的优点之一是不必选主元，此外，Cholesky 方法还有一个突出的优点，即数值稳定性，在乘除的过程中，Ljk的数量级不会增长。
        ///     进一步地可以参考“改进的平方根法”，其计算量与平方根法一样，但是没有开方运算。
        /// </remarks>
        /// <exception cref="T:System.ArgumentException">Matrix dimensions do not match.</exception>
        /// <exception cref="T:System.InvalidOperationException">Matrix is not symmetrix and positive definite.</exception>
        public Matrix Solve(Matrix rhs)
        {
            if (rhs.Rows != L.Rows)
            {
                throw new ArgumentException("Matrix dimensions do not match.");
            }
            if (!isSymmetric)
            {
                throw new InvalidOperationException("Matrix is not symmetric.");
            }
            if (!isPositiveDefinite)
            {
                throw new InvalidOperationException("Matrix is not positive definite.");
            }

            int dimension = L.Rows;
            int count = rhs.Columns;

            Matrix B = (Matrix)rhs.Clone();
            double[][] l = L.Array;

            // Solve L*Y = B;
            for (int k = 0; k < L.Rows; k++)
            {
                for (int i = k + 1; i < dimension; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        B[i, j] -= B[k, j] * l[i][k];
                    }
                }

                for (int j = 0; j < count; j++)
                {
                    B[k, j] /= l[k][k];
                }
            }

            // Solve L'*X = Y;
            for (int k = dimension - 1; k >= 0; k--)
            {
                for (int j = 0; j < count; j++)
                {
                    B[k, j] /= l[k][k];
                }

                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        B[i, j] -= B[k, j] * l[k][i];
                    }
                }
            }

            return B;
        }
    }
}
