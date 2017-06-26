using JustBenchmark.BenchmarkExecutors;
using System;
using System.Reflection;

namespace JustBenchmark {
	/// <summary>
	/// Benchmark result
	/// </summary>
	public class BenchmarkResult {
		/// <summary>
		/// The executor used
		/// </summary>
		public IBenchmarkExecutor Executor { get; set; }
		/// <summary>
		/// The method used
		/// </summary>
		public MethodInfo Method { get; set; }
		/// <summary>
		/// Total time elapsed
		/// </summary>
		public TimeSpan Elapsed { get; set; }
		/// <summary>
		/// Collection count of generation 0~3
		/// </summary>
		public int[] CollectionCounts { get; set; }

		/// <summary>
		/// Initialize
		/// </summary>
		public BenchmarkResult() {
			CollectionCounts = new int[GC.MaxGeneration + 2];
		}
	}
}
