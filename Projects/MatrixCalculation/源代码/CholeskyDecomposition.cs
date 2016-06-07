// ----------------------------------------------------------------------------------------
// Lutz Roeder's .NET Mapack, September 2000, adapted from Mapack for COM and Jama routines
// http://www.aisto.com/roeder/dotnet
// roeder@aisto.com
// ----------------------------------------------------------------------------------------
namespace Mapack
{
	using System;

	/// <summary>
	///		Cholesky Decomposition of a symmetric, positive definite matrix.
	///	</summary>
	/// <remarks>
	///		For a symmetric, positive definite matrix <c>A</c>, the Cholesky decomposition is a
	///		lower triangular matrix <c>L</c> so that <c>A = L * L'</c>.
	///		If the matrix is not symmetric or positive definite, the constructor returns a partial 
	///		decomposition and sets two internal variables that can be queried using the
	///		<see cref="IsSymmetric"/> and <see cref="IsPositiveDefinite"/> properties.
	///	</remarks>
	public class CholeskyDecomposition
	{
		private Matrix L;
		private bool isSymmetric;
		private	bool isPositiveDefinite;

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
					d = d + s*s;
					isSymmetric = isSymmetric & (a[k][j] == a[j][k]); 
				}

				d = a[j][j] - d;
				isPositiveDefinite = isPositiveDefinite & (d > 0.0);
				l[j][j] = Math.Sqrt(Math.Max(d,0.0));
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

		/// <summary>Returns <see langword="true"/> if the matrix is positive definite.</summary>
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

			Matrix B = (Matrix) rhs.Clone();
			double[][] l = L.Array;

			// Solve L*Y = B;
			for (int k = 0; k < L.Rows; k++)
			{
				for (int i = k + 1; i < dimension; i++)
				{
					for (int j = 0; j < count; j++)
					{
						B[i,j] -= B[k,j] * l[i][k];
					}
				}

				for (int j = 0; j < count; j++)
				{
					B[k,j] /= l[k][k];
				}
			}

			// Solve L'*X = Y;
			for (int k = dimension - 1; k >= 0; k--)
			{
				for (int j = 0; j < count; j++)
				{
					B[k,j] /= l[k][k];
				}

				for (int i = 0; i < k; i++)
				{
					for (int j = 0; j < count; j++)
					{
						B[i,j] -= B[k,j] * l[k][i];
					}
				}
			}

			return B;
		}
	}
}
