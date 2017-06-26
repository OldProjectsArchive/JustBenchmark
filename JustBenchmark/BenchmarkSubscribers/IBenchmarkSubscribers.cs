namespace JustBenchmark.BenchmarkSubscribers {
	/// <summary>
	/// Interface for handle benchmark result
	/// </summary>
	public interface IBenchmarkSubscribers {
		/// <summary>
		/// Receive benchmark result
		/// </summary>
		/// <param name="result"></param>
		void OnResult(BenchmarkResult result);
	}
}
