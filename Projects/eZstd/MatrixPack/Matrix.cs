// ----------------------------------------------------------------------------------------
// Lutz Roeder's .NET MatrixPack, September 2000, adapted from MatrixPack for COM and Jama routines
// http://www.aisto.com/roeder/dotnet
// roeder@aisto.com
// ----------------------------------------------------------------------------------------
namespace eZstd.MatrixPack
{
    using System;
    using System.IO;

    /// <summary>Matrix provides the fundamental operations of numerical linear algebra. 
    /// Matrix 类代表了任意形态（比如一维向量、方阵等）的 m*n 二维矩阵。</summary>
    /// <remarks> Matrix 代表了任意形态（比如一维向量、方阵等）的 m*n 二维矩阵。</remarks>
    public class Matrix
    {
        /// <summary> 二维嵌套数组，第一个下标代表行号，第二个下标代表列号。 </summary>
        private double[][] data;
        private int rows;
        private int columns;

        private static Random random = new Random();

        /// <summary>Constructs an empty matrix of the given size.</summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.data = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                this.data[i] = new double[columns];
            }
        }

        /// <summary>Constructs a matrix of the given size and assigns a given value to all diagonal elements.</summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <param name="value">Value to assign to the diagnoal elements.</param>
        public Matrix(int rows, int columns, double value)
        {
            this.rows = rows;
            this.columns = columns;
            this.data = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                data[i] = new double[columns];
            }
            for (int i = 0; i < rows; i++)
            {
                data[i][i] = value;
            }
        }

        /// <summary>Constructs a matrix from the given array.</summary>
        /// <param name="data">The array the matrix gets constructed from.</param>
        public Matrix(double[][] data)
        {
            this.rows = data.Length;
            if (rows == 0)
            {
                this.columns = 0;
            }
            else
            {
                this.columns = data[0].Length;

                for (int i = 0; i < rows; i++)
                {
                    if (data[i].Length != columns)
                    {
                        throw new ArgumentException("矩阵中每一行所含的元素个数必须相同。");
                    }
                }

            }

            this.data = data;
        }

        /// <summary> 二维嵌套数组，第一个下标代表行号，第二个下标代表列号。 </summary>
        internal double[][] Array
        {
            get
            {
                return this.data;
            }
        }

        /// <summary>Returns the number of columns.</summary>
        public int Rows
        {
            get
            {
                return this.rows;
            }
        }

        /// <summary>Returns the number of columns.</summary>
        public int Columns
        {
            get
            {
                return this.columns;
            }
        }

        /// <summary>Return <see langword="true"/> if the matrix is a square matrix.</summary>
        public bool IsSquare
        {
            get
            {
                return (rows == columns);
            }
        }

        /// <summary>Returns <see langword="true"/> if the matrix is symmetric.</summary>
        public bool IsSymmetric
        {
            get
            {
                if (this.IsSquare)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j <= i; j++)
                        {
                            if (data[i][j] != data[j][i])
                            {
                                return false;
                            }
                        }
                    }

                    return true;
                }

                return false;
            }
        }

        /// <summary>Access the value at the given location.</summary>
        public double this[int i, int j]
        {
            set
            {
                this.data[i][j] = value;
            }
            get
            {
                return this.data[i][j];
            }
        }

        #region ---   Submatrix 提取子矩阵

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="i0">Start row index</param>
        /// <param name="i1">End row index</param>
        /// <param name="j0">Start column index</param>
        /// <param name="j1">End column index</param>
        public Matrix Submatrix(int i0, int i1, int j0, int j1)
        {
            if ((i0 > i1) || (j0 > j1) || (i0 < 0) || (i0 >= this.rows) || (i1 < 0) || (i1 >= this.rows) || (j0 < 0) || (j0 >= this.columns) || (j1 < 0) || (j1 >= this.columns))
            {
                throw new ArgumentException();
            }

            Matrix X = new Matrix(i1 - i0 + 1, j1 - j0 + 1);
            double[][] x = X.Array;
            for (int i = i0; i <= i1; i++)
            {
                for (int j = j0; j <= j1; j++)
                {
                    x[i - i0][j - j0] = data[i][j];
                }
            }

            return X;
        }

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="r">Array of row indices</param>
        /// <param name="c">Array of row indices</param>
        public Matrix Submatrix(int[] r, int[] c)
        {
            Matrix X = new Matrix(r.Length, c.Length);
            double[][] x = X.Array;
            for (int i = 0; i < r.Length; i++)
            {
                for (int j = 0; j < c.Length; j++)
                {
                    if ((r[i] < 0) || (r[i] >= rows) || (c[j] < 0) || (c[j] >= columns))
                    {
                        throw new ArgumentException();
                    }

                    x[i][j] = data[r[i]][c[j]];
                }
            }

            return X;
        }

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="i0">Starttial row index</param>
        /// <param name="i1">End row index</param>
        /// <param name="c">Array of row indices</param>
        public Matrix Submatrix(int i0, int i1, int[] c)
        {
            if ((i0 > i1) || (i0 < 0) || (i0 >= this.rows) || (i1 < 0) || (i1 >= this.rows))
            {
                throw new ArgumentException();
            }

            Matrix X = new Matrix(i1 - i0 + 1, c.Length);
            double[][] x = X.Array;
            for (int i = i0; i <= i1; i++)
            {
                for (int j = 0; j < c.Length; j++)
                {
                    if ((c[j] < 0) || (c[j] >= columns))
                    {
                        throw new ArgumentException();
                    }

                    x[i - i0][j] = data[i][c[j]];
                }
            }

            return X;
        }

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="rowIndices">要从父矩阵中提取出来的行号。Array of row indices</param>
        /// <param name="startColumn">Start column index</param>
        /// <param name="endColumn">End column index</param>
        public Matrix Submatrix(int[] rowIndices, int startColumn, int endColumn)
        {
            if ((startColumn > endColumn) || (startColumn < 0) || (startColumn >= columns) || (endColumn < 0) || (endColumn >= columns))
            {
                throw new ArgumentException();
            }

            Matrix X = new Matrix(rowIndices.Length, endColumn - startColumn + 1);
            double[][] x = X.Array;
            for (int i = 0; i < rowIndices.Length; i++)
            {
                for (int j = startColumn; j <= endColumn; j++)
                {
                    if ((rowIndices[i] < 0) || (rowIndices[i] >= this.rows))
                    {
                        throw new ArgumentException();
                    }

                    x[i][j - startColumn] = data[rowIndices[i]][j];
                }
            }

            return X;
        }

        #endregion

        #region ---   GetVector 提取某一行或一列的向量

        /// <summary> 提取矩阵中的某一列的数据 </summary>
        /// <param name="column">要提取的列的列号</param>
        public double[] GetColumn(int column)
        {
            double[] vec = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                vec[i] = this[i, column];
            }
            return vec;
        }

        /// <summary> 提取矩阵中的某一行的数据 </summary>
        /// <param name="row">要提取的行的行号</param>
        public double[] GetRow(int row)
        {
            double[] vec = new double[columns];

            for (int i = 0; i < columns; i++)
            {
                vec[i] = this[row, i];
            }
            return vec;
        }

        /// <summary> 提取矩阵中的某一列中指定行的数据 </summary>
        /// <param name="rowIndices">要提取行的行号集合</param>
        /// <param name="column">要提取的列的列号</param>
        public double[] GetVector(int[] rowIndices, int column)
        {
            double[] vec = new double[rowIndices.Length];

            for (int i = 0; i < rowIndices.Length; i++)
            {
                vec[i] = this[rowIndices[i], column];
            }
            return vec;
        }

        /// <summary> 提取矩阵中的某一行中指定列的数据 </summary>
        /// <param name="columnIndices">要提取列的列号集合</param>
        /// <param name="row">要提取的行的行号</param>
        public double[] GetVector(int row, int[] columnIndices)
        {
            double[] vec = new double[columnIndices.Length];

            for (int i = 0; i < columnIndices.Length; i++)
            {
                vec[i] = this[row, columnIndices[i]];
            }
            return vec;
        }

        #endregion

        /// <summary>Creates a copy of the matrix.</summary>
        public Matrix Clone()
        {
            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    x[i][j] = data[i][j];
                }
            }

            return X;
        }

        /// <summary>Returns the transposed matrix.</summary>
        public Matrix Transpose()
        {
            Matrix X = new Matrix(columns, rows);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    x[j][i] = data[i][j];
                }
            }

            return X;
        }

        /// <summary>Returns the One Norm for the matrix.</summary>
        /// <value>The maximum column sum.</value>
        public double Norm1
        {
            get
            {
                double f = 0;
                for (int j = 0; j < columns; j++)
                {
                    double s = 0;
                    for (int i = 0; i < rows; i++)
                    {
                        s += Math.Abs(data[i][j]);
                    }

                    f = Math.Max(f, s);
                }
                return f;
            }
        }

        /// <summary>Returns the Infinity Norm for the matrix.</summary>
        /// <value>The maximum row sum.</value>
        public double InfinityNorm
        {
            get
            {
                double f = 0;
                for (int i = 0; i < rows; i++)
                {
                    double s = 0;
                    for (int j = 0; j < columns; j++)
                        s += Math.Abs(data[i][j]);
                    f = Math.Max(f, s);
                }
                return f;
            }
        }

        /// <summary>Returns the Frobenius Norm for the matrix.</summary>
        /// <value>The square root of sum of squares of all elements.</value>
        public double FrobeniusNorm
        {
            get
            {
                double f = 0;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        f = this.Hypotenuse(f, data[i][j]);
                    }
                }

                return f;
            }
        }

        #region ---   矩阵运算符

        /// <summary>Unary minus.</summary>
        public static Matrix operator -(Matrix a)
        {
            int rows = a.Rows;
            int columns = a.Columns;
            double[][] data = a.Array;

            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    x[i][j] = -data[i][j];
                }
            }

            return X;
        }

        /// <summary>Matrix addition.</summary>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            int rows = a.Rows;
            int columns = a.Columns;
            double[][] data = a.Array;

            if ((rows != b.Rows) || (columns != b.Columns))
            {
                throw new ArgumentException("Matrix dimension do not match.");
            }

            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    x[i][j] = data[i][j] + b[i, j];
                }
            }
            return X;
        }

        /// <summary>Matrix subtraction.</summary>
        public static Matrix operator -(Matrix a, Matrix b)
        {
            int rows = a.Rows;
            int columns = a.Columns;
            double[][] data = a.Array;

            if ((rows != b.Rows) || (columns != b.Columns))
            {
                throw new ArgumentException("Matrix dimension do not match.");
            }

            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    x[i][j] = data[i][j] - b[i, j];
                }
            }
            return X;
        }

        /// <summary>Matrix-scalar multiplication.</summary>
        public static Matrix operator *(Matrix a, double s)
        {
            int rows = a.Rows;
            int columns = a.Columns;
            double[][] data = a.Array;

            Matrix X = new Matrix(rows, columns);

            double[][] x = X.Array;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    x[i][j] = data[i][j] * s;
                }
            }

            return X;
        }

        /// <summary>Matrix-matrix multiplication.</summary>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            int rows = a.Rows;
            double[][] data = a.Array;

            if (b.Rows != a.columns)
            {
                throw new ArgumentException("Matrix dimensions are not valid.");
            }

            int columns = b.Columns;
            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;

            int size = a.columns;
            double[] column = new double[size];
            for (int j = 0; j < columns; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    column[k] = b[k, j];
                }
                for (int i = 0; i < rows; i++)
                {
                    double[] row = data[i];
                    double s = 0;
                    for (int k = 0; k < size; k++)
                    {
                        s += row[k] * column[k];
                    }
                    x[i][j] = s;
                }
            }

            return X;
        }

        #endregion

        /*
        /// <summary>Adds a matrix to the current matrix.</summary>
        public void Add(Matrix A)
        {
        if ((rows != A.Rows) || (columns != A.Columns)) throw new ArgumentException();
        double[][] a = A.Array;
        for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
        data[i][j] += a[i][j];
        }

        /// <summary>Subtracts a matrix from the current matrix.</summary>
        public void Sub(Matrix A)
        {
        if ((rows != A.Rows) || (this.columns != A.Columns)) throw new ArgumentException();
        double[][] a = A.Array;
        for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
        data[i][j] -= a[i][j];
        }

        /// <summary>Multiplies the current matrix with a scalar factor.</summary>
        public void Times(double s)
        {
        for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
        data[i][j] *= s;
        }
        */

        /// <summary> 对矩阵方程 A * X = B 进行求解。</summary>
        /// <param name="rhs">right hand side of the equation A * X = B .线性方程 A * X = B ，的右手边的参数，即 B </param>
        /// <returns> 如果矩阵A为方阵，则返回LU分解的求解结果，如果不是方阵，则返回QR分解的最小二乘解。
        /// Returns the LHS (left hand side) solution vetor if the matrix is square or the least squares solution otherwise.</returns>
        public Matrix Solve(Matrix rhs)
        {
            return (rows == columns) ? new LuDecomposition(this).Solve(rhs) : new QrDecomposition(this).Solve(rhs);
        }

        /// <summary>Inverse of the matrix if matrix is square, pseudoinverse otherwise.</summary>
        public Matrix Inverse
        {
            get
            {
                return this.Solve(Diagonal(rows, rows, 1.0));
            }
        }

        /// <summary>Determinant if matrix is square. 方阵的行列式 </summary>
        public double Determinant
        {
            get
            {
                return new LuDecomposition(this).Determinant;
            }
        }

        /// <summary>Returns the trace of the matrix.</summary>
        /// <returns>Sum of the diagonal elements.</returns>
        public double Trace
        {
            get
            {
                double trace = 0;
                for (int i = 0; i < Math.Min(rows, columns); i++)
                {
                    trace += data[i][i];
                }
                return trace;
            }
        }

        /// <summary>Returns a matrix filled with random values.</summary>
        public static Matrix Random(int rows, int columns)
        {
            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    x[i][j] = random.NextDouble();
                }
            }
            return X;
        }

        /// <summary>Returns a diagonal matrix（对角矩阵：只有i=j的位置的元素值才不为0） of the given size.</summary>
        /// <param name="value">对角位置的元素的值。</param>
        public static Matrix Diagonal(int rows, int columns, double value)
        {
            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    x[i][j] = ((i == j) ? value : 0.0);
                }
            }
            return X;
        }

        /// <summary>Returns the matrix in a textual form.</summary>
        public override string ToString()
        {
            using (StringWriter writer = new StringWriter())
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                        writer.Write(data[i][j] + "\t");

                    writer.WriteLine();
                }

                return writer.ToString();
            }
        }

        private double Hypotenuse(double a, double b)
        {
            if (Math.Abs(a) > Math.Abs(b))
            {
                double r = b / a;
                return Math.Abs(a) * Math.Sqrt(1 + r * r);
            }

            if (b != 0)
            {
                double r = a / b;
                return Math.Abs(b) * Math.Sqrt(1 + r * r);
            }

            return 0.0;
        }
    }
}
