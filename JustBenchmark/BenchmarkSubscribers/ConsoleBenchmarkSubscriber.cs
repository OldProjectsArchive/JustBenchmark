using System;

namespace JustBenchmark.BenchmarkSubscribers {
	/// <summary>
	/// Print benchmark result to console
	/// </summary>
	public class ConsoleBenchmarkSubscriber : IBenchmarkSubscribers {
		/// <summary>
		/// Attribute suffix string
		/// </summary>
		public const string AttributeSuffix = "Attribute";

		/// <summary>
		/// Print benchmark result to console
		/// </summary>
		/// <param name="result"></param>
		public void OnResult(BenchmarkResult result) {
			var executorName = result.Executor.GetType().Name;
			if (executorName.EndsWith(AttributeSuffix)) {
				executorName = executorName.Substring(0, executorName.Length - AttributeSuffix.Length);
			}
			var methodFullName = $"{result.Method.DeclaringType.Name}.{result.Method.Name}";
			var elapsedSeconds = result.Elapsed.TotalSeconds;
			var collectionCounts = string.Join(", ", result.CollectionCounts);
			Console.WriteLine($"({executorName}) {methodFullName}: {elapsedSeconds}s, GC: [{collectionCounts}]");
		}
	}
}
