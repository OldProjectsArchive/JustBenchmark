using System;
using System.Reflection;

namespace JustBenchmark.BenchmarkExecutors {
	/// <summary>
	/// Extension methods for IBenchmarkExecutor
	/// </summary>
	public static class IBenchmarkExecutorExtensions {
		/// <summary>
		/// Get benchmark result with elapsed time and GC collection counts
		/// </summary>
		/// <param name="method"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public static BenchmarkResult GenericExecute(
			this IBenchmarkExecutor executor, MethodInfo method, Action action) {
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			var collectionCounts = new int[GC.MaxGeneration + 2];
			for (var x = 0; x < collectionCounts.Length; ++x) {
				collectionCounts[x] = GC.CollectionCount(x);
			}
			var begin = DateTime.UtcNow;
			action();
			var elapsed = DateTime.UtcNow - begin;
			for (var x = 0; x < collectionCounts.Length; ++x) {
				collectionCounts[x] = GC.CollectionCount(x) - collectionCounts[x];
			}
			var result = new BenchmarkResult();
			result.Executor = executor;
			result.Method = method;
			result.Elapsed = elapsed;
			Array.Copy(collectionCounts, result.CollectionCounts, result.CollectionCounts.Length);
			return result;
		}
	}
}
